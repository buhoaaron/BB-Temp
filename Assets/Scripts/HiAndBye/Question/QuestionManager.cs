using System.Collections.Generic;
using Barnabus;
using UnityEngine;

namespace HiAndBye.Question
{
    public class QuestionManager : HiAndByeBaseManager
    {
        //RANDOM的角色池
        private List<BarnabusBaseData> listBarnabusOwned = null;
        private SetQuestionInfo currentSetQuestionInfo = null;
        /// <summary>
        /// CorrectHi出現機率
        /// </summary>
        private int probabilityCorrectHi = 35;
        /// <summary>
        /// CorrectHi保底
        /// </summary>
        private int guaranteedCorrectHi = 5;
        /// <summary>
        /// 目前CorrectBye累積數
        /// </summary>
        private int currentCorrectByeCount = 0;

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
            currentCorrectByeCount = 0;
        }
        #endregion

        public void SetBatch(List<BarnabusBaseData> batch)
        {
            //取出該組內玩家有的角色
            listBarnabusOwned = batch.FindAll(barnabus => barnabus.AlreadyOwned);
        }

        public SetQuestionInfo RandomQuestion()
        {
            int barnabusOwnedCount = listBarnabusOwned.Count;
            int randomIndex = Random.Range(0, barnabusOwnedCount);
            var barnabus = listBarnabusOwned[randomIndex];
            var sprite = gameManager.GetBarnabusSprite(barnabus.Name);

            bool isCorrectHi = RandomCorrectHi();

            currentSetQuestionInfo = new SetQuestionInfo();
            currentSetQuestionInfo.BarnabusBaseData = barnabus;
            currentSetQuestionInfo.BarnabusSprite = sprite;

            if (isCorrectHi)
            {
                currentSetQuestionInfo.BarnabusVocab = barnabus.Vocab;
                currentSetQuestionInfo.BarnabusVoice = barnabus.SoundKey;
            }
            else
            {
                currentSetQuestionInfo.BarnabusVocab = listBarnabusOwned[Random.Range(0, barnabusOwnedCount)].Vocab;
                currentSetQuestionInfo.BarnabusVoice = listBarnabusOwned[Random.Range(0, barnabusOwnedCount)].SoundKey;

                currentCorrectByeCount++;
            }

            //判斷題目類型
            currentSetQuestionInfo.QuestionType = CheckHiOrBye(currentSetQuestionInfo);

            return currentSetQuestionInfo;
        }

        private bool RandomCorrectHi()
        {
            //保底判斷
            if (currentCorrectByeCount >= (guaranteedCorrectHi - 1))
            {
                //重置累積
                currentCorrectByeCount = 0;
                return true;
            }

            //正常機率
            int randomNumber = Random.Range(0, 100);
            return randomNumber < probabilityCorrectHi;
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
