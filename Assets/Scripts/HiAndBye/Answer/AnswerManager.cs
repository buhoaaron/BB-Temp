using UnityEngine;
using UnityEngine.Events;
using HiAndBye.Question;

namespace HiAndBye
{
    /// <summary>
    /// 答題管理者
    /// </summary>
    public class AnswerManager : HiAndByeBaseManager
    {
        public UnityAction<int> OnUpdateCorrectNum;

        private QuestionManager questionManager = null;

        private int correctHiNum = 0;
        private int correctByeNum = 0;
        private int incorrectNum = 0;
        private QUESTION_TYPE currentPlayerAnswer;

        public AnswerManager(HiAndByeGameManager gm, QuestionManager qm) : base(gm)
        {
            questionManager = qm;
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
            correctHiNum = 0;
            correctByeNum = 0;
            incorrectNum = 0;
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
            }
        }

        public AnswerInfo GetAnswerInfo()
        {
            return new AnswerInfo(correctHiNum, correctByeNum, incorrectNum);
        }

        public void SetPlayerAnswer(QUESTION_TYPE playerAnswer)
        {
            currentPlayerAnswer = playerAnswer;
        }
    }
}
