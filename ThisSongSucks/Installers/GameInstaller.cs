using ThisSongSucks.Censor;
using Zenject;

namespace ThisSongSucks.Installers
{
    public class GameInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SongMuter>().AsSingle();
        }
    }
}