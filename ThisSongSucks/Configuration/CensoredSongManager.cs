using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Reflection;
using IPA.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThisSongSucks.Censor;
using Zenject;
using System.Collections.Specialized;
using IPA.Config.Data;

namespace ThisSongSucks.Configuration
{
    public class CensoredSongManager : IInitializable
    {
        private static readonly string folderPath = Path.Combine(UnityGame.UserDataPath, "SongCensoring");
        private static readonly string path = Path.Combine(folderPath, "CensoredSongManager");
        
        private List<CensoredSong> _censoredSongs = new List<CensoredSong>();
        public IReadOnlyList<CensoredSong> CensoredSongs => _censoredSongs.AsReadOnly();
        
        private List<CensoredSong> readFromDisk() => JArray.Parse(File.ReadAllText(path)).ToObject<List<CensoredSong>>();
        
        public void Initialize()
        {
            Directory.CreateDirectory(folderPath);
            
            if (!File.Exists(path))
                writeToDisk();
            
            _censoredSongs = readFromDisk();
        }

        private void writeToDisk() => File.WriteAllText(path, JsonConvert.SerializeObject(_censoredSongs));
        
        public void AddSong(CensoredSong song)
        {
            _censoredSongs.Add(song);
            writeToDisk();
        }

        public void RemoveSong(CensoredSong song)
        {
            _censoredSongs.Remove(song);
            writeToDisk();
        }
    }
}