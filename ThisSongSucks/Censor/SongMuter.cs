using System;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.Censor
{
    public class SongMuter : IInitializable, IDisposable
    {
        [Inject] private readonly GameSongController _songController = null;
        
        public void Initialize()
        {
            _songController.songDidStartEvent += onSongStart;
        }

        private void onSongStart()
        {
            _songController.PauseSong();
        }

        public void Dispose()
        {
            _songController.songDidStartEvent -= onSongStart;
        }
    }
}