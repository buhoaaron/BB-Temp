using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("End");
        var delay = 0.5f;
        StartCoroutine(LoadScene(delay));
    }

    IEnumerator LoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (PlayerPrefs.GetInt("IsOpeningPlayed", 0) == 0)
            SceneTransit.LoadScene("opening");
        else
            SceneTransit.LoadScene("MainScene");
    }
}
