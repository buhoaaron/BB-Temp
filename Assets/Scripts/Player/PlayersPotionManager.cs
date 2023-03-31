using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    /// <summary>
    /// 管理玩家擁有的Potion
    /// </summary>
    public class PlayersPotionManager : BaseBarnabusManager
    {
        public int PotionAmount => playerPotionData.Amount;

        public PlayerPotionData playerPotionData = null;

        public PlayersPotionManager(NewGameManager gm) : base(gm)
        {

        }

        #region BASE_API
        public override void Init()
        {
            InitPlayerPotionData();
        }

        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        /// <summary>
        /// 初始化玩家的藥水資料
        /// </summary>
        public void InitPlayerPotionData()
        {
            playerPotionData = new PlayerPotionData();
        }
        /// <summary>
        /// 讀取玩家本地存的藥水資料
        /// </summary>
        private void LoadPlayerPotionData()
        {
            //本地是否有存檔
            var isLocal = DataManager.IsLocalPotionData();

            if (isLocal)
            {
                DataManager.LoadPotions();

                Debug.Log("有Potions本地資料: " + DataManager.Potions.ToJson());

                playerPotionData.Amount = DataManager.Potions[PotionType.Red];
            }
            else
            {
                Debug.Log("無Potions本地資料: " + DataManager.Potions.ToJson());

                DataManager.SavePotions();
            }
        }

        public override void Save()
        {

        }

        public override void Load()
        {
            LoadPlayerPotionData();
        }
    }
}
