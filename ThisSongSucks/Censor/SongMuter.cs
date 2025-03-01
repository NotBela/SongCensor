using System;
using IPA.Loader;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.Censor
{
    public class SongMuter : IInitializable, IDisposable
    {
        [Inject] private readonly GameSongController gameSongController = null;
        [Inject] private readonly PluginConfig _config = null;
        
        public void Initialize()
        {
            gameSongController.songDidStartEvent += songStartedEvent;
        }

        private void songStartedEvent()
        {
            if (!_config.censoredSongs.Contains("replace this with level id")) // TODO: do this
                return;
            
            gameSongController._audioTimeSyncController._audioSource.mute = true;
        }

        public void Dispose()
        {
            gameSongController.songDidStartEvent -= songStartedEvent;
        }
    }
}