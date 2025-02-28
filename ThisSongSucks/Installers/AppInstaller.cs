using IPA.Loader;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.Installers
{
    internal class AppInstaller : Installer
    {
        private readonly PluginConfig _config;

        public AppInstaller(PluginConfig config)
        {
            _config = config;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(_config);
            Container.BindInterfacesAndSelfTo<CensoredSongManager>().AsSingle();
        }
    }
}