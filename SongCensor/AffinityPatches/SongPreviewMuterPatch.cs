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
            if (!_config.Enabled) return true;
            if (!_config.CensoredSongs[level.levelID].censorSong) return true;

            __instance._songPreviewPlayer.PauseCurrentChannel();
            return false;
        }
    }
}