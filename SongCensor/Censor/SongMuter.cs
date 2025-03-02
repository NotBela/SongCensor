using System;
using IPA.Loader;
using SongCensor.Configuration;
using Zenject;

namespace SongCensor.Censor
{
    public class SongMuter : IInitializable, IDisposable
    {
        [Inject] private readonly GameSongController _gameSongController = null;
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly GameplayCoreSceneSetupData _gameCoreSceneSetupData = null;
        
        public void Initialize()
        {
            _gameSongController.songDidStartEvent += songStartedEvent;
        }

        private void songStartedEvent()
        {
            if (!_config.CensoredSongs[_gameCoreSceneSetupData.beatmapLevel.levelID].censorSong)
                return;
            
            _gameSongController._audioTimeSyncController._audioSource.mute = true;
        }

        public void Dispose()
        {
            _gameSongController.songDidStartEvent -= songStartedEvent;
        }
    }
}