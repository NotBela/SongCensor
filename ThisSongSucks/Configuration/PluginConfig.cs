using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;
using ThisSongSucks.Censor;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace ThisSongSucks.Configuration
{
    internal class PluginConfig
    {
        public virtual bool Enabled { get; set; } = true;
    }
}