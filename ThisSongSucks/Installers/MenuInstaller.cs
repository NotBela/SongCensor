using ThisSongSucks.AffinityPatches;
using ThisSongSucks.Censor;
using ThisSongSucks.Configuration;
using ThisSongSucks.UI.Controllers;
using Zenject;

namespace ThisSongSucks.Installers
{
    public class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SongPreviewMuterPatch>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsViewController>().FromNewComponentAsViewController().AsSingle();
        }
    }
}