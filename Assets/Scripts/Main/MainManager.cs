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

    private ShelfAssets shelfAssets = null;

    #region BASE_API
    public void Init()
    {
        shelfAssets = transform.Find("ShelfAssets").GetComponent<ShelfAssets>();
        shelfAssets.Init();
    }
    public void SystemUpdate()
    {

    }
    public void Clear()
    {

    }
    #endregion

    public void LoadAsset(UnityAction onComplete)
    {
        shelfAssets.OnLoadCompleted = onComplete;
        shelfAssets.LoadAssets();
    }

    public BarnabusBaseData GetBarnabusBaseData(int id)
    {
        return NewGameManager.Instance.PlayersBarnabusManager.GetBarnabusBaseData(id);
    }

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
}