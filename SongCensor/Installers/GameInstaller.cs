using System.Runtime.InteropServices;
using SongCensor.AffinityPatches;
using SongCensor.Censor;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            if (!Container.Resolve<PluginConfig>().Enabled) return;
            
            Container.BindInterfacesAndSelfTo<SongMuter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBarSongCoverArtPatch>().AsSingle();
        }
    }
}