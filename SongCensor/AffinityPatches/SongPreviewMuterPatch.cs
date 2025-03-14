using System;
using System.Linq;
using IPA.Logging;
using IPA.Utilities;
using SiraUtil.Affinity;
using SiraUtil.Logging;
using SongCensor.Censor;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.AffinityPatches
{
    public class SongPreviewMuterPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPrefix]
        [AffinityPatch(typeof(LevelCollectionViewController), nameof(LevelCollectionViewController.SongPlayerCrossfadeToLevel))]
        private bool PrefixPatch(LevelCollectionViewController __instance, BeatmapLevel level)
        {
            if (!_config.CensoredSongs.TryGetValue(level.levelID, out var song)) return true;
            if (!song.CensorSong) return true;

            __instance._songPreviewPlayer.CrossfadeToDefault();
            return false;
        }
    }
}