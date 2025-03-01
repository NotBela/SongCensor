using System;
using System.Linq;
using IPA.Logging;
using IPA.Utilities;
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
        [Inject] private readonly SiraLog _siraLog = null;

        [AffinityPrefix]
        [AffinityPatch(typeof(LevelCollectionViewController), nameof(LevelCollectionViewController.SongPlayerCrossfadeToLevel))]
        private bool PrefixPatch(LevelCollectionViewController __instance, BeatmapLevel level)
        {
            if (!_config.Enabled) return true;
            if (!_config.censoredSongs.Contains(level.levelID)) return true;

            __instance._songPreviewPlayer.PauseCurrentChannel();
            return false;
        }
    }
}