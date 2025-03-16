using System;
using System.Collections.Generic;
using System.Linq;
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
        
        [NonNullable]
        [UseConverter(typeof(DictionaryConverter<MapSettings>))]
        public virtual Dictionary<string, MapSettings> CensoredSongs { get; set; } = new Dictionary<string, MapSettings>()
        {
            {"custom_level_5993203C880D2AD5F4513FCC9B372FD8451E8ED8", new MapSettings(true, true)}
        };
    }

    public class MapSettings
    {
        public bool CensorSong { get; set; }
        public bool CensorCoverArt { get; set; }
        
        public MapSettings(bool censorSong, bool censorCoverArt)
        {
            this.CensorSong = censorSong;
            this.CensorCoverArt = censorCoverArt;
        }
        // bsipa requires a parameterless constructor to be present in order to serialize to a config file
        // do not remove!!!!
        public MapSettings()
        {
            this.CensorSong = true;
            this.CensorCoverArt = false;
        }
    }
}