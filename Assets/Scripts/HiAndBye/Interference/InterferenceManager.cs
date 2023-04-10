using UnityEngine;
using UnityEngine.Events;

namespace HiAndBye
{
    /// <summary>
    /// 干擾效果管理
    /// </summary>
    public class InterferenceManager : HiAndByeBaseManager
    {
        private AllInterferenceInfo allInterferenceInfo = null;
        private InterferenceProbability probability = null;
        public InterferenceManager(HiAndByeGameManager gm) : base(gm)
        {
        }

        #region BASE_API
        public override void Init()
        {
            probability = new InterferenceProbability(gameManager);
            probability.Init();
        }

        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {
            
        }
        #endregion
        /// <summary>
        /// 決定要出哪個干擾效果
        /// </summary>
        /// <returns>回傳null代表沒有選中干擾效果</returns>
        public InterferenceInfo RandomInterferenceEffect(float gameTime, int correctNum)
        {
            Debug.Log(string.Format("RandomInterferenceEffect, gameTime:{0}, correctNum:{1}", gameTime, correctNum));

            foreach (var info in allInterferenceInfo)
            {
                var occurProbability = probability.Calculate(info, gameTime, correctNum);

                Debug.Log(string.Format("{0} probability: {1}", info.EffectType, occurProbability));

                if (RandomEffectOccur(occurProbability))
                {
                    Debug.Log("Occur " + info.EffectType);
                    return info;
                }
            }

            Debug.Log("no InterferenceEffect Occur.");
            return null;
        }
        private bool RandomEffectOccur(float occurProbability)
        {
            var randomSeed = Random.Range(0f, 1f);

            return randomSeed <= occurProbability;
        }

        public void SetAllInterferenceInfo(AllInterferenceInfo allInterferenceInfo)
        {
            this.allInterferenceInfo = allInterferenceInfo;
        }
    }
}
