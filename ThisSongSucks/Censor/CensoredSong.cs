using System;
using System.Linq;
using Newtonsoft.Json;
using SongCore;

namespace ThisSongSucks.Censor
{
    public class CensoredSong : IEquatable<CensoredSong>
    {
        public string BeatmapId { get; private set; }

        [JsonIgnore]
        private BeatmapLevel BeatmapLevel
        {
            get
            {
                if (!Loader.AreSongsLoaded)
                    throw new Exception("Levels are not loaded yet!");
                return Loader.GetLevelById(BeatmapId);
            }
        }
        
        public bool MuteSong { get; set; }
        public bool BlurCoverArt { get; set; }
        public bool CensorName { get; set; }

        public CensoredSong(BeatmapLevel beatmapLevel, bool muteSong = true, bool blurCoverArt = false, bool censorName = false)
        {
            BeatmapId = beatmapLevel.levelID;
            MuteSong = muteSong;
            BlurCoverArt = blurCoverArt;
            CensorName = censorName;
        }

        public string getCensoredSongName()
        {
            if (!Loader.AreSongsLoaded) throw new Exception("getCensoredSongName called too early!");
            
            if (BeatmapLevel == null) throw new Exception("Song not found!");

            return string.Join(" ", BeatmapLevel.songName.Split(' ').Select(i => i.Length <= 2 ? i : $"{i.First()}{'*' * i.Length - 2}{i.Last()}"));
        }

        public bool Equals(CensoredSong other) => BeatmapId == other?.BeatmapId;
    }
}