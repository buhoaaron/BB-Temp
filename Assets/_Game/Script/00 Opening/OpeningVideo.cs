using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpeningVideo : MonoBehaviour
{
    public PlayableDirector director;

    public GameObject skipText;

    int isSkipAble;

    private void Awake()
    {
        isSkipAble = PlayerPrefs.GetInt("IsOpeningPlayed", 0);

        director.Play();
        if (isSkipAble == 0)
        {
            skipText.SetActive(false);
        }
        else
        {
            skipText.SetActive(true);
        }
    }

    public void VideoEnd()
    {
        director.Pause();
        PlayerPrefs.SetInt("IsOpeningPlayed", 1);
        SceneTransit.LoadSceneAsync("MainScene");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isSkipAble == 0)
                return;

            VideoEnd();
        }
    }

    // void OnEnable()
    // {
    //     director.stopped += OnPlayableDirectorStopped;
    // }

    // void OnPlayableDirectorStopped(PlayableDirector aDirector)
    // {
    //     if (director == aDirector)
    //     {
    //         VideoEnd();
    //     }
    // }


    // void OnDisable()
    // {
    //     director.stopped -= OnPlayableDirectorStopped;
    // }
}
