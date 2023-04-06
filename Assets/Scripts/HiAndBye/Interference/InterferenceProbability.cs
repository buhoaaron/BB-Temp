using UnityEngine;
using UnityEngine.Events;

namespace HiAndBye
{
    /// <summary>
    /// 干擾效果機率管理
    /// </summary>
    public class InterferenceProbability : HiAndByeBaseManager
    {

        public InterferenceProbability(HiAndByeGameManager gm) : base(gm)
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

        public float Calculate(InterferenceInfo info, float gameTime, int correctNum)
        {
            var arg1 = info.t;
            var arg2 = gameTime / info.W_Time + correctNum / info.W_CorrectNum;

            var result = Mathf.Min(arg1, arg2);

            return result;
        }
    }
}
