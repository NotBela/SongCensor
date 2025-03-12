using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using SongCensor.Configuration;
using SongCore;
using UnityEngine.UI;
using Zenject;

namespace SongCensor.UI.BSML
{
    public class CensorListViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null;
        [Inject] private readonly SongPreviewPlayer _songPreviewPlayer = null;

        [UIComponent("addButton")] private readonly Button _addButton = null;
        [UIComponent("removeButton")] private readonly Button _removeButton = null;
        [UIComponent("censorList")] private readonly CustomListTableData _censorList = null;

        private BeatmapLevel _selectedLevel;
        private int _selectedCensorListCell;

        private void reloadCensorListData()
        {
            _censorList.Data = _config.CensoredSongs.Select(i => getCustomCellInfo(Loader.GetLevelById(i))).ToList();
            _censorList.TableView.ReloadData();
        }

        private CustomListTableData.CustomCellInfo getCustomCellInfo(BeatmapLevel map) =>
            new CustomListTableData.CustomCellInfo(map.songName, map.songAuthorName);
        
        public void Initialize()
        {
            _levelCollectionViewController.didSelectLevelEvent += DidSelectLevelEvent;
            _levelCollectionViewController.didSelectHeaderEvent += DidSelectHeaderEvent;
            Loader.SongsLoadedEvent += OnSongLoad;
            
            GameplaySetup.Instance.AddTab("SongCensor", "SongCensor.UI.BSML.CensorListView.bsml", this);
        }

        private void OnSongLoad(Loader arg1, ConcurrentDictionary<string, BeatmapLevel> arg2)
        {
            reloadCensorListData();
        }

        [UIAction("addButtonOnClick")]
        private void addButtonOnClick()
        {
            if (_selectedLevel == null) return;
            if (_config.CensoredSongs.Contains(_selectedLevel.levelID)) return;
            
            _songPreviewPlayer.CrossfadeToDefault();
            
            _config.CensoredSongs.Add(_selectedLevel.levelID);
            reloadCensorListData();
        }

        [UIAction("removeButtonOnClick")]
        private void removeButtonOnClick()
        {
            _removeButton.interactable = false;
            _censorList.TableView.ClearSelection();
            
            _config.CensoredSongs.RemoveAt(_selectedCensorListCell);
            reloadCensorListData();
        }

        [UIAction("censorListCell")]
        private void onCensorListCellSelect(TableView _, int cellIdx)
        {
            _removeButton.interactable = true;

            _selectedCensorListCell = cellIdx;
        }

        private void DidSelectHeaderEvent(LevelCollectionViewController _)
        {
            _addButton.interactable = false;
        }

        private void DidSelectLevelEvent(LevelCollectionViewController _, BeatmapLevel level)
        {
            _selectedLevel = level;

            _addButton.interactable = true;
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