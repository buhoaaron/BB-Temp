using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.CharacterAsset
{
    [CreateAssetMenu(menuName = "Game/CharacterAsset/InfoList", fileName = "")]
    public class InfoList : ScriptableObject
    {
        [SerializeField]
        private List<Info> characters;

        public Info this[int index] { get { return characters[index]; } }
        public int Count { get { return characters.Count; } }
        public Info GetInfo(int characterID)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                if (characters[i].id == characterID) return characters[i];
            }
            return null;
        }
    }
}
