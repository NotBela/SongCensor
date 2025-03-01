using System;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using ThisSongSucks.Censor;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.UI.Controllers
{
    public class CensorListViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null;
        
        private BeatmapLevel _currentLevel = null;
        
        [UIComponent("enableSongNameCensoring")]
        private readonly ToggleSetting _enableSongNameCensoringToggle = null;
        
        [UIComponent("enableSongCensoring")]
        private readonly ToggleSetting _enableSongCensoringToggle = null;
        
        [UIComponent("enableCoverArtCensoring")]
        private readonly ToggleSetting _enableCoverArtCensoringToggle = null;

        [UIValue("enableSongCensoringValue")]
        private bool enableSongCensoring
        {
            get
            {
                if (_currentLevel == null) return false;
                return _config.censoredSongs.Contains(_currentLevel.levelID);
            }
            set
            {
                if (value)
                {
                    _config.censoredSongs.Add(_currentLevel.levelID);
                    return;
                }
                if (_config.censoredSongs.Contains(_currentLevel.levelID)) 
                    _config.censoredSongs.Remove(_currentLevel.levelID);
            }
        }

        [UIValue("enableCoverArtCensoringValue")]
        private bool enableCoverArtCensoring
        {
            get
            {
                if (_currentLevel == null) return false;
                return _config.censoredCoverArt.Contains(_currentLevel.levelID);
            }
            set
            {
                if (value)
                {
                    _config.censoredCoverArt.Add(_currentLevel.levelID);
                    return;
                }
                if (_config.censoredCoverArt.Contains(_currentLevel.levelID)) 
                    _config.censoredCoverArt.Remove(_currentLevel.levelID);
            }
        }

        [UIValue("enableSongNameCensoringValue")]
        private bool enableSongNameCensoring
        {
            get
            {
                if (_currentLevel == null) return false;
                return _config.censoredSongNames.Contains(_currentLevel.levelID);
            }
            set
            {
                if (value)
                {
                    _config.censoredSongNames.Add(_currentLevel.levelID);
                    return;
                }
                if (_config.censoredSongNames.Contains(_currentLevel.levelID)) 
                    _config.censoredSongNames.Remove(_currentLevel.levelID);
            }
        }

        private void onSongSelected(LevelCollectionViewController _, BeatmapLevel beatmapLevel)
        {
            _enableSongCensoringToggle.Interactable = true;
            _enableCoverArtCensoringToggle.Interactable = true;
            _enableSongNameCensoringToggle.Interactable = true;
            
            _currentLevel = beatmapLevel;
            NotifyPropertyChanged();
        }
        
        public void Initialize()
        {
            _levelCollectionViewController.didSelectLevelEvent += onSongSelected;
            _levelCollectionViewController.didSelectHeaderEvent += onHeaderSelected;
            
            GameplaySetup.Instance.AddTab("SongCensor", "ThisSongSucks.UI.BSML.CensorListView.bsml", this);
        }

        public void Dispose()
        {
            _levelCollectionViewController.didSelectLevelEvent -= onSongSelected;
            _levelCollectionViewController.didSelectHeaderEvent -= onHeaderSelected;
            
            GameplaySetup.Instance.RemoveTab("SongCensor");
        }

        private void onHeaderSelected(LevelCollectionViewController _)
        {
            _enableSongCensoringToggle.Interactable = false;
            _enableCoverArtCensoringToggle.Interactable = false;
            _enableSongNameCensoringToggle.Interactable = false;
            
            _currentLevel = null;
        }
    }
}