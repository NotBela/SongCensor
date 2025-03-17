using SongCensor.AffinityPatches;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            if (!Container.Resolve<PluginConfig>().Enabled) return;
            
            Container.BindInterfacesAndSelfTo<GameSongControllerPatch>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBarSongCoverArtPatch>().AsSingle();
        }
    }
}