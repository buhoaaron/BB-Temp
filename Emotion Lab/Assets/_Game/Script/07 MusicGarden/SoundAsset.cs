using UnityEngine;

namespace Barnabus.MusicGarden
{
    [CreateAssetMenu(menuName = "Game/MusicGarden/SoundAsset")]
    public class SoundAsset : ScriptableObject
    {
        public Sprite buttonSprite;
        public Sprite noteSprite;
        public AudioClip audioClip;
    }
}