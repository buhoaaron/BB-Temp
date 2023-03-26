using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    /// <summary>
    /// 管理玩家擁有的Barnabus
    /// </summary>
    public class PlayersBarnabusManager : BaseBarnabusManager
    {
        private AllBarnabusBaseData playerBarnabusData = null;
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

        }

        public void SetPlayerBarnabusData(AllBarnabusBaseData allBaseData)
        {
            playerBarnabusData = allBaseData.Copy();
        }
        public BarnabusBaseData GetBarnabusBaseData(int id)
        {
            return playerBarnabusData.GetBarnabusBaseData(id);
        }
        /// <summary>
        /// 擁有的Barnabus數目
        /// </summary>
        public int GetPlayerBarnabusCount()
        {
            return playerBarnabusData.FindAll(data => data.AlreadyOwned).Count;
        }
        /// <summary>
        /// 獲得指定組別的Barnabus
        /// </summary>
        public List<BarnabusBaseData> GetBatch(int batchNo)
        {
            return playerBarnabusData.FindAll(data => data.Batch == batchNo);
        }
    }
}
