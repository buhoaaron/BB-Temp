using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Barnabus;

public class ShapeBreathingController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    private GameObject levelSelector;
    [SerializeField]
    private GameObject breathingObjects;

    [Header("Levels Selector")]
    [SerializeField] private GameObject confirmBtn;
    [SerializeField] private PlayableDirector mainTimeline;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private ShapeItem shapeItemPref;

    [Header("Game Levels")]
    [SerializeField] private List<PlayableDirector> levels;

    private int currentLevel;
    private PlayableDirector selectedLevel;
    private ShapeItem tempItem;

    [Header("Pause Contents")]
    public Image pauseImage;
    public Sprite pauseSprite, resumeSprite;
    public GameObject pausePanel;

    bool isPaused;
    public void DoPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            selectedLevel.Pause();
            pauseImage.sprite = pauseSprite;
            pausePanel.SetActive(true);
        }
        else
        {
            selectedLevel.Resume();
            pauseImage.sprite = resumeSprite;
            pausePanel.SetActive(false);
        }
    }

    private void Awake()
    {
        levelSelector.SetActive(true);
        GenerateLevels();
    }

    public void ExitGame()
    {
        SceneTransit.LoadSceneAsync("MainScene");
    }

    public void GameStart()
    {
        selectedLevel.gameObject.SetActive(true);
        mainTimeline.Stop();
        selectedLevel.Play();
        selectedLevel.stopped += GameOver;
    }

    public void GameOver(PlayableDirector aDirector)
    {
        breathingObjects.SetActive(false);

        if (DataManager.ShapeBreathingLevel == currentLevel)
            DataManager.ShapeBreathingLevel = DataManager.ShapeBreathingLevel + 1;

        ShowAward();
    }

    public void ConfirmLevel()
    {
        levelSelector.SetActive(false);
        mainTimeline.Play();
    }

    public void GenerateLevels()
    {
        int counts = 0;
        foreach (PlayableDirector level in levels)
        {
            ShapeItem item = Instantiate(shapeItemPref, levelContainer);
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

    public void ChangeLevelCallback(ShapeItem item)
    {
        confirmBtn.SetActive(true);

        if (tempItem != null)
            tempItem.outline.color = new Color(1, 1, 1, 0);

        tempItem = item;

        // mainTimeline.playableAsset = clips[item.id];
        selectedLevel = levels[item.id];
        item.outline.color = new Color(1, 1, 1, 1);
        currentLevel = item.id;
    }

    private void ShowAward()
    {
        AwardController.SetPotionSprite(Game.GameSettings.BreathingPotionType);
        AwardController.ShowAward(3, 3, () => SceneTransit.LoadSceneAsync("MainScene"),
                                       () => SceneTransit.LoadSceneAsync("ShapeBreathing"),
                                       () => SceneTransit.LoadSceneAsync("ShapeBreathing"));

        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(Game.GameSettings.BreathingPotionType, 3);
        DataManager.SavePotions();
    }
}
