using UnityEngine.Events;
using HiAndBye.Question;
using System.Collections.Generic;

namespace HiAndBye
{
    /// <summary>
    /// 答題管理者
    /// </summary>
    public class AnswerManager : HiAndByeBaseManager
    {
        public List<SetQuestionInfo> ListIncorrect => listIncorrect;
        public UnityAction<int> OnUpdateCorrectNum;

        private QuestionManager questionManager = null;

        private int correctHiNum = 0;
        private int correctByeNum = 0;
        private int incorrectNum = 0;
        private QUESTION_TYPE currentPlayerAnswer;
        /// <summary>
        /// 玩家答錯的題目
        /// </summary>
        private List<SetQuestionInfo> listIncorrect = new List<SetQuestionInfo>();

        public AnswerManager(HiAndByeGameManager gm, QuestionManager qm) : base(gm)
        {
            questionManager = qm;
        }

        #region BASE_API
        public override void Init()
        {
            listIncorrect = new List<SetQuestionInfo>();
        }
        public override void SystemUpdate()
        {
           
        }
        public override void Clear()
        {
            correctHiNum = 0;
            correctByeNum = 0;
            incorrectNum = 0;

            listIncorrect.Clear();
        }
        #endregion

        public void CheckAnswer()
        {
            var questionInfo = questionManager.GetCurrentQuestionInfo();

            //比對玩家答案
            if (questionInfo.QuestionType == currentPlayerAnswer)
            {
                if(questionInfo.QuestionType == QUESTION_TYPE.HI)
                {
                    correctHiNum++;
                }
                else
                {
                    correctByeNum++;
                }

                var correctNum = correctHiNum + correctByeNum;
                OnUpdateCorrectNum?.Invoke(correctNum);
            }
            else
            {
                incorrectNum++;

                AddIncorrect(questionInfo);
            }
        }

        private void AddIncorrect(SetQuestionInfo incorrectInfo)
        {
            var checkDuplicate = listIncorrect.Find(info => info.BarnabusBaseData == incorrectInfo.BarnabusBaseData);

            if (checkDuplicate == null)
                listIncorrect.Add(incorrectInfo);
        }

        public AnswerResultInfo GetAnswerInfo()
        {
            return new AnswerResultInfo(correctHiNum, correctByeNum, incorrectNum, listIncorrect);
        }

        public void SetPlayerAnswer(QUESTION_TYPE playerAnswer)
        {
            currentPlayerAnswer = playerAnswer;
        }
    }
}
