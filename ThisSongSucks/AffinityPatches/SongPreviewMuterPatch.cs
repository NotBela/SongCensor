using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using HarmonyLib;
using SiraUtil.Affinity;
using ThisSongSucks.Censor;
using ThisSongSucks.Configuration;
using Zenject;

namespace ThisSongSucks.AffinityPatches
{
    public class SongPreviewMuterPatch : IAffinity
    {
        [Inject] private readonly PluginConfig _config = null;
        
        [AffinityPrefix]
        [AffinityPatch(typeof(LevelCollectionViewController), "SongPlayerCrossfadeToLevelAsync")]
        private bool PrefixPatch()
        {
            return true;
        }
    }
}