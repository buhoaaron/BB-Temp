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
            var saveData = allAudios.ConvertSaveData();

            string jsonText = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(Config.AudioSettingsFilePath, jsonText);
        }

        public static AllAudios LoadJson()
        {
            TextAsset jsonText = (TextAsset)Resources.Load(Config.AudioSettingsFile);

            if (jsonText == null)
                return new AllAudios();

            List<AudioClipSaveData> datas = JsonConvert.DeserializeObject<List<AudioClipSaveData>>(jsonText.text);
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
