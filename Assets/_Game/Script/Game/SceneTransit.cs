using System.Collections;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransit : MonoBehaviour
{
    private static SceneTransit instance;
    private static SceneTransit Instance
    {
        get
        {
            if (!instance) instance = Instantiate(Resources.LoadAll<SceneTransit>("Game")[0]).GetComponent<SceneTransit>();
            if (!instance)
            {
                Debug.LogError("SceneTransit not found...");
                instance = null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<SceneTransit>().Length > 1)
        {
            Debug.Log("Destroy SceneTransit");
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject transitView;
    [SerializeField]
    private GameObject loadingView;

    public static void LoadScene(string sceneName)
    {
        Instance.TransitIn();
        Instance.onTransitInAnimationEnd = delegate
        {
            SceneManager.sceneLoaded += Instance.OnSceneLoaded;
            SceneManager.LoadScene(sceneName);
        };
    }

    public static void LoadSceneAsync(string sceneName)
    {
        Instance.TransitIn();
        Instance.onTransitInAnimationEnd = delegate
        {
            SceneManager.sceneLoaded += Instance.OnSceneLoaded;
            Instance.TransitOpen();
            Instance.StartCoroutine(Instance.LoadAsynchronously(sceneName));
        };
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        TransitOut();
        onTransitOutAnimationEnd = TransitClose;
        Time.timeScale = 1;
    }

    private void SetCanvasCamera()
    {
        if (canvas.worldCamera == null) canvas.worldCamera = Camera.main;
    }

    #region Transit
    [Header("Transit")]
    [SerializeField]
    private Animator transitAnimator;

    private Action onTransitInAnimationEnd;
    private Action onTransitOutAnimationEnd;

    private void TransitIn()
    {
        SetCanvasCamera();

        transitView.SetActive(true);
        Instance.transitAnimator.Play("TransitIn", 0, 0);
    }

    private void TransitOut()
    {
        SetCanvasCamera();

        transitView.SetActive(true);
        Instance.transitAnimator.Play("TransitOut", 0, 0);
    }

    private void TransitOpen()
    {
        SetCanvasCamera();

        transitView.SetActive(true);
        Instance.transitAnimator.Play("TransitOut", 0, 1000);
    }

    private void TransitClose()
    {
        transitView.SetActive(false);
    }

    public void OnTransitInAnimationEnd()
    {
        onTransitInAnimationEnd?.Invoke();
        onTransitInAnimationEnd = null;
    }

    public void OnTransitOutAnimationEnd()
    {
        onTransitOutAnimationEnd?.Invoke();
        onTransitOutAnimationEnd = null;
    }
    #endregion

    #region Loading
    [Header("Loading")]
    [SerializeField]
    private float progressBarSpeed;
    [SerializeField]
    private Image progressBarImage;
    [SerializeField]
    private Vector2 loadingImagePositionRange;
    [SerializeField]
    private RectTransform loadingImageRectTransform;
    [SerializeField]
    private Animator loadingImageAnimator;
    [SerializeField]
    private Animator loadingTextAnimator;

    private Vector2 imagePosition;

    private void LoadingInitialize()
    {
        loadingView.SetActive(true);
        progressBarImage.fillAmount = 0;
        loadingImageRectTransform.anchoredPosition = new Vector2(loadingImagePositionRange.x, loadingImageRectTransform.anchoredPosition.y);
        loadingImageAnimator.Play("Loading", 0, 0);
        loadingTextAnimator.Play("Loading", 0, 0);
    }

    private void SetLoadingProgress(float progress)
    {
        progressBarImage.fillAmount = progress;
        imagePosition.x = loadingImagePositionRange.x + (loadingImagePositionRange.y - loadingImagePositionRange.x) * progress;
        loadingImageRectTransform.anchoredPosition = imagePosition;
    }

    private IEnumerator LoadAsynchronously(string sceneName)
    {
        LoadingInitialize();

        AsyncOperation _asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        _asyncOperation.allowSceneActivation = false;

        float progress = 0;
        imagePosition = new Vector2(loadingImagePositionRange.x, loadingImageRectTransform.anchoredPosition.y);
        while (_asyncOperation.progress < 0.9f)
        {
            while (progress < _asyncOperation.progress)
            {
                SetLoadingProgress(progress);
                yield return null;
                progress += Time.unscaledDeltaTime * progressBarSpeed;
            }
            progress = _asyncOperation.progress;
            SetLoadingProgress(progress);
            yield return null;
        }

        while (progress < 1)
        {
            SetLoadingProgress(progress);
            yield return null;
            progress += Time.unscaledDeltaTime * progressBarSpeed;
        }
        SetLoadingProgress(1);

        TransitIn();
        onTransitInAnimationEnd = () => 
        { 
            _asyncOperation.allowSceneActivation = true;
            loadingView.SetActive(false);
        };
    }
    #endregion
}