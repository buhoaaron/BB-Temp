using System;

using UnityEngine;
using UnityEngine.SceneManagement;

using Barnabus.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Obsolete("�гz�L�������A���ӱ����������", true)]
    public void DoChange(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void SetSceneState(int sceneStateIndex)
    {
        NewGameManager.Instance.SetSceneState((SCENE_STATE)sceneStateIndex);
    }
}
