using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BackToMainScene : MonoBehaviour
{
    public static BackToMainScene Instance;

    // public GameObject transis;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainScene");
        }
        if (Input.touchCount > 3)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void DoSceneTransit(string name)
    {
        SceneTransit.LoadSceneAsync(name);
    }
}
