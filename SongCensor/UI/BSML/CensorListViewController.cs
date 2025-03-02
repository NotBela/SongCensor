using System;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Tags;
using BeatSaberMarkupLanguage.ViewControllers;
using SongCensor.Configuration;
using SongCore;
using Zenject;

namespace SongCensor.UI.BSML
{
    public class CensorListViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null;
        
        private BeatmapLevel _currentLevel;

        [UIComponent("censorList")] 
        private readonly CustomListTableData _censorList = null;

        [UIAction("#post-parse")]
        void PostParse()
        {
            _censorList.Data = _config.CensoredSongs.Keys.Select(i => getCellInfo(Loader.GetLevelById(i))).ToList();
            _censorList.TableView.ReloadData();
        }

        private CustomListTableData.CustomCellInfo getCellInfo(BeatmapLevel level) =>
            new CustomListTableData.CustomCellInfo(level.songName, level.songAuthorName);
        
        private void onSongSelected(LevelCollectionViewController _, BeatmapLevel beatmapLevel)
        {
            _currentLevel = beatmapLevel;
            NotifyPropertyChanged(null);
        }
        
        public void Initialize()
        {
            _levelCollectionViewController.didSelectLevelEvent += onSongSelected;
            GameplaySetup.Instance.AddTab("SongCensor", "SongCensor.UI.BSML.CensorListView.bsml", this);
        }

        public void Dispose()
        {
            _levelCollectionViewController.didSelectLevelEvent -= onSongSelected;
            GameplaySetup.Instance.RemoveTab("SongCensor");
        }
    }
}