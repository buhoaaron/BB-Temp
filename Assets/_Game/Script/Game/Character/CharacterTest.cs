using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    public class CharacterTest : MonoBehaviour
    {
        public CharacterData data;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                Debug.Log(DataManager.Characters.ToJson());
                Debug.Log(JsonUtility.ToJson(DataManager.Characters));
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                DataManager.Characters.SetCharacter(data);
                DataManager.SaveCharacterData();
                Debug.Log("Save, Count: " + DataManager.Characters.Count);
                Debug.Log(DataManager.Characters.ToJson());
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                DataManager.LoadCharacterData();
                Debug.Log("Load, Count: " + DataManager.Characters.Count);
                Debug.Log(DataManager.Characters.ToJson());
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteKey("Characters");
            }
        }
    }
}