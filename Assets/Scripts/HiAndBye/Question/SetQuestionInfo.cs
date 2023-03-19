using UnityEngine;
using Barnabus;
using Spine.Unity;

namespace HiAndBye.Question
{
    /// <summary>
    /// 設定題目用資訊
    /// </summary>
    public class SetQuestionInfo
    {
        public BarnabusBaseData BarnabusBaseData = null;
        public Sprite BarnabusSprite = null;
        public SkeletonDataAsset BarnabusSkeletonDataAsset = null;
        public string BarnabusVocab = string.Empty;
        public string BarnabusFace = string.Empty;
        public AUDIO_NAME BarnabusVoice;
        public QUESTION_TYPE QuestionType;
    }
}
