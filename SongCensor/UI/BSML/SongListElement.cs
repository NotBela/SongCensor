using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using SongCensor.Configuration;
using TMPro;
using UnityEngine;
using Zenject;

namespace SongCensor.UI.BSML
{
    public class SongListElement
    {
        private static CensorListViewController _censorListViewController;

        private BeatmapLevel ParentBeatmap { get; set; }
        
        [UIValue("songName")] private string _songName = null;
        [UIValue("songArtist")] private string _songArtist = null;

        public SongListElement(BeatmapLevel beatmap)
        {
            _censorListViewController = Resources.FindObjectsOfTypeAll<CensorListViewController>().FirstOrDefault();
            
            this.ParentBeatmap = beatmap;

            this._songArtist = beatmap.songAuthorName;
            this._songName = beatmap.songName;
        }

        [UIAction("editButtonOnClick")]
        private void EditButtonOnClick()
        {
            _censorListViewController.OpenEditMenu(ParentBeatmap);
        }

        [UIAction("removeButtonOnClick")]
        private void removeButtonOnClick()
        {
            _censorListViewController.RemoveEntryFromList(ParentBeatmap.levelID);
        }
    }
}