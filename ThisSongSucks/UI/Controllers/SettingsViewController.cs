using System;
using BeatSaberMarkupLanguage.Settings;
using BeatSaberMarkupLanguage.ViewControllers;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.UI.Controllers
{
    internal class SettingsViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        [Inject] private readonly PluginConfig _config = null;

        public void Initialize()
        {
            BSMLSettings.Instance.AddSettingsMenu("ThisSongSucks", "ThisSongSucks.UI.BSML.ThisSongSucksView.bsml", this);
        }

        public void Dispose()
        {
            if (BSMLSettings.Instance != null) BSMLSettings.Instance.RemoveSettingsMenu("ThisSongSucks");
        }
    }
}