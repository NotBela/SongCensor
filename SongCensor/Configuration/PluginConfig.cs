using System.Collections.Generic;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
using IPA.Config.Stores.Converters;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace SongCensor.Configuration
{
    internal class PluginConfig
    {
        public virtual bool Enabled { get; set; } = true;
        
        [UseConverter(typeof(ListConverter<string>))]
        public virtual List<string> censoredSongs { get; set; } = new List<string>();
        
        [UseConverter(typeof(ListConverter<string>))]
        public virtual List<string> censoredCoverArt { get; set; } = new List<string>();
        
        [UseConverter(typeof(ListConverter<string>))]
        public virtual List<string> censoredSongNames { get; set; } = new List<string>();
    }
}