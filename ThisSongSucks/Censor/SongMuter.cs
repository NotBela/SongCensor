using System;
using IPA.Loader;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.Censor
{
    public class SongMuter : IInitializable, IDisposable
    {
        [Inject] private readonly GameSongController gameSongController = null;
        
        public void Initialize()
        {
            gameSongController.songDidStartEvent += songStartedEvent;
        }

        private void songStartedEvent()
        {
            gameSongController._audioTimeSyncController._audioSource.mute = true;
        }

        public void Dispose()
        {
            gameSongController.songDidStartEvent -= songStartedEvent;
        }
    }
}