using System;
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
        
        [UIParams] private readonly BSMLParserParams _parserParams = null;

        [UIComponent("censorList")] 
        private readonly CustomListTableData _censorList = null;
        
        [UIComponent("mapListCustomMapList")]
        private readonly CustomListTableData _mapList = null;
        
        [UIComponent("chosenMapToFilter")]
        private readonly CustomListTableData _chosenMapToFilter = null;
        
        [UIComponent("removeButton")]
        private readonly Button _removeButton = null;
        
        [UIComponent("editButton")]
        private readonly Button _editButton = null;
        
        private KeyValuePair<string, (bool, bool, bool)> _currentlySelectedMap = new KeyValuePair<string, (bool, bool, bool)>(String.Empty, (true, false, false));

        private int _currentlySelectedCensoredMapIndex = 0;
        
        [UIValue("editModalCensorSongValue")]
        private bool _censorSongValue
        {
            get => _currentlySelectedMap.Value.Item1;
            set => _currentlySelectedMap = new KeyValuePair<string, (bool, bool, bool)>(
                _currentlySelectedMap.Key,
                (value, _currentlySelectedMap.Value.Item2, _currentlySelectedMap.Value.Item3)
            );
        }
        
        [UIValue("editModalCensorCoverArtValue")]
        private bool _censorCoverArtValue
        {
            get => _currentlySelectedMap.Value.Item2;
            set => _currentlySelectedMap = new KeyValuePair<string, (bool, bool, bool)>(
                _currentlySelectedMap.Key,
                (_currentlySelectedMap.Value.Item1, value, _currentlySelectedMap.Value.Item3)
            );
        }
        
        [UIValue("editModalCensorSongNameValue")]
        private bool _censorSongNameValue
        {
            get => _currentlySelectedMap.Value.Item3;
            set => _currentlySelectedMap = new KeyValuePair<string, (bool, bool, bool)>(
                _currentlySelectedMap.Key,
                (_currentlySelectedMap.Value.Item1, _currentlySelectedMap.Value.Item2, value)
            );
        }

        [UIValue("#post-parse")]
        void PostParse()
        {
            _mapList.Data = Loader.CustomLevels.Values.Select(GetCustomCellInfo).ToList();
            _mapList.TableView.ReloadData();
            Plugin.Log.Info(_mapList.Data.Count.ToString());

            _editButton.interactable = false;
            _removeButton.interactable = false;
            
            ReloadCensorList();
            _chosenMapToFilter.Data = new List<CustomListTableData.CustomCellInfo>()
            {
                new CustomListTableData.CustomCellInfo("Select to choose map")
            };
            _chosenMapToFilter.TableView.ReloadData();
        }

        [UIAction("censorListCell")]
        private void censorListCellOnSelect(TableView tableView, int cell)
        {
            _currentlySelectedCensoredMapIndex = cell;
        }

        [UIAction("addButtonOnClick")]
        private void addButtonOnClick()
        {
            _chosenMapToFilter.Data = new List<CustomListTableData.CustomCellInfo>()
            {
                new CustomListTableData.CustomCellInfo("Select to choose map")
            };
            _chosenMapToFilter.TableView.ReloadData();
            _parserParams.EmitEvent("editModalShow");
            NotifyPropertyChanged(null);
        }

        [UIAction("editButtonOnClick")]
        private void editButtonOnClick()
        {
            var mapSelected =
                Loader.GetLevelById(_config.CensoredSongs.ElementAt(_currentlySelectedCensoredMapIndex).Key);

            _chosenMapToFilter.Data = new List<CustomListTableData.CustomCellInfo>()
            {
                new CustomListTableData.CustomCellInfo(mapSelected?.songName, mapSelected?.songAuthorName)
            };
            _chosenMapToFilter.TableView.ReloadData();
            _parserParams.EmitEvent("editModalShow");
        }

        [UIAction("onChosenMapToFilterSelected")]
        private void onChosenMapToFilterSelected(TableView tableView, int cell)
        {
            tableView.ClearSelection();
            _parserParams.EmitEvent("mapListModalShow");
        }

        [UIAction("onMapListCellSelect")]
        private void onMapListCellSelect(TableView tableView, int cell)
        {
            var beatmap = Loader.CustomLevels.ElementAt(cell).Value;
            tableView.ClearSelection();
            _currentlySelectedMap = new KeyValuePair<string, (bool, bool, bool)>(beatmap.levelID, _currentlySelectedMap.Value);
            _chosenMapToFilter.Data = new List<CustomListTableData.CustomCellInfo>()
            {
                GetCustomCellInfo(beatmap)
            };
            _chosenMapToFilter.TableView.ReloadData();
            _parserParams.EmitEvent("mapListModalHide");
        }
        
        [UIAction("editModalCloseButtonOnClick")]
        private void editModalCloseButtonOnClick() => _parserParams.EmitEvent("editModalHide");
        
        private CustomListTableData.CustomCellInfo GetCustomCellInfo(BeatmapLevel level) => new CustomListTableData.CustomCellInfo(level.songName, level.songAuthorName);

        private void ReloadCensorList()
        {
            _censorList.Data = _config.CensoredSongs.Keys.Select(i => GetCustomCellInfo(Loader.GetLevelById(i)))
                .ToList();
            _censorList.TableView.ReloadData();
        }
        
        
        public void Initialize()
        {
            GameplaySetup.Instance.AddTab("SongCensor", "SongCensor.UI.BSML.CensorListView.bsml", this);
        }

        public void Dispose()
        {
            GameplaySetup.Instance.RemoveTab("SongCensor");
        }
    }
}