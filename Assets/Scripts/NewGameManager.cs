using UnityEngine;
using AudioSystem;
using Barnabus.SceneManagement;
using Barnabus.Card;
using Barnabus.SceneTransitions;
using System.Collections;
using Barnabus.Network;

/// <summary>
/// 新的遊戲管理者
/// </summary>
public class NewGameManager : MonoBehaviour 
{
    public static NewGameManager Instance => instance;
    private static NewGameManager instance;

    #region COMMON_MANAGER
    public BarnabusAudioManager AudioManager { get; private set; } = null;
    public BarnabusCardManager BarnabusCardManager { get; private set; } = null;
    public JsonManager JsonManager { get; private set; } = null;
    public MainManager MainManager { get; private set; } = null;
    public PlayerDataManager PlayerDataManager { get; private set; } = null;
    public NetworkManager NetworkManager { get; private set; } = null;
    public AddressableAssetsManager AddressableAssetsManager { get; private set; } = null;

    private SceneStateController sceneStateController;
    private SceneTransitionsManager sceneTransitionsManager;
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
        PlayerDataManager.SystemUpdate();
    }

    private void Init()
    {
        instance = this;

        sceneTransitionsManager = GetComponentInChildren<SceneTransitionsManager>();
        sceneTransitionsManager.Init();

        BarnabusCardManager = new BarnabusCardManager(this);
        BarnabusCardManager.Init();

        sceneStateController = new SceneStateController(this, sceneTransitionsManager);
        sceneStateController.Init();

        JsonManager = GetComponentInChildren<JsonManager>();
        JsonManager.Init();

        PlayerDataManager = new PlayerDataManager(this);
        PlayerDataManager.Init();

        var audioSourceManager = GetComponentInChildren<AudioSourceManager>();
        AudioManager = new BarnabusAudioManager(this, audioSourceManager);
        AudioManager.Init();

        NetworkManager = GetComponentInChildren<NetworkManager>();
        NetworkManager.Init();

        AddressableAssetsManager = new AddressableAssetsManager(this);
        AddressableAssetsManager.Init();
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
        MainManager.SceneTransitionsManager = sceneTransitionsManager;
        MainManager.PlayerDataManager = PlayerDataManager;
        MainManager.CardManager = BarnabusCardManager;
    }

    /// <summary>
    /// 
    /// </summary>
    public Coroutine CustomStartCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}