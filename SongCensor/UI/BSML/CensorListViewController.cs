using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using SongCensor.Configuration;
using SongCore;
using UnityEngine.UI;
using Zenject;
using Toggle = UnityEngine.UIElements.Toggle;

namespace SongCensor.UI.BSML
{
    public class CensorListViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null;
        [Inject] private readonly SongPreviewPlayer _songPreviewPlayer = null;

        [UIParams] private readonly BSMLParserParams _parserParams = null;

        [UIComponent("addButton")] private readonly Button _addButton = null;
        [UIComponent("censorList")] private readonly CustomCellListTableData _censorList = null;

        private BeatmapLevel _selectedLevel;
        private int _selectedCensorListCell;

        private BeatmapLevel _lastEditedListCell = null;

        #region CensorList
        
        private void DidSelectHeaderEvent(LevelCollectionViewController _)
        {
            _addButton.interactable = false;
        }

        private void DidSelectLevelEvent(LevelCollectionViewController _, BeatmapLevel level)
        {
            _selectedLevel = level;

            _addButton.interactable = !_config.CensoredSongs.ContainsKey(level.levelID);
        }
        
        private void reloadCensorListData()
        {
            var list = _config.CensoredSongs.Select(i =>
            {
                var beatmap = Loader.GetLevelById(i.Key);

                return new SongListElement(beatmap);
            }).ToList();
            _censorList.Data = list.Cast<object>().ToList();

            _censorList.TableView.ReloadData();
        }

        private void OnSongLoad(Loader arg1, ConcurrentDictionary<string, BeatmapLevel> arg2)
        {
            reloadCensorListData();
        }

        [UIAction("addButtonOnClick")]
        private void addButtonOnClick()
        {
            if (_selectedLevel == null) return;
            if (_config.CensoredSongs.ContainsKey(_selectedLevel.levelID)) return;
            
            _songPreviewPlayer.CrossfadeToDefault();
            
            _config.CensoredSongs.Add(_selectedLevel.levelID, new MapSettings(true, false));
            _addButton.interactable = false;
            reloadCensorListData();
        }

        [UIAction("censorListCell")]
        private void onCensorListCellSelect(TableView _, int cellIdx)
        {
            _selectedCensorListCell = cellIdx;
        }
        
        #endregion

        #region EditModal

        [UIComponent("censorCoverArtToggle")] private readonly ToggleSetting _censorCoverArtToggle = null;
        [UIComponent("censorSongToggle")] private readonly ToggleSetting _censorSongToggle = null;
        
        public void OpenEditMenu(BeatmapLevel beatmapLevel)
        {
            _lastEditedListCell = beatmapLevel;

            _parserParams.EmitEvent("editModalShow");
            
            _censorCoverArtToggle.Value = _config.CensoredSongs[_lastEditedListCell.levelID].CensorCoverArt;
            _censorSongToggle.Value = _config.CensoredSongs[_lastEditedListCell.levelID].CensorSong;
        }
        
        [UIValue("censorSongValue")]
        private bool censorSongBool
        {
            get => _lastEditedListCell != null && _config.CensoredSongs[_lastEditedListCell.levelID].CensorSong;
            set
            {
                if (!_config.CensoredSongs.TryGetValue(_lastEditedListCell.levelID, out var song))
                {
                    _config.CensoredSongs.Add(_lastEditedListCell.levelID, new MapSettings(value, false));
                    return;
                }
                
                song.CensorSong = value;
            }
        }
        
        [UIValue("censorCoverArtValue")]
        private bool censorCoverArtBool
        {
            get => _lastEditedListCell != null && _config.CensoredSongs[_lastEditedListCell.levelID].CensorCoverArt;
            set
            {
                if (!_config.CensoredSongs.TryGetValue(_lastEditedListCell.levelID, out var song))
                {
                    _config.CensoredSongs.Add(_lastEditedListCell.levelID, new MapSettings(true, value));
                    return;
                }
                
                song.CensorCoverArt = value;
            }
        }

        #endregion
        
        public void Initialize()
        {
            _levelCollectionViewController.didSelectLevelEvent += DidSelectLevelEvent;
            _levelCollectionViewController.didSelectHeaderEvent += DidSelectHeaderEvent;
            Loader.SongsLoadedEvent += OnSongLoad;
            
            GameplaySetup.Instance.AddTab("SongCensor", "SongCensor.UI.BSML.CensorListView.bsml", this);
        }

        public void Dispose()
        {
            _levelCollectionViewController.didSelectLevelEvent -= DidSelectLevelEvent;
            _levelCollectionViewController.didSelectHeaderEvent -= DidSelectHeaderEvent;
            Loader.SongsLoadedEvent -= OnSongLoad;
            
            GameplaySetup.Instance.RemoveTab("SongCensor");
        }
    }
}