using SongCensor.Configuration;
using Zenject;

namespace SongCensor.Censor
{
    public class PauseMenuCoverArtReplacer
    {
        [Inject] private readonly PluginConfig _config = null;
        [Inject] private readonly PauseMenuManager _pauseMenuManager = null;

        public void Initialize()
        {
            _pauseMenuManager._levelBar._songArtworkImageView.sprite = _pauseMenuManager._levelBar._defaultArtworkImage;
        }
        
    }
}