using System;
using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;

namespace ThisSongSucks.UI.Controllers
{
    public class CensorListViewController : BSMLAutomaticViewController, IInitializable, IDisposable
    {
        public void Initialize()
        {
            GameplaySetup.Instance.AddTab("SongCensor", "ThisSongSucks.UI.BSML.CensorListView.bsml", this);
        }

        public void Dispose()
        {
            GameplaySetup.Instance.RemoveTab("SongCensor");
        }
    }
}