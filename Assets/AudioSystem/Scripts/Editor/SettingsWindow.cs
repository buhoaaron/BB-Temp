using UnityEditor;
using UnityEngine;

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
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, 
                GUILayout.Width(position.width), GUILayout.Height(position.height - 160));

            EditorGUILayout.LabelField("Audios:", EditorStyles.boldLabel);

            for (int i = 0; i < allAudios.clips.Count; i++)
            {
                var clipData = allAudios.clips[i];

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i + ". " + clipData.Name, GUILayout.Width(100));

                clipData.Name = EditorGUILayout.TextField(clipData.Name);
                clipData.Clip = (AudioClip)EditorGUILayout.ObjectField(clipData.Clip, typeof(AudioClip), true);

                if (clipData.Clip != null)
                {
                    long file;
                    AssetDatabase.TryGetGUIDAndLocalFileIdentifier(clipData.Clip, out clipData.GUID, out file);
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

            EditorGUILayout.LabelField(Config.AudioSourcesDefaultName, EditorStyles.boldLabel);

            if (GUILayout.Button(string.Format("Create New To Scene")))
            {
                CreateAudioSourceManager();
            }
            if (GUILayout.Button(string.Format("Override To Scene")))
            {
                OverrideAudioSourceManager();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Import From Json"))
            {
                ImportFormJson();
            }

            if (GUILayout.Button("Export to Json"))
            {
                ExportToJson();
            }
        }

        private void SaveSettings()
        {
            if (allAudios == null) return;
            if (CheckForDuplicates() == true)
                return;

            JsonLoader.SaveJson(allAudios);
            AssetDatabase.Refresh();
        }

        private void ImportFormJson()
        {
            var path = EditorUtility.OpenFilePanel("Select .json file", "", "json");
            allAudios = JsonLoader.LoadJsonFile(path);
        }

        private void ExportToJson()
        {
            if (allAudios == null) return;

            var path = EditorUtility.SaveFilePanel(
                "Save AudioSettings as json",
                "",
                "AudioSettingsFile.json",
                "json");

            if (path.Length != 0)
            {
                JsonLoader.SaveJson(allAudios, path);
            }

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

        private void CreateAudioSourceManager()
        {
            var prefab = PrefabUtility.LoadPrefabContents(Config.AudioManagerFilePath);
            var go = GameObject.Instantiate(prefab);
            go.name = Config.AudioSourcesDefaultName;

            AddAudioClipResources(go);
        }

        private void OverrideAudioSourceManager()
        {
            var target = GameObject.FindObjectOfType<AudioSourceManager>();

            if (target == null)
            {
                CreateAudioSourceManager();
                return;
            }

            AddAudioClipResources(target.gameObject);
        }

        private void AddAudioClipResources(GameObject target)
        {
            if (!target.TryGetComponent<AudioClipResources>(out var resources))
                resources = target.AddComponent<AudioClipResources>();

            resources.Clear();

            foreach (var clipData in allAudios.clips)
            {
                var audioClip = new AudioClipPair();
                audioClip.Name = clipData.Name;
                audioClip.Clip = clipData.Clip;

                resources.AddAudioClip(audioClip);
            }
        }
    }
}
