using System;
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
        
        [UseConverter(typeof(DictionaryConverter<string>))]
        public virtual Dictionary<string, (bool censorSong, bool censorArt, bool censorSongName)> CensoredSongs { get; set; } = new Dictionary<string, (bool censorSong, bool censorArt, bool censorSongName)>();
    }
}