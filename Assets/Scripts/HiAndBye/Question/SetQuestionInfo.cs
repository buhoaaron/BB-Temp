using UnityEngine;
using Barnabus;

namespace HiAndBye.Question
{
    /// <summary>
    /// 設定題目用資訊
    /// </summary>
    public class SetQuestionInfo
    {
        public BarnabusBaseData BarnabusBaseData = null;
        public Sprite BarnabusSprite = null;
        public string BarnabusVocab = string.Empty;
        public AUDIO_NAME BarnabusVoice;
        public QUESTION_TYPE QuestionType;
    }
}
