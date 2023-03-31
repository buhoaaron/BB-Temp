using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Barnabus.SceneManagement;

public class AwardController : MonoBehaviour
{
    private static AwardController instance;
    private static AwardController Instance
    {
        get
        {
            if (!instance) instance = Instantiate(Resources.LoadAll<AwardController>("Game")[0]).GetComponent<AwardController>();
            if (!instance)
            {
                Debug.LogError("AwardController not found...");
                instance = null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<AwardController>().Length > 1)
        {
            Debug.Log("Destroy AwardController");
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private List<GameObject> fillStars;

    [Header("Button Images")]
    [SerializeField]
    private Image homeButton;
    [SerializeField]
    private Image replayButton;
    [SerializeField]
    private Image nextLevelButton;
    [SerializeField]
    private Image mapButton;

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI potionValueText;
    [SerializeField]
    private float textAnimationDuration;

    [Header("Potion Image")]
    [SerializeField]
    private Image potionImage;
    [SerializeField]
    private Sprite redPotion;
    [SerializeField]
    private Sprite yellowPotion;
    [SerializeField]
    private Sprite greenPotion;
    [SerializeField]
    private Sprite bluePotion;

    private Action onClick_Menu;
    private Action onClick_Replay;
    private Action onClick_NextLevel;
    private Action onClick_Map;

    public static void ShowAward(int starCount, int potionValue,
                                 Action clickMenuButtonCallback, Action clickReplayButtonCallback,
                                 Action clickNextLevelButtonCallback, Action clickMapButtonCallback = null)
    {
        Instance.StopAllCoroutines();
        Instance.SetCanvasCamera();
        Instance.canvas.gameObject.SetActive(true);

        Instance.onClick_Menu = clickMenuButtonCallback;
        Instance.onClick_Replay = clickReplayButtonCallback;
        Instance.onClick_NextLevel = clickNextLevelButtonCallback;
        Instance.onClick_Map = clickMapButtonCallback;
        Instance.mapButton.gameObject.SetActive(clickMapButtonCallback != null);

        for (int i = 0; i < Instance.fillStars.Count; i++) Instance.fillStars[i].SetActive(i < starCount);
        Instance.animator.Play("Appear", 0, 0);

        Instance.StartCoroutine(Instance.PotionValueTextAnimation(potionValue));
        SetNextLevelButtonActice(clickNextLevelButtonCallback != null);
    }

    public static void SetPotionSprite(PotionType potionType)
    {
        Instance.potionImage.sprite = potionType switch
        {
            PotionType.Red => Instance.redPotion,
            PotionType.Yellow => Instance.yellowPotion,
            PotionType.Green => Instance.greenPotion,
            PotionType.Blue => Instance.bluePotion,
            _ => Instance.redPotion,
        };
    }

    public static void SetHomeButtonActice(bool active) => Instance.homeButton.gameObject.SetActive(active);
    public static void SetReplayButtonActice(bool active) => Instance.replayButton.gameObject.SetActive(active);
    public static void SetNextLevelButtonActice(bool active) => Instance.nextLevelButton.gameObject.SetActive(active);

    public void SetCanvasCamera()
    {
        if (canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }

    public void OnClick_Menu()
    {
        StopAllCoroutines();
        
        canvas.gameObject.SetActive(false);

        //FIXED: Use the new scene state machine instead
        //onClick_Menu?.Invoke();
        NewGameManager.Instance.SetSceneState(SCENE_STATE.LOADING_MAIN);
    }

    public void OnClick_Replay()
    {
        StopAllCoroutines();
        onClick_Replay?.Invoke();
        canvas.gameObject.SetActive(false);
    }

    public void OnClick_NextLevel()
    {
        StopAllCoroutines();
        onClick_NextLevel?.Invoke();
        canvas.gameObject.SetActive(false);
    }

    public void OnClick_Map()
    {
        StopAllCoroutines();
        onClick_Map?.Invoke();
        canvas.gameObject.SetActive(false);
    }

    private IEnumerator PotionValueTextAnimation(int targetValue)
    {
        float times = textAnimationDuration / 0.02f;
        float valueInterval = (float)targetValue / times;

        float currentValue = 0;
        WaitForSeconds interval = new WaitForSeconds(0.02f);
        while (currentValue < targetValue)
        {
            potionValueText.text = ((int)currentValue).ToString();
            yield return interval;
            currentValue += valueInterval;
        }

        potionValueText.text = targetValue.ToString();
    }
}