using UnityEngine;

namespace AudioSystem
{
    public static class Config
    {
        public static string AudioSourcesDefaultName = "AudioSourceManager";
        public static string AudioSettingsFile = "AudioSettingsFile";
        public static string AudioSettingsFilePath = Application.dataPath + "/AudioSystem/Resources/AudioSettingsFile.json";
        public static string AudioEnumsFilePath = Application.dataPath + "/AudioSystem/Scripts/AudioEnum.cs";
        public static string AudioManagerFilePath = Application.dataPath + "/AudioSystem/Prefabs/AudioManager.prefab";
    }
}
