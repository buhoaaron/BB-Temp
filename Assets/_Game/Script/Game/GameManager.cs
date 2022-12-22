using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Barnabus
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                CustomAudio.AudioManager.instance.LoadMuteSetting();
            }
        }

        private void Start()
        {
            DataManager.SetPlayerID();
            DataManager.LoadGamePassedTime();
            StartCoroutine(StartTimer());
        }

        private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);
        private IEnumerator StartTimer()
        {
            while (true)
            {
                yield return wait;
                DataManager.GamePassedTime += 1;
                DataManager.SaveGamePassedTime();
            }
        }


        #region Scene Loaded
        private string currentScene;
        public List<string> gameScenes = new List<string>();
        void OnEnable()
        {
            Debug.Log("OnEnable called");
            SceneManager.sceneLoaded += OnSceneLoaded;
            currentScene = SceneManager.GetActiveScene().name;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "MainScene":
                    Debug.Log("Get main sceneeeeeeeee");
                    if (gameScenes.Contains(currentScene))
                    {
                        Debug.Log("GO");
                        Barnabus.MainScene.MainContent.Instance.gameRoom.SetActive(true);
                    }
                    break;
            }
            currentScene = scene.name;
        }


        // called when the game is terminated
        void OnDisable()
        {
            Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endregion
    }
}