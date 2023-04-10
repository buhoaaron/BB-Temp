using UnityEngine;
using Barnabus;
using System;
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
            IncreasePotionAmount(50);
            Save();
        }
#endif
    }
    public override void Clear()
    {

    }
    #endregion

    public override void Save()
    {
        barnabusManager.Save();
        potionManager.Save();
    }

    #region AUDIO
    public void SetMuteAll(bool isMute)
    {
        var muteState = Convert.ToInt16(isMute); 

        DataManager.IsMuteBGM = muteState;
        DataManager.IsMuteSFX = muteState;
    }
    public bool GetMuteBGM()
    {
        return Convert.ToBoolean(DataManager.IsMuteBGM);
    }
    public bool GetMuteSound()
    {
        return Convert.ToBoolean(DataManager.IsMuteSFX);
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
    
    #region BARNABUS
    public void LoadPlayerBarnabus()
    {
        var allData = GameManager.BarnabusCardManager.GetAllBarnabusBaseData();
        barnabusManager.InitPlayerBarnabusData(allData);
        barnabusManager.Load();
    }
    public void SetUnlockInfo(PlayerBarnabusData barnabusData)
    {
        UnlockBarnabusData = barnabusData;
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
    #endregion

    #region POTIONS

    public void LoadPlayerPotions()
    {
        potionManager.Load();
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
    #endregion
}