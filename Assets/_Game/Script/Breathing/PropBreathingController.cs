using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Spine.Unity;
using Barnabus;

public class PropBreathingController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    private GameObject levelSelector;
    [SerializeField]
    private GameObject breathingObjects;
    [SerializeField]
    private SpriteRenderer characterSR;
    [SerializeField]
    private SpriteRenderer characterBG;
    [SerializeField]
    private SkeletonAnimation characterSkeleton;

    [Header("Levels Selector")]
    [SerializeField] private GameObject confirmBtn;
    [SerializeField] private PlayableDirector mainTimeline;
    [SerializeField] private Transform levelContainer;
    [SerializeField] private ShapeItem shapeItemPref;


    [Header("Game Levels")]
    [SerializeField] private List<SkeletonDataAsset> levelSkeletonAsset;
    [SerializeField] private List<string> levelAnimationName;
    [SerializeField] private List<Material> levelMaterial;
    [SerializeField] private List<Sprite> levelSprite;
    [SerializeField] private List<Sprite> levelBackground;

    private ShapeItem tempItem;
    private int currentLevel;

    private void Awake()
    {
        levelSelector.SetActive(true);
        GenerateLevels();
    }

    public void Exit()
    {
        levelSelector.SetActive(false);
        SceneTransit.LoadSceneAsync("MainScene");
    }

    public void Replay()
    {
        SceneTransit.LoadSceneAsync("PropBreathing");
    }

    public void Next()
    {
        SceneTransit.LoadSceneAsync("PropBreathing");
    }

    public void Confirm()
    {
        levelSelector.SetActive(false);
        StartCoroutine(GameStartCoroutine());
    }

    public void GameOver()
    {
        breathingObjects.SetActive(false);

        if (DataManager.PropBreathingLevel == currentLevel)
            DataManager.PropBreathingLevel = DataManager.PropBreathingLevel + 1;

        ShowAward();
    }


    public void GenerateLevels()
    {
        int counts = 0;
        DataManager.LoadCharacterData();

        foreach (SkeletonDataAsset level in levelSkeletonAsset)
        {
            ShapeItem item = Instantiate(shapeItemPref, levelContainer);
            item.id = counts;
            if (DataManager.PropBreathingLevel < counts || DataManager.IsCharacterUnlocked(counts + 1) == false)
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
            item.outline.color = new Color(1, 1, 1, 0);
            item.levelTxt.text += ++counts;

            // if (DataManager.PropBreathingLevel < counts)
            // {
            //     return;
            // }
        }
    }

    public void ChangeLevelCallback(ShapeItem item)
    {
        confirmBtn.SetActive(true);

        if (tempItem != null)
            tempItem.outline.color = new Color(1, 1, 1, 0);

        tempItem = item;

        item.outline.color = new Color(1, 1, 1, 1);
        currentLevel = item.id;
    }

    IEnumerator GameStartCoroutine()
    {
        yield return StartCoroutine(SettingGame());
        mainTimeline.Play();
    }

    IEnumerator SettingGame()
    {
        int id = currentLevel;
        characterSR.sprite = levelSprite[id];
        characterBG.sprite = levelBackground[id];

        characterSkeleton.skeletonDataAsset = levelSkeletonAsset[id];
        characterSkeleton.Initialize(true);
        characterSkeleton.AnimationState.SetAnimation(0, levelAnimationName[id], true);

        Material mat = levelMaterial[id];
        mat.SetFloat("_GrayPhase", 0);

        yield return null;
        breathingObjects.SetActive(true);
    }

    private void ShowAward()
    {
        AwardController.SetPotionSprite(Game.GameSettings.BreathingPotionType);
        AwardController.ShowAward(3, 3, () => SceneTransit.LoadSceneAsync("MainScene"),
                                       () => SceneTransit.LoadSceneAsync("PropBreathing"),
                                       () => SceneTransit.LoadSceneAsync("PropBreathing"));

        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(Game.GameSettings.BreathingPotionType, 3);
        DataManager.SavePotions();
    }
}
