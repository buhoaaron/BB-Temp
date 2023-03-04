using System;

using UnityEngine;
using UnityEngine.SceneManagement;

using Barnabus.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Obsolete("請透過場景狀態機來控制場景切換", true)]
    public void DoChange(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void SetSceneState(int sceneStateIndex)
    {
        NewGameManager.Instance.SetSceneState((SCENE_STATE)sceneStateIndex);
    }
}
