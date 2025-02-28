using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using ThisSongSucks.Configuration;
using ThisSongSucks.Installers;
using IPALogger = IPA.Logging.Logger;

namespace ThisSongSucks
{
    [Plugin(RuntimeOptions.DynamicInit),
     NoEnableDisable]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(Zenjector zenjector, IPALogger logger, Config config)
        {
            Instance = this;
            Log = logger;

            zenjector.UseLogger(logger);
            zenjector.UseMetadataBinder<Plugin>();
            
            zenjector.Install<AppInstaller>(Location.App, config.Generated<PluginConfig>()); }
    }
}