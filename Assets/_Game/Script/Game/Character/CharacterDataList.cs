using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    [System.Serializable]
    public class CharacterDataList
    {
        [SerializeField]
        private List<CharacterData> characters;

        public CharacterData this[int id] { get { return characters.Find((character) => character.id == id); } }
        public int Count { get { return characters.Count; } }

        public CharacterDataList()
        {
            characters = new List<CharacterData>();
        }

        public CharacterDataList(string json)
        {
           
            characters = new List<CharacterData>();
            CharacterDataList data = JsonUtility.FromJson<CharacterDataList>(json);
            if (data == null) return;
           

            characters = data.characters;
        }

        public string ToJson() { return JsonUtility.ToJson(this); }

        public void SetCharacter(CharacterData data)
        {

            bool isExist = false;
            for (int i = 0; i < Count; i++)
            {
                if (characters[i].id == data.id)
                {
                    characters[i] = new CharacterData(data);
                    isExist = true;
                    break;
                }
            }
            if (!isExist) characters.Add(new CharacterData(data));
        }
    }
}