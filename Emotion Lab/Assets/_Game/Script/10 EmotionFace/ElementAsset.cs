using UnityEngine;

namespace Barnabus.EmotionFace
{
    [System.Serializable]
    public class ElementAsset
    {
        [SerializeField]
        private string elementName;
        [SerializeField]
        private Sprite buttonIcon;
        [SerializeField]
        private Sprite buttonIcon_Gray;
        [SerializeField]
        private Sprite sprite;

        public string Name { get { return elementName; } }
        public Sprite Icon { get { return buttonIcon; } }
        public Sprite LockedIcon { get { return buttonIcon_Gray; } }
        public Sprite Sprite { get { return sprite; } }
    }
}