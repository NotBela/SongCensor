using SongCensor.AffinityPatches;
using SongCensor.Censor;
using SongCensor.Configuration;
using SongCensor.UI.BSML;
using Zenject;

namespace SongCensor.Installers
{
    public class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsViewController>().FromNewComponentAsViewController().AsSingle();

            if (!Container.Resolve<PluginConfig>().Enabled)
                return;
            
            Container.BindInterfacesAndSelfTo<SongPreviewMuterPatch>().AsSingle();
            Container.BindInterfacesAndSelfTo<CensorListViewController>().FromNewComponentAsViewController().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBarSongCoverArtPatch>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelListTableCellPatch>().AsSingle();
        }
    }
}