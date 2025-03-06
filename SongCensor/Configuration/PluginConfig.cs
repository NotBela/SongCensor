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
        
        [UseConverter(typeof(DictionaryConverter<string>))]
        public virtual Dictionary<string, (bool censorSong, bool censorArt)> CensoredSongs { get; set; } = new Dictionary<string, (bool censorSong, bool censorArt)>();

        public void ChangeCensorDictionaryValue(string key, CensorTypes valueToChange, bool value)
        {
            var keyValuePair = CensoredSongs[key];
            switch (valueToChange)
            {
                case CensorTypes.Song:
                    keyValuePair.censorSong = value;
                    break;
                case CensorTypes.CoverArt:
                    keyValuePair.censorArt = value;
                    break;
            }
            CensoredSongs[key] = keyValuePair;
        }
        
        public enum CensorTypes
        {
            Song,
            CoverArt,
            SongName
        }
    }
}