using IPA.Loader;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.Installers
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
        }
    }
}