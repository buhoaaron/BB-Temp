namespace HiAndBye
{
    /// <summary>
    /// 答題資料
    /// </summary>
    public class AnswerInfo
    {
        public int CorrectNum => CorrectHiNum + CorrectByeNum;
        public int CorrectHiNum { get; }
        public int CorrectByeNum { get; }
        public int IncorrectNum { get; }

        public AnswerInfo(int correctHiNum, int correctByeNum, int incorrectNum)
        {
            CorrectHiNum = correctHiNum;
            CorrectByeNum = correctByeNum;
            IncorrectNum = incorrectNum;
        }
    }
}
