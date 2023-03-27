using UnityEngine;
using Barnabus;
using Barnabus.SceneManagement;
using Barnabus.Card;
using Barnabus.SceneTransitions;
using System.Collections;

/// <summary>
/// 新的遊戲管理者
/// </summary>
public class NewGameManager : MonoBehaviour 
{
    public static NewGameManager Instance => instance;
    private static NewGameManager instance;

    public BarnabusAudioManager AudioManager => audioeManager;
    public BarnabusCardManager BarnabusCardManager => barnabusCardManager;
    public JsonManager JsonManager => jsonManager;
    public PlayersBarnabusManager PlayersBarnabusManager => playersBarnabusManager;
    public MainManager MainManager { get; private set; }

    #region COMMON_MANAGER
    private SceneStateController sceneStateController;
    private BarnabusAudioManager audioeManager;
    private BarnabusCardManager barnabusCardManager;
    private SceneTransitionsManager sceneTransitionsManager;
    private JsonManager jsonManager;
    private PlayersBarnabusManager playersBarnabusManager;
    #endregion

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Init();

        sceneStateController.SetState(SCENE_STATE.START);
    }

    private void Update()
    {
        sceneStateController.StateUpdate();
    }

    private void Init()
    {
        instance = this;

        var audioSourceManager = GetComponentInChildren<AudioSystem.AudioSourceManager>();
        audioeManager = new BarnabusAudioManager(audioSourceManager);
        audioeManager.Init();

        sceneTransitionsManager = GetComponentInChildren<SceneTransitionsManager>();
        sceneTransitionsManager.Init();

        barnabusCardManager = new BarnabusCardManager(this);
        barnabusCardManager.Init();

        sceneStateController = new SceneStateController(this, sceneTransitionsManager);
        sceneStateController.Init();

        jsonManager = GetComponentInChildren<JsonManager>();
        jsonManager.Init();

        playersBarnabusManager = new PlayersBarnabusManager(this);
        playersBarnabusManager.Init();
    }

    /// <summary>
    /// 通知場景狀態機切換狀態
    /// </summary>
    public void SetSceneState(SCENE_STATE state)
    {
        sceneStateController.SetState(state);
    }
    /// <summary>
    /// 設定主選單管理
    /// </summary>
    /// <param name="manager"></param>
    public void SetMainManager(MainManager manager)
    {
        MainManager = manager;
        MainManager.AudioManager = AudioManager;
    }

    /// <summary>
    /// 
    /// </summary>
    public Coroutine CustomStartCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}