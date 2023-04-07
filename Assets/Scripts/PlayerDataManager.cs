using UnityEngine;
using Barnabus;
using System.Collections.Generic;

/// <summary>
/// 新版資料管理者
/// </summary>
public class PlayerDataManager : BaseBarnabusManager
{
    #region CACHE_GAME_INFO
    public PlayerBarnabusData UnlockBarnabusData { get; private set; }

    private GameSceneCacheData sceneCacheData = new GameSceneCacheData(MAIN_MENU.MAIN);
    #endregion

    private PlayersPotionManager potionManager;
    private PlayersBarnabusManager barnabusManager;

    public PlayerDataManager(NewGameManager gm) : base(gm)
    {

    }


    #region BASE_API
    public override void Init()
    {
        barnabusManager = new PlayersBarnabusManager(GameManager);
        barnabusManager.Init();

        potionManager = new PlayersPotionManager(GameManager);
        potionManager.Init();
    }
    public override void SystemUpdate()
    {
    #if DEBUG_MODE
        if (Input.GetKeyUp(KeyCode.A))
        {
            NewGameManager.Instance.PlayerDataManager.IncreasePotionAmount(50);
            NewGameManager.Instance.PlayerDataManager.Save();
        }
    #endif
    }
    public override void Clear()
    {

    }
    #endregion

    #region GAME_SCENE_CACHE
    public GameSceneCacheData GetSceneCacheData()
    {
        return sceneCacheData;
    }

    public void SetSceneCacheData(GameSceneCacheData sceneCacheData)
    {
        this.sceneCacheData = sceneCacheData;
    }

    public void ResetSceneCacheData()
    {
        this.sceneCacheData.Reset();
    }
    #endregion

    public override void Save()
    {
        barnabusManager.Save();
        potionManager.Save();
    }

    public void LoadPlayerBarnabus()
    {
        var allData = GameManager.BarnabusCardManager.GetAllBarnabusBaseData();
        barnabusManager.InitPlayerBarnabusData(allData);
        barnabusManager.Load();
    }

    public void LoadPlayerPotions()
    {
        potionManager.Load();
    }

    public void SetUnlockInfo(PlayerBarnabusData barnabusData)
    {
        UnlockBarnabusData = barnabusData;
    }

    public void ReducePotionAmount(int value)
    {
        potionManager.ReducePotionAmount(value);
    }

    public void IncreasePotionAmount(int value)
    {
        potionManager.IncreasePotionAmount(value);
    }

    public int GetPotionAmount()
    {
        return potionManager.PotionAmount;
    }

    /// <summary>
    /// 解鎖角色
    /// </summary>
    public void UnlockCharacter(int charID)
    {
        barnabusManager.SetCharacter(charID);
    }

    public PlayerBarnabusData GetPlayerBarnabusData(int id)
    {
        return barnabusManager.GetPlayerBarnabusData(id);
    }
    public int GetPlayerBarnabusCount()
    {
        return barnabusManager.GetPlayerBarnabusCount();
    }
    public List<PlayerBarnabusData> GetBatch(int batchNo)
    {
        return barnabusManager.GetBatch(batchNo);
    }
}