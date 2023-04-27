using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using TMPro;
using Barnabus;

public class BreathingGame : MonoBehaviour
{
    [Header("Levels Selector")]
    [SerializeField]
    private GameObject levelSelector;

    [SerializeField]
    private Transform levelContainer;

    [SerializeField]
    private BreathingLevels levelPref;

    [SerializeField]
    private GameObject confirmBtn;

    [Header("Game Levels")]
    [SerializeField]
    private List<GameObject> levels;

    [Header("Games")]
    [SerializeField]
    private PlayableDirector mainTimeline;

    [Header("Buttons")]
    [SerializeField]
    private Button ButtonPause;

    [SerializeField]
    private Button ButtonHome;

    [SerializeField]
    private Button ButtonCancelHome;

    [SerializeField]
    private Button ButtonConfirmHome;

    [Header("UI")]
    [SerializeField]
    private Sprite stopPlayImage;

    [SerializeField]
    private Sprite startPlayImage;

    [SerializeField]
    private GameObject resumeGuide;

    [SerializeField]
    private TMP_Text TextGuide;

    [Header("Pause")]
    [SerializeField]
    private GameObject pause;

    [SerializeField]
    private GameObject settings;

    private int currentLevel;
    private BreathingLevels tempItem;

    private bool isPause;

    private void Awake()
    {
        levelSelector.SetActive(true);
        ButtonRegister();
        GenerateLevels();
    }

    private void ButtonRegister()
    {
        ButtonPause.onClick.AddListener(DoPause);
        ButtonHome.onClick.AddListener(ReturnHome);
        ButtonCancelHome.onClick.AddListener(CancelReturnHome);
        ButtonConfirmHome.onClick.AddListener(ConfirmReturnHome);
    }

    #region GAMES
    public void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        GameObject go = Instantiate(levels[currentLevel]);
        yield return null;
        mainTimeline = go.GetComponent<PlayableDirector>();
        mainTimeline.stopped += OnPlayableDirectorStopped;
        yield return null;
        mainTimeline.Play();
    }

    private void OnDestroy()
    {
        if (mainTimeline != null)
        {
            mainTimeline.stopped -= OnPlayableDirectorStopped;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        Invoke("ShowAward", 0.5f);
    }

    /// <summary>
    /// Pause event
    /// </summary>
    public void DoPause()
    {
        if (isPause == false)
        {
            GamePause();
        }
        else
        {
            GameResume();
        }
    }

    /// <summary>
    /// Home event
    /// </summary>
    public void ReturnHome()
    {
        DoPause();
        settings.SetActive(true);
    }

    /// <summary>
    /// Home confirm event
    /// </summary>
    public void ConfirmReturnHome()
    {
        // Call scene transition.
    }

    /// <summary>
    /// Home cancel event
    /// </summary>
    public void CancelReturnHome()
    {
        GameResume();
        settings.SetActive(false);
    }

    public void GamePause()
    {
        if (mainTimeline != null)
            mainTimeline.Pause();
        ButtonPause.GetComponent<Image>().sprite = startPlayImage;
        isPause = true;
    }

    public void GameResume()
    {
        StartCoroutine(ResumeCorotuine());
    }

    private IEnumerator ResumeCorotuine()
    {
        TextGuide.text = "Click To Start";
        resumeGuide.SetActive(true);

        while (true)
        {
            // Detect touch on mobile devices
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Debug.Log("Touch down detected on mobile device");
                break;
            }

            // Detect mouse click on desktop platforms
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse down detected on desktop platform");
                break;
            }
            yield return null;
        }

        int counter = 3;
        while (counter > 0)
        {
            TextGuide.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }

        if (mainTimeline != null)
            mainTimeline.Resume();

        ButtonPause.GetComponent<Image>().sprite = stopPlayImage;
        isPause = false;

        resumeGuide.SetActive(false);
    }
    #endregion

    #region LEVEL SELECTOR
    public void ConfirmLevel()
    {
        levelSelector.SetActive(false);
        GameStart();
    }

    public void GenerateLevels()
    {
        int counts = 0;
        foreach (var level in levels)
        {
            BreathingLevels item = Instantiate(levelPref, levelContainer);
            item.id = counts;
            item.outline.color = new Color(1, 1, 1, 0);
            if (DataManager.ShapeBreathingLevel < counts)
            {
                item.btn.enabled = false;
                Color color;
                ColorUtility.TryParseHtmlString("#9D9D9D", out color);
                item.background.color = color;
            }
            else
            {
                item.onClick = ChangeLevelCallback;
            }
            item.levelTxt.text += ++counts;
        }
    }

    public void ChangeLevelCallback(BreathingLevels item)
    {
        confirmBtn.SetActive(true);

        if (tempItem != null)
            tempItem.outline.color = new Color(1, 1, 1, 0);

        tempItem = item;

        // mainTimeline.playableAsset = clips[item.id];
        currentLevel = item.id;
        item.outline.color = new Color(1, 1, 1, 1);
        currentLevel = item.id;
    }
    #endregion

    #region GAME OVER
    public void GameOver(PlayableDirector aDirector)
    {
        if (DataManager.ShapeBreathingLevel == currentLevel)
            DataManager.ShapeBreathingLevel = DataManager.ShapeBreathingLevel + 1;

        ShowAward();
    }

    private void ShowAward()
    {
        AwardController.SetPotionSprite(Game.GameSettings.BreathingPotionType);
        AwardController.ShowAward(
            3,
            3,
            () => SceneTransit.LoadSceneAsync("MainScene"),
            () => SceneTransit.LoadSceneAsync("BreathingScene"),
            () => SceneTransit.LoadSceneAsync("MainScene")
        );

        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(Game.GameSettings.BreathingPotionType, 3);
        DataManager.SavePotions();
    }
    #endregion
}
