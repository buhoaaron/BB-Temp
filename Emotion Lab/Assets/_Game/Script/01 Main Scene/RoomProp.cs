using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class RoomProp : MonoBehaviour
{
    public int currentStep = 0;

    public GameObject tutorialPage;
    public Image potionImage;
    public Sprite nextSprite, startSprite;
    public VideoPlayer videoPlayer;
    public TMP_Text title, dialogue;
    public string gameName;

    public void Init(VideoClip clip, string name, string description, string _gameName, Sprite potion)
    {
        videoPlayer.clip = clip;
        title.text = name;
        dialogue.text = description;
        currentStep = 0;
        gameName = _gameName;
        potionImage.sprite = potion;
    }

    public void switchPage(int pageNum)
    {
        currentStep += pageNum;
        switch (currentStep)
        {
            case 0:
                SetTutorialPage();
                break;
            // case 1:
            //     SetReviewPage();
            //     break;
            case 1:
                SceneTransit.LoadScene(gameName);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }

    public void SetTutorialPage()
    {
        tutorialPage.SetActive(true);
        // nextBTN.sprite = nextSprite;
    }

    public void ChangeSceneLoad(string name)
    {
        SceneTransit.LoadSceneAsync(name);
    }
    public void ChangeScene(string name)
    {
        SceneTransit.LoadScene(name);
    }
}
