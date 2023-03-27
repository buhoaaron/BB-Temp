using UnityEngine;
using Barnabus;
using Barnabus.UI;

using Barnabus.Shelf;
using UnityEngine.Events;

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

    public BarnabusAudioManager AudioManager = null;

    private ShelfAssets shelfAssets = null;
    private PrefabPool shelfPrefabPool = null;

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

    public void LoadAsset(UnityAction onComplete)
    {
        shelfAssets.OnLoadCompleted = onComplete;
        shelfAssets.LoadAssets();
    }

    public BarnabusBaseData GetBarnabusBaseData(int id)
    {
        return NewGameManager.Instance.PlayersBarnabusManager.GetBarnabusBaseData(id);
    }

    #region MAIN_COMMON_UI
    public void MaximizeShelf()
    {
        ShelfUI.Maximize();
    }
    public void MinimizeShelf()
    {
        ShelfUI.Minimize();
    }
    public void MaximizeLessons()
    {
        LessonsUI.Maximize();
    }
    public void MinimizeLessons()
    {
        LessonsUI.Minimize();
    }
    public void MaximizeBooks()
    {
        BooksUI.Maximize();
    }
    public void MinimizeBooks()
    {
        BooksUI.Minimize();
    }

    public void MaximizeGameRoom()
    {
        GameRoomUI.Maximize();
    }
    public void MinimizeGameRoom()
    {
        GameRoomUI.Minimize();
    }
    #endregion

    #region SHELF_ASSETS
    public Sprite GetBarnabusSprite(string name)
    {
        return shelfAssets.GetHubBrandBarnabusSprite(name);
    }

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
    #endregion
}