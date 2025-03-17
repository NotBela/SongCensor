using SiraUtil.Affinity;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.AffinityPatches
{
    public class GameSongControllerPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly GameplayCoreSceneSetupData _gameplayCoreSceneSetupData = null;

        [AffinityPostfix]
        [AffinityPatch(typeof(GameSongController), nameof(GameSongController.StartSong))]
        private void PostFix(GameSongController __instance)
        {
            if (!_config.CensoredSongs.ContainsKey(_gameplayCoreSceneSetupData.beatmapLevel.levelID)) return;
            if (!_config.CensoredSongs[_gameplayCoreSceneSetupData.beatmapLevel.levelID].CensorSong) return;

            __instance._audioTimeSyncController._audioSource.mute = true;
        }
    }
}