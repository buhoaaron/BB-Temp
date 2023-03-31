using System.Collections.Generic;
using Barnabus;
using UnityEngine;

namespace HiAndBye.Question
{
    public class QuestionManager : HiAndByeBaseManager
    {
        /// <summary>
        /// 玩家擁有的角色池
        /// </summary>
        private List<PlayerBarnabusData> listBarnabusOwned = null;
        private SetQuestionInfo currentSetQuestionInfo = null;
        /// <summary>
        /// Hi出現機率
        /// </summary>
        private int probabilityHi = 35;
        /// <summary>
        /// Hi保底
        /// </summary>
        private int guaranteedHi = 5;
        /// <summary>
        /// 目前Bye累積數
        /// </summary>
        private int currentByeCount = 0;

        public QuestionManager(HiAndByeGameManager gm) : base(gm)
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
            currentByeCount = 0;
        }
        #endregion

        public void SetBatch(List<PlayerBarnabusData> batch)
        {
            //取出該組內玩家有的角色
            listBarnabusOwned = batch.FindAll(barnabus => barnabus.IsUnlocked);
        }

        public SetQuestionInfo RandomQuestion()
        {
            //隨機選出一隻角色
            int barnabusOwnedCount = listBarnabusOwned.Count;
            int randomIndex = Random.Range(0, barnabusOwnedCount);
            var barnabus = listBarnabusOwned[randomIndex];
            var sprite = gameManager.GetBarnabusSprite(barnabus.BaseData.Name);
            //機率決定是否出Hi
            bool isHi = RandomHi();
            //把選中的角色資料傳給題目設定
            currentSetQuestionInfo = new SetQuestionInfo();
            currentSetQuestionInfo.BarnabusBaseData = barnabus.BaseData;
            currentSetQuestionInfo.BarnabusSprite = sprite;
            
            var asset = gameManager.GetSpineAssets().GetSpineAsset(barnabus.BaseData.Name);
            currentSetQuestionInfo.BarnabusSkeletonDataAsset = asset;

            if (isHi)
            {
                //若是Hi直接設定正確的資料
                currentSetQuestionInfo.BarnabusFace = barnabus.BaseData.Vocab;
                currentSetQuestionInfo.BarnabusVocab = barnabus.BaseData.Vocab;
                currentSetQuestionInfo.BarnabusVoice = barnabus.BaseData.SoundKey;
            }
            else
            {
                //若不是則隨機設定角色資料
                currentSetQuestionInfo.BarnabusFace = listBarnabusOwned[Random.Range(0, barnabusOwnedCount)].BaseData.Vocab;
                currentSetQuestionInfo.BarnabusVocab = listBarnabusOwned[Random.Range(0, barnabusOwnedCount)].BaseData.Vocab;
                currentSetQuestionInfo.BarnabusVoice = listBarnabusOwned[Random.Range(0, barnabusOwnedCount)].BaseData.SoundKey;

                currentByeCount++;
            }

            //判斷該題為Hi或Bye
            currentSetQuestionInfo.QuestionType = CheckHiOrBye(currentSetQuestionInfo);

            return currentSetQuestionInfo;
        }

        private bool RandomHi()
        {
            //保底判斷
            if (currentByeCount >= (guaranteedHi - 1))
            {
                //重置累積
                currentByeCount = 0;
                return true;
            }

            //正常機率
            int randomNumber = Random.Range(0, 100);
            return randomNumber < probabilityHi;
        }

        private QUESTION_TYPE CheckHiOrBye(SetQuestionInfo setQuestionInfo)
        {
            var isSameVocab = setQuestionInfo.BarnabusBaseData.Vocab == setQuestionInfo.BarnabusVocab;
            var isSameVoice = setQuestionInfo.BarnabusBaseData.SoundKey == setQuestionInfo.BarnabusVoice;

            var isHi = isSameVocab && isSameVoice;

            return isHi ? QUESTION_TYPE.HI : QUESTION_TYPE.BYE;
        }

        public SetQuestionInfo GetCurrentQuestionInfo()
        {
            return currentSetQuestionInfo;
        }
    }
}
