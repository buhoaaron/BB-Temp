using UnityEngine;
using UnityEngine.Events;
using Barnabus;
using Barnabus.UI;
using Barnabus.Shelf;
using Barnabus.SceneTransitions;
using Barnabus.Card;

/// <summary>
/// 主選單管理者
/// </summary>
public class MainManager : MonoBehaviour, IBaseSystem 
{
    [Header("Set Canvas Main")]
    public MainUI MainUI = null;
    [Header("Set Canvas GameRoom")]
    public GameRoomUI GameRoomUI = null;
    [Header("Set Canvas Shelf")]
    public ShelfUI ShelfUI = null;
    [Header("Set Canvas Library")]
    public BooksUI BooksUI = null;
    [Header("Set Canvas ClassRoom")]
    public LessonsUI LessonsUI = null;
    [Header("Set Canvas NavigationBar")]
    public NavigationBarUI NavigationBarUI = null;
    [Space(20)]
    public SceneTransitionsManager SceneTransitionsManager = null;
    public PlayerDataManager PlayerDataManager = null;
    public BarnabusAudioManager AudioManager = null;
    public BarnabusCardManager CardManager = null;

    private ShelfAssets shelfAssets = null;
    private PrefabPool shelfPrefabPool = null;

    private bool isDoWakeUp = false;
    private bool isDoUnlock = false;
    //使用者目前選中的房間
    private BaseMainCommonUI curretnSelect = null;

    #region BASE_API
    public void Init()
    {
        shelfAssets = transform.Find("ShelfAssets").GetComponent<ShelfAssets>();
        shelfPrefabPool = shelfAssets.GetComponent<PrefabPool>();
    }
    public void SystemUpdate()
    {

    }
    public void Clear()
    {

    }
    #endregion

    public HubInfoUI CreateHubInfoUIAndInit(HUB_STATE state)
    {
        var prefab = GetShelfPrefab("HubInfo");
        var hubInfoUI = ShelfUI.AddChild(prefab).GetComponent<HubInfoUI>();
        hubInfoUI.Init(state);
        //註冊按鈕音效
        AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, hubInfoUI.Buttons);

        return hubInfoUI;
    }

    public void GotoWakeUpState()
    {
        isDoWakeUp = true;
    }
    public bool CheckWakeUp()
    {
        return isDoWakeUp;
    }
    public void GotoUnlockState()
    {
        isDoUnlock = true;
    }
    public bool CheckUnlock()
    {
        return isDoUnlock;
    }

    public int GetPotionAmount()
    {
        return PlayerDataManager.GetPotionAmount();
    }
    public PlayerBarnabusData GetBarnabusBaseData(int id)
    {
        return PlayerDataManager.GetPlayerBarnabusData(id);
    }

    public BarnabusScanScriptable GetBarnabusCard(int characterID)
    {
        return CardManager.GetCard(characterID);
    }

    #region MAIN_COMMON_UI
    public void MaximizeShelf()
    {
        curretnSelect = ShelfUI.Maximize();
    }
    public void MinimizeShelf()
    {
        ShelfUI.Minimize();
    }
    public void MaximizeLessons()
    {
        curretnSelect = LessonsUI.Maximize();
    }
    public void MinimizeLessons()
    {
        LessonsUI.Minimize();
    }
    public void MaximizeBooks()
    {
        curretnSelect = BooksUI.Maximize();
    }
    public void MinimizeBooks()
    {
        BooksUI.Minimize();
    }

    public void MaximizeGameRoom()
    {
        curretnSelect = GameRoomUI.Maximize();
    }
    public void MinimizeGameRoom()
    {
        GameRoomUI.Minimize();
    }
    public void BackHome()
    {
        if (curretnSelect != null)
        {
            curretnSelect.Minimize();
            curretnSelect = null;
        }
    }
    #endregion

    #region SHELF_ASSETS
    public Sprite GetHubBrandBg(COLOR color)
    {
        return shelfAssets.GetHubBrandBg((int)color);
    }

    public Sprite GetHubBrand(COLOR color)
    {
        return shelfAssets.GetHubBrand((int)color);
    }

    public GameObject GetShelfPrefab(string name)
    {
        return shelfPrefabPool.GetPrefab(name);
    }

    public void LoadNavigationBarAsset()
    {
        shelfAssets.LoadNavigationBarAsset();
    }
    #endregion
}