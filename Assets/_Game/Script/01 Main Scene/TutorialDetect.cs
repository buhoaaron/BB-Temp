using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Barnabus;

public class TutorialDetect : MonoBehaviour
{
    public PlayableDirector director;
    public Image angerImage;
    public Sprite angerSprite;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("IsTutorialPlayed", 0) == 0)
            director.Play();
        else
            gameObject.SetActive(false);
    }

    public void Replay()
    {
        angerImage.sprite = angerSprite;
        gameObject.SetActive(true);
        director.Play();
    }

    void OnEnable()
    {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector && PlayerPrefs.GetInt("IsTutorialPlayed", 0) == 0)
        {
            PlayerPrefs.SetInt("IsTutorialPlayed", 1);
            DataManager.LoadCharacterData();
            DataManager.Characters.SetCharacter(new CharacterData(1, true));
            DataManager.SaveCharacterData();
            DataManager.LastHatchTime = DataManager.GamePassedTime;
        }
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
