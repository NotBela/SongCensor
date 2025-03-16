using SiraUtil.Affinity;
using SongCensor.Censor;
using SongCensor.Configuration;
using SongCore;
using Zenject;

namespace SongCensor.AffinityPatches
{
    public class LevelBarSongCoverArtPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;
        
        [AffinityPostfix]
        [AffinityPatch(typeof(LevelBar), nameof(LevelBar.SetupData))]
        private void PostFix(LevelBar __instance)
        {
            if (!_config.CensoredSongs.ContainsKey(__instance._beatmapLevel.levelID)) return;
            if (!_config.CensoredSongs[__instance._beatmapLevel.levelID].CensorCoverArt) return;
            
            __instance._songArtworkImageView.sprite = Loader.defaultCoverImage;
        }
    }
}