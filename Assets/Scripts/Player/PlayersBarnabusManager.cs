using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    /// <summary>
    /// 管理玩家擁有的Barnabus
    /// </summary>
    public class PlayersBarnabusManager : BaseBarnabusManager
    {
        private AllPlayerBarnabusData allPlayerBarnabusData = null;
        public PlayersBarnabusManager(NewGameManager gm) : base(gm)
        {

        }

        #region BASE_API
        public override void Init()
        {
            
        }

        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        public override void Save()
        {
            
        }

        public override void Load()
        {
            LoadPlayerBarnabusData();
        }
        /// <summary>
        /// 初始化玩家的角色資料
        /// </summary>
        /// <param name="allBaseData">角色初始資料</param>
        public void InitPlayerBarnabusData(AllBarnabusBaseData allBaseData)
        {            
            allPlayerBarnabusData = new AllPlayerBarnabusData();

            foreach (var baseData in allBaseData)
            {
                var playerBarnabusData = new PlayerBarnabusData(baseData);
                allPlayerBarnabusData.Add(playerBarnabusData);
            }
        }
        /// <summary>
        /// 讀取玩家本地存的角色資料
        /// </summary>
        private void LoadPlayerBarnabusData()
        {
            //本地是否有存檔
            var isLocal = DataManager.IsLocalCharacterData();

            if (isLocal)
            {
                DataManager.LoadCharacterData();

                foreach (var barnabusData in allPlayerBarnabusData)
                {
                    var characterID = barnabusData.BaseData.CharacterID;
                    var charData = DataManager.Characters[characterID];

                    if (charData != null)
                    {
                        Debug.Log(string.Format("有charID {0} 本地資料，更新！", characterID));
                        barnabusData.IsUnlocked = charData.isUnlocked;
                    }
                }
            }
            else
            {
                foreach (var barnabusData in allPlayerBarnabusData)
                {
                    if (barnabusData.IsUnlocked)
                    {
                        var characterData = new CharacterData(barnabusData.BaseData.CharacterID, barnabusData.IsUnlocked);
                        DataManager.Characters.SetCharacter(characterData);
                    }
                }

                Debug.Log("本地無玩家角色資料，建立：" + DataManager.Characters.ToJson());

                DataManager.SaveCharacterData();
            }
        }

        public BarnabusBaseData GetBarnabusBaseData(int id)
        {
            return allPlayerBarnabusData.GetBarnabusData(id).BaseData;
        }
        /// <summary>
        /// 擁有的Barnabus數目
        /// </summary>
        public int GetPlayerBarnabusCount()
        {
            return allPlayerBarnabusData.FindAll(data => data.IsUnlocked).Count;
        }
        /// <summary>
        /// 獲得指定組別的Barnabus
        /// </summary>
        public List<PlayerBarnabusData> GetBatch(int batchNo)
        {
            return allPlayerBarnabusData.FindAll(data => data.BaseData.Batch == batchNo);
        }
    }
}
