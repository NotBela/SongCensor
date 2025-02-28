using System.Linq;
using SiraUtil.Affinity;
using SiraUtil.Logging;
using ThisSongSucks.Censor;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.AffinityPatches
{
    public class SongPreviewMuterPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly CensoredSongManager _censoredSongManager = null;
        [Inject] private readonly SongPreviewPlayer _songPreviewPlayer = null;

        [AffinityPrefix]
        [AffinityPatch(typeof(LevelCollectionViewController), "SongPlayerCrossfadeToLevelAsync")]
        private bool PrefixPatch(BeatmapLevel level)
        {
            if (!_config.Enabled) return true;
            if (_censoredSongManager.CensoredSongs.Contains(new CensoredSong(level))) return true;
            
            _songPreviewPlayer.CrossfadeToDefault();
            return false;
        }
    }
}