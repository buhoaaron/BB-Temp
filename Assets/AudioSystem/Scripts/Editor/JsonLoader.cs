using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AudioSystem
{
    public class JsonLoader
    {
        public static void SaveJson(AllAudios allAudios)
        {
            SaveJson(allAudios, Config.AudioSettingsFilePath);
        }

        public static void SaveJson(AllAudios allAudios, string path)
        {
            var saveData = allAudios.ConvertSaveData();

            string jsonText = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(path, jsonText);
        }

        public static AllAudios LoadJson()
        {
            TextAsset jsonText = (TextAsset)Resources.Load(Config.AudioSettingsFile);
            return LoadJsonText(jsonText.text);
        }

        public static AllAudios LoadJsonFile(string path)
        {
            string result = File.ReadAllText(path);
            return LoadJsonText(result);
        }

        private static AllAudios LoadJsonText(string jsonText)
        {
            List<AudioClipSaveData> datas = JsonConvert.DeserializeObject<List<AudioClipSaveData>>(jsonText);
            AllAudios allAudios = new AllAudios();

            //創建AllAudios實例
            foreach (var saveData in datas)
            {
                var clipData = new AudioClipData();
                clipData.Name = saveData.Name;
                clipData.GUID = saveData.GUID;
                var clipAssetPath = AssetDatabase.GUIDToAssetPath(clipData.GUID);
                clipData.Clip = AssetDatabase.LoadAssetAtPath<AudioClip>(clipAssetPath);

                allAudios.clips.Add(clipData);
            }

            return allAudios;
        }
    }
}
