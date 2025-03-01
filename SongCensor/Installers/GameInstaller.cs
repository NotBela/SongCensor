using SongCensor.Censor;
using Zenject;

namespace SongCensor.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SongMuter>().AsSingle();
        }
    }
}