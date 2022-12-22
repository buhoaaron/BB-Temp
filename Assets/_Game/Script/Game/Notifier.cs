using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Notifier : MonoBehaviour
{
    private static Notifier instance;
    private static Notifier Instance
    {
        get
        {
            if (!instance) instance = Instantiate(Resources.LoadAll<Notifier>("Notifier")[0]).GetComponent<Notifier>();
            if (!instance)
            {
                Debug.LogError("Notifier not found...");
                instance = null;
            }
            return instance;
        }
    }

    [SerializeField]
    private Canvas canvas;

    #region Confirm
    [Header("Confirm")]
    [SerializeField]
    private GameObject confirmView;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI message;

    private Action confirmCallback;
    private Action cancleCallback;

    private void Awake()
    {
        SetCanvasCamera();
        confirmView.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void SetCanvasCamera()
    {
        if(canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.main;
            canvas.sortingLayerName = "UI";
        }
    }

    public static void ShowConfirmView(string title, string message, Action confirmCallback, Action cancleCallback = null)
    {
        Instance.SetCanvasCamera();
        Instance.title.text = title;
        Instance.message.text = message;
        Instance.confirmCallback = confirmCallback;
        Instance.cancleCallback = cancleCallback;
        Instance.confirmView.SetActive(true);
    }

    public void OnClick_Confirm()
    {
        confirmCallback?.Invoke();
        confirmView.SetActive(false);
    }

    public void OnClick_Cancel()
    {
        cancleCallback?.Invoke();
        confirmView.SetActive(false);
    }
    #endregion

    #region Alert
    [Header("Alert")]
    [SerializeField]
    private float duration = 2f;
    [SerializeField]
    private Image alert;
    [SerializeField]
    private TextMeshProUGUI alertMessage;
    [SerializeField]
    private ContentSizeFitter alertContentFitter;

    public static void ShowAlert(string message)
    {
        Instance.SetCanvasCamera();
        Instance.StopAllCoroutines();
        Instance.alert.color = new Color(Instance.alert.color.r, Instance.alert.color.g, Instance.alert.color.b, 1);
        Instance.alertMessage.color = new Color(Instance.alertMessage.color.r, Instance.alertMessage.color.g, Instance.alertMessage.color.b, 1);
        Instance.alertMessage.text = message;
        Instance.alertContentFitter.SetLayoutHorizontal();
        Instance.alertContentFitter.SetLayoutVertical();
        Instance.alert.gameObject.SetActive(true);
        Instance.StartCoroutine(Instance.AlertFadeOut());
    }

    private IEnumerator AlertFadeOut()
    {
        WaitForSeconds interval = new WaitForSeconds(duration / 100f);
        Color color = new Color(0, 0, 0, 0.01f);

        for (int i = 0; i < 100; i++)
        {
            alert.color -= color;
            alertMessage.color -= color;
            yield return interval;
        }

        alert.gameObject.SetActive(false);
    }
    #endregion
}