using SiraUtil.Affinity;
using SongCensor.Configuration;
using SongCore;
using Zenject;

namespace SongCensor.AffinityPatches
{
    public class LevelListTableCellPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(LevelListTableCell), nameof(LevelListTableCell.SetDataFromLevelAsync))]
        private void PostFix(LevelListTableCell __instance, BeatmapLevel beatmapLevel)
        {
            if (!_config.CensoredSongs.ContainsKey(beatmapLevel.levelID)) return;
            if (!_config.CensoredSongs[beatmapLevel.levelID].CensorCoverArt) return;
            
            __instance._coverImage.sprite = Loader.defaultCoverImage;
        }
    }
}