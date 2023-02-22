using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace AudioSystem
{
    public class SettingsWindow : EditorWindow
    {
        private AllAudios allAudios = null;

        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem("Window/AudioSystem/Settings", false, 55)]

        private static void Init()
        {
            SettingsWindow window = (SettingsWindow)GetWindow(typeof(SettingsWindow));
            window.titleContent = new GUIContent("AudioSetting");
            window.minSize = new Vector2(550, 550);
            window.Show();
        }

        void OnEnable()
        {
            allAudios = JsonLoader.LoadJson();
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(position.width), GUILayout.Height(position.height - 135));

            EditorGUILayout.LabelField("Game Audios:", EditorStyles.boldLabel);

            for (int i = 0; i < allAudios.clips.Count; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i + ". " + allAudios.clips[i].Name, GUILayout.Width(100));
                allAudios.clips[i].Name = EditorGUILayout.TextField(allAudios.clips[i].Name);
                allAudios.clips[i].Clip = (AudioClip)EditorGUILayout.ObjectField(allAudios.clips[i].Clip, typeof(AudioClip), true);

                if (allAudios.clips[i].Clip != null)
                {
                    long file;
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(allAudios.clips[i].Clip, out allAudios.clips[i].GUID, out file);
                }
                if (GUILayout.Button("Remove", GUILayout.Width(70)))
                {
                    allAudios.clips.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }

            GUILayout.EndScrollView();

            if (GUILayout.Button("Add AudioClip"))
            {
                allAudios.clips.Add(new AudioClipData());
                scrollPosition.y = Mathf.Infinity;
            }

            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Generate/Update AudioSources To Scene"))
            {
                SaveSettings();
                GenerateOrUpdate();
            }
        }

        private void SaveSettings()
        {
            if (allAudios == null) return;

            JsonLoader.SaveJson(allAudios);

            CreateAudioEnumFile();
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 建立Audio列舉檔案
        /// </summary>
        private void CreateAudioEnumFile()
        {
            if (CheckForDuplicates() == true)
                return;

            string text =
            "public enum AUDIO_NAME\n" +
            "{\n";

            foreach (var clip in allAudios.clips)
            {
                text += "\t" + clip.Name + ",\n";
            }

            text += "}";
            File.WriteAllText(Config.AudioEnumsFilePath, text);
            AssetDatabase.Refresh();
        }
        /// <summary>
        /// 檢查是否有重覆的AUDIO NAME
        /// </summary>
        private bool CheckForDuplicates()
        {
            bool duplicateFound = false;

            for (int i = 0; i < allAudios.clips.Count - 1; i++)
            {
                for (int j = i + 1; j < allAudios.clips.Count; j++)
                {
                    if (allAudios.clips[i].Name == allAudios.clips[j].Name)
                    {
                        duplicateFound = true;
                        Debug.LogError("Duplicate id found: " + allAudios.clips[i].Name + " in positions " + i + ", " + j);
                    }
                }
            }

            return duplicateFound;
        }
        /// <summary>
        /// 創建或更新AudioSource到當前場景上
        /// </summary>
        private void GenerateOrUpdate()
        {
            var parent = GameObject.Find(Config.AudioSourcesDefaultName);

            if (parent == null)
                parent = new GameObject(Config.AudioSourcesDefaultName);

            //取得已存在的AudioSources
            var listAudioSourcesExisted = new List<AudioSource>(parent.GetComponentsInChildren<AudioSource>());

            var listAudioSourcesExistedName = new List<string>(listAudioSourcesExisted.ConvertAll<string>(source => source.name));
            var listSettingAudioName = new List<string>(allAudios.clips.ConvertAll<string>(clipData => clipData.Name));
            //建立比對用的列表
            var createList = listSettingAudioName.Except(listAudioSourcesExistedName).ToList();
            var removeList = listAudioSourcesExistedName.Except(listSettingAudioName).ToList();

            //刪除
            var realRemoveList = new List<AudioSource>();
            foreach (string removeName in removeList)
            {
                realRemoveList.Add(listAudioSourcesExisted.Find((x) => x.name == removeName));
            }
            for (int i = 0; i < realRemoveList.Count; i++)
            {
                DestroyImmediate(realRemoveList[i].gameObject);
            }
            //新增
            foreach (string createName in createList)
            {
                var audioClipData = allAudios.clips.Find((x) => x.Name == createName);

                var audioSourceObj = new GameObject(audioClipData.Name);
                audioSourceObj.transform.SetParent(parent.transform);
                //加入AudioSource組件
                var audioSource = audioSourceObj.AddComponent<AudioSource>();
                audioSource.clip = audioClipData.Clip;
                audioSource.playOnAwake = false;
            }
            //更新
            foreach (var clip in allAudios.clips)
            {
                foreach (var audioSourcesExisted in listAudioSourcesExisted)
                {
                    if (clip.Name == audioSourcesExisted.name)
                    {
                        audioSourcesExisted.clip = clip.Clip;
                    }
                }
            }
        }
    }
}
