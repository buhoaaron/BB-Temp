using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private static DialogController instance;
    private static DialogController Instance
    {
        get
        {
            if (!instance) instance = Instantiate(Resources.LoadAll<DialogController>("Game")[0]).GetComponent<DialogController>();
            if (!instance)
            {
                Debug.LogError("DialogController not found...");
                instance = null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<DialogController>().Length > 1)
        {
            Debug.Log("Destroy DialogController");
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
    }

    public static StringAsset StringAsset { get { return Instance.stringAsset; } }
    [SerializeField]
    private StringAsset stringAsset;

    [Space(10)]
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject nextButton;

    [Header("Dialog")]
    [SerializeField]
    private Image character;
    [SerializeField]
    private Sprite defaultCharacterSprite;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    [SerializeField]
    private float textAnimationDuration;

    private bool isTextPlaying;
    private string text;
    private Action onClick_Next;

    public static void ShowDialog(string text, Action clickNextButtonCallback, Sprite characterSprite = null)
    {
        if (text == "") return;

        Instance.StopAllCoroutines();
        Instance.SetCanvasCamera();
        Instance.canvas.gameObject.SetActive(true);
        Instance.nextButton.SetActive(false);

        Instance.text = text;
        Instance.onClick_Next = clickNextButtonCallback;

        Instance.character.sprite = (characterSprite == null) ? Instance.defaultCharacterSprite : characterSprite;
        Instance.animator.Play("Appear", 0, 0);

        Instance.StartCoroutine(Instance.TextAnimation());
    }

    private void Update()
    {
        if(isTextPlaying && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            isTextPlaying = false;
            dialogText.text = text;
            Instance.nextButton.SetActive(true);
        }
    }

    public void SetCanvasCamera()
    {
        if (canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }

    public void OnClick_Next()
    {
        StopAllCoroutines();
        onClick_Next?.Invoke();
        canvas.gameObject.SetActive(false);
    }

    private IEnumerator TextAnimation()
    {
        float times = textAnimationDuration / 0.02f;
        float valueInterval = (float)text.Length / times;

        float currentValue = 0;
        WaitForSeconds interval = new WaitForSeconds(0.02f);

        isTextPlaying = true;
        while (currentValue < text.Length)
        {
            dialogText.text = text.Substring(0, (int)currentValue);
            yield return interval;
            currentValue += valueInterval;
        }

        isTextPlaying = false;
        dialogText.text = text;
        Instance.nextButton.SetActive(true);
    }
}