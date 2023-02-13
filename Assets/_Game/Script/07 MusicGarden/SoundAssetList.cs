using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.MusicGarden
{
    [CreateAssetMenu(menuName = "Game/MusicGarden/SoundAssetList")]
    public class SoundAssetList : ScriptableObject
    {
        public List<SoundAsset> soundAssets;

        public SoundAsset this[int index] { get { return soundAssets[index]; } }
        public int Count { get { return soundAssets.Count; } }
    }
}