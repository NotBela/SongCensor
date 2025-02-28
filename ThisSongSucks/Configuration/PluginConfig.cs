using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace ThisSongSucks.Configuration
{
    internal class PluginConfig
    {
        public virtual bool Enabled { get; set; } = true;
    }
}