using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    [CreateAssetMenu(menuName = "Game/EmotionFace/EmotionFaceAsset")]
    public class EmotionFaceAsset : ScriptableObject
    {
        [Header("Sprite")]
        public Sprite backgroundButtonImage;

        [Space(10)]
        public Sprite leftButtonSelectedSprite;
        public Sprite leftButtonUnselectedSprite;
        [Space(10)]
        public Sprite rightButtonSelectedSprite;
        public Sprite rightButtonUnselectedSprite;
        [Space(10)]
        public Sprite buttonSelectedSprite;
        public Sprite buttonUnselectedSprite;

        [Space(10)]
        [NonReorderable]
        [SerializeField]
        private List<TypeAsset> characterTypes;

        [NonReorderable]
        [SerializeField]
        private List<TypeAsset> itemTypes;

        [NonReorderable]
        public List<Color> colors;

        public int CharacterTypeCount { get { return characterTypes.Count; } }
        public TypeAsset GetCharacterType(int index) { return characterTypes[index]; }
        public TypeAsset GetCharacterType(string typeName)
        {
            for (int i = 0; i < characterTypes.Count; i++)
                if (characterTypes[i].Name == typeName)
                    return characterTypes[i];

            return null;
        }

        public int ItemTypeCount { get { return itemTypes.Count; } }
        public TypeAsset GetItemType(int index) { return itemTypes[index]; }
        public TypeAsset GetItemType(string typeName)
        {
            for (int i = 0; i < itemTypes.Count; i++)
                if (itemTypes[i].Name == typeName)
                    return itemTypes[i];

            return null;
        }

        public bool IsCharacterUnlocked(int id)
        {
            return DataManager.IsCharacterUnlocked(id);
        }
    }
}