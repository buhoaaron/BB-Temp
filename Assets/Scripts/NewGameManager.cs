using UnityEngine;
using Barnabus.SceneManagement;
using Barnabus.Card;
using Barnabus.Transitions;

/// <summary>
/// 新的遊戲管理者
/// </summary>
public class NewGameManager : MonoBehaviour 
{
    public static NewGameManager Instance => instance;
    private static NewGameManager instance;

    public AudioSourceManager AudioSourceManager => audioSourceManager;
    public BarnabusCardManager BarnabusCardManager => barnabusCardManager;

    private SceneStateController sceneStateController;
    private AudioSourceManager audioSourceManager;
    private BarnabusCardManager barnabusCardManager;
    private TransitionsManager transitionsManager;

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

        sceneStateController = new SceneStateController();
        sceneStateController.Init();
       
        audioSourceManager = GetComponentInChildren<AudioSourceManager>();

        transitionsManager = GetComponentInChildren<TransitionsManager>();
        transitionsManager.Init();

        barnabusCardManager = new BarnabusCardManager();
        barnabusCardManager.Init();
    }

    /// <summary>
    /// 通知場景狀態機切換狀態
    /// </summary>
    public void SetSceneState(SCENE_STATE state)
    {
        sceneStateController.SetState(state);
    }
    /// <summary>
    /// 讀取角色卡牌資料
    /// </summary>
    public void LoadBarnabusListAsync()
    {
        StartCoroutine(barnabusCardManager.LoadBarnabusListAsync());
    }
}

