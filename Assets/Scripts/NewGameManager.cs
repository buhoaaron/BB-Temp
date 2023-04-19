﻿using UnityEngine;
using AudioSystem;
using Barnabus.SceneManagement;
using Barnabus.Card;
using Barnabus.SceneTransitions;
using System.Collections;
using Barnabus.Network;
using Barnabus;

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
    public MainManager MainManager { get; private set; }
    public PlayerDataManager PlayerDataManager => playerDataManager;
    public NetworkManager NetworkManager => networkManager;

    #region COMMON_MANAGER
    private SceneStateController sceneStateController;
    private BarnabusAudioManager audioeManager;
    private BarnabusCardManager barnabusCardManager;
    private SceneTransitionsManager sceneTransitionsManager;
    private JsonManager jsonManager;
    private PlayerDataManager playerDataManager;
    private NetworkManager networkManager;
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
        playerDataManager.SystemUpdate();
    }

    private void Init()
    {
        instance = this;

        sceneTransitionsManager = GetComponentInChildren<SceneTransitionsManager>();
        sceneTransitionsManager.Init();

        barnabusCardManager = new BarnabusCardManager(this);
        barnabusCardManager.Init();

        sceneStateController = new SceneStateController(this, sceneTransitionsManager);
        sceneStateController.Init();

        jsonManager = GetComponentInChildren<JsonManager>();
        jsonManager.Init();

        playerDataManager = new PlayerDataManager(this);
        playerDataManager.Init();

        var audioSourceManager = GetComponentInChildren<AudioSourceManager>();
        audioeManager = new BarnabusAudioManager(this, audioSourceManager);
        audioeManager.Init();

        networkManager = new NetworkManager(this);
        networkManager.Init();
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
        MainManager.AudioManager = audioeManager;
        MainManager.SceneTransitionsManager = sceneTransitionsManager;
        MainManager.PlayerDataManager = playerDataManager;
        MainManager.CardManager = barnabusCardManager;
    }

    /// <summary>
    /// 
    /// </summary>
    public Coroutine CustomStartCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}