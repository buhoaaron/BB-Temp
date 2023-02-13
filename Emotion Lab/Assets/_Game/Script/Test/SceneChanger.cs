// using Barnabus.Adventure;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] List<string> sceneNames;

    public void ChangeScene(int num)
    {
        SceneTransit.LoadSceneAsync(sceneNames[num]);
    }
}
