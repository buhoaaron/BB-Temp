using UnityEngine;
using Barnabus;
using System.Collections.Generic;

/// <summary>
/// 新版資料管理者
/// </summary>
public class PlayerDataManager : BaseBarnabusManager
{
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
        if (Input.GetKeyUp(KeyCode.D))
        {
            DataManager.DeleteLocalCharacterData();
        }
    #endif
    }
    public override void Clear()
    {

    }
    #endregion

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


    public int GetPotionAmount()
    {
        return potionManager.PotionAmount;
    }

    public BarnabusBaseData GetBarnabusBaseData(int id)
    {
        return barnabusManager.GetBarnabusBaseData(id);
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