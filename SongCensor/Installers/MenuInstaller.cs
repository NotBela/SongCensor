using SongCensor.AffinityPatches;
using SongCensor.Censor;
using SongCensor.Configuration;
using SongCensor.UI.Controllers;
using Zenject;

namespace SongCensor.Installers
{
    public class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SongPreviewMuterPatch>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsViewController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<CensorListViewController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<CoverArtBlur>().AsSingle();
        }
    }
}