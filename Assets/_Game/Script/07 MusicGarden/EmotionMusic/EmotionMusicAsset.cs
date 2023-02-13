using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionMusic
{
    [CreateAssetMenu(menuName = "Game/Emotion Music/Asset")]
    public class EmotionMusicAsset : ScriptableObject
    {
        public Sprite defaultSoundButtonBackground;

        [Space(10)]
        public List<Character> characterList;

        [Header("Character Selector")]
        public List<Sprite> selectedFrames;

        public Character GetCharacterAssetByID(int characterID)
        {
            for(int i = 0; i < characterList.Count; i++)
            {
                if (characterList[i].id == characterID) return characterList[i];
            }
            return null;

            /*
            if (characterID < 0) return null;
            else return characterList[characterID];
            */
        }
    }
}