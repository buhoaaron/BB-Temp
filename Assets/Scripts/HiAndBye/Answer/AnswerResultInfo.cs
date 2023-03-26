using HiAndBye.Question;
using System.Collections.Generic;

namespace HiAndBye
{
    /// <summary>
    /// 答題資料
    /// </summary>
    public class AnswerResultInfo
    {
        public int IncorrectEmtionsCount => ListIncorrectBarnabus.Count;
        public int CorrectNum => CorrectHiNum + CorrectByeNum;
        public int CorrectHiNum { get; }
        public int CorrectByeNum { get; }
        public int IncorrectNum { get; }

        public List<SetQuestionInfo> ListIncorrectBarnabus = null;

        public AnswerResultInfo(int correctHiNum, int correctByeNum, int incorrectNum, List<SetQuestionInfo> incorrectBarnabus)
        {
            CorrectHiNum = correctHiNum;
            CorrectByeNum = correctByeNum;
            IncorrectNum = incorrectNum;

            ListIncorrectBarnabus = incorrectBarnabus;
        }
    }
}
