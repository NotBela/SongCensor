using System;
using IPA.Logging;
using UnityEngine;
using Zenject;

namespace SongCensor.Censor
{
    public class CoverArtBlur : IInitializable, IDisposable
    {
        [Inject] private readonly LevelCollectionViewController _levelCollectionViewController = null;
        [Inject] private readonly StandardLevelDetailViewController _levelDetailViewController = null;

        public void Initialize()
        {
            _levelCollectionViewController.didSelectLevelEvent += levelSelectedEvent;
        }

        private void levelSelectedEvent(LevelCollectionViewController _, BeatmapLevel level)
        {
            if (_levelDetailViewController._standardLevelDetailView._levelBar._songArtworkImageView.sprite == null)
                return;

            var blurredSprite = Sprite.Create(
                Blur(_levelDetailViewController._standardLevelDetailView._levelBar._songArtworkImageView.sprite.texture, 5),
                _levelDetailViewController._standardLevelDetailView._levelBar._songArtworkImageView.sprite.rect,
                _levelDetailViewController._standardLevelDetailView._levelBar._songArtworkImageView.sprite.pivot
            );


            _levelDetailViewController._standardLevelDetailView._levelBar._songArtworkImageView.overrideSprite =
                blurredSprite;

        }

        private Texture2D Blur(Texture2D tex, int amount)
        {
            // TODO: implement this
            return tex;
        }

        public void Dispose()
        {
            _levelCollectionViewController.didSelectLevelEvent -= levelSelectedEvent;
        }
    }
}