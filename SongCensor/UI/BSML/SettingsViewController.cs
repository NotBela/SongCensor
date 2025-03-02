using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.UI.BSML
{
    internal class SettingsViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;

        [UIValue("modEnabled")]
        private bool modEnabled
        {
            get => _config.Enabled;
            set => _config.Enabled = value;
        }
        
        public void Initialize()
        {
            BSMLSettings.Instance.AddSettingsMenu("SongCensor", "SongCensor.UI.BSML.SettingsView.bsml", this);
        }

        public void Dispose()
        {
            BSMLSettings.Instance.RemoveSettingsMenu("SongCensor");
        }
    }
}