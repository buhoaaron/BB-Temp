using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    public static class DataManager
    {
        public static string PlayerID { get; private set; }

        private static string keyCharacters = "Characters";
        private static string keyPotions = "Potions";

        public static void SetPlayerID()
        {
            PlayerID = PlayerPrefs.GetString("PlayerID", "null");
            if (PlayerID == "null") PlayerID = GeneratePlayerID();
            PlayerPrefs.SetString("PlayerID", PlayerID);

            Debug.Log("PlayerID: " + PlayerID);
        }

        private static Potions potions = new Potions();
        public static Potions Potions { get { return potions; } private set { potions = value; } }
        public static void LoadPotions() { Potions = new Potions(PlayerPrefs.GetString(keyPotions, "")); }
        public static void SavePotions() { PlayerPrefs.SetString(keyPotions, Potions.ToJson()); }

        public static bool IsLocalPotionData()
        {
            return PlayerPrefs.HasKey(keyPotions);
        }

        public static void DeleteLocalPotionData()
        {
            PlayerPrefs.DeleteKey(keyPotions);
        }

        private static CharacterDataList characters = new CharacterDataList();
        public static CharacterDataList Characters { get { return characters; } private set { characters = value; } }
        public static void LoadCharacterData() { Characters = new CharacterDataList(PlayerPrefs.GetString(keyCharacters, "")); }
        public static void SaveCharacterData() { PlayerPrefs.SetString(keyCharacters, Characters.ToJson()); }
        public static bool IsCharacterUnlocked(int id) { return Characters[id] != null && Characters[id].isUnlocked; }

        public static bool IsLocalCharacterData() 
        {
            return PlayerPrefs.HasKey(keyCharacters);
        }

        public static void DeleteLocalCharacterData()
        {
            PlayerPrefs.DeleteKey(keyCharacters);
        }

        public static int GamePassedTime { get; set; }
        public static void LoadGamePassedTime() { GamePassedTime = PlayerPrefs.GetInt("GamePassedTime", 0); }
        public static void SaveGamePassedTime() { PlayerPrefs.SetInt("GamePassedTime", GamePassedTime); }

        public static int LastHatchTime { get { return PlayerPrefs.GetInt("LastHatchTime", 0); } set { PlayerPrefs.SetInt("LastHatchTime", value); } }
        public static int MoodQuestLevel { get { return PlayerPrefs.GetInt("MoodQuestLevel", 0); } set { PlayerPrefs.SetInt("MoodQuestLevel", value); } }
        public static int ShapeBreathingLevel { get { return PlayerPrefs.GetInt("ShapeBreathingLevel", 0); } set { PlayerPrefs.SetInt("ShapeBreathingLevel", value); } }
        public static int PropBreathingLevel { get { return PlayerPrefs.GetInt("PropBreathingLevel", 0); } set { PlayerPrefs.SetInt("PropBreathingLevel", value); } }
        public static int SoftTutorialState { get { return PlayerPrefs.GetInt("SoftTutorialState", 0); } set { PlayerPrefs.SetInt("SoftTutorialState", value); } }

        public static int IsMuteBGM { get { return PlayerPrefs.GetInt("IsMuteBGM", 0); } set { PlayerPrefs.SetInt("IsMuteBGM", value); } }
        public static int IsMuteSFX { get { return PlayerPrefs.GetInt("IsMuteSFX", 0); } set { PlayerPrefs.SetInt("IsMuteSFX", value); } }

        public static int DotGameLevel { get { return PlayerPrefs.GetInt("DotGameLevel", 0); } set { PlayerPrefs.SetInt("DotGameLevel", value); } }

        private static string GeneratePlayerID()
        {
            string playerID = "";
#if UNITY_EDITOR
            playerID += "EDT";
#elif UNITY_ANDROID
            playerID += "AND";
#elif UNITY_IOS
            playerID += "IOS";
#else
            playerID += "OTH";
#endif
            playerID += System.DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            for (int i = 0; i < 13; i++) playerID += (char)Random.Range('A', 'Z' + 1);

            return playerID;
        }
    }
}