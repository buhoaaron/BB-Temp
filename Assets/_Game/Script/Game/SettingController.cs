using Barnabus.SceneManagement;
using UnityEngine;

public class SettingController : MonoBehaviour
{
    [SerializeField]
    private bool gamePauseable;
    [SerializeField]
    private GameObject settingView;

    [Header("Map")]
    [SerializeField]
    private bool canBackMap;
    [SerializeField]
    private GameObject mapButton;

    [Header("SceneName")]
    [SerializeField]
    private string homeSceneName;
    [SerializeField]
    private string mapSceneName;

    private void Start()
    {
        mapButton.SetActive(canBackMap);
    }

    public void OnClick_OpenSetting()
    {
        if (gamePauseable) Time.timeScale = 0;
        settingView.SetActive(true);
    }

    public void OnClick_CloseSetting()
    {
        if (gamePauseable) Time.timeScale = 1;
        settingView.SetActive(false);
    }

    public void ToHomeScene()
    {
        //SceneTransit.LoadSceneAsync(homeSceneName);

        //FIXED: Use the new scene state machine instead
        NewGameManager.Instance.SetSceneState(SCENE_STATE.LOADING_MAIN);
    }

    public void ToMapScene()
    {
        SceneTransit.LoadSceneAsync(mapSceneName);
    }

    /// <summary>
    /// <新增>呼叫場景狀態機切場景狀態
    /// </summary>
    public void SetSceneState(int sceneState)
    {
        NewGameManager.Instance.SetSceneState((SCENE_STATE)sceneState);
    }
}