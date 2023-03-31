using UnityEngine;
using UnityEngine.Events;

namespace HiAndBye
{
    public class CountDownManager : HiAndByeBaseManager
    {
        public UnityAction<string> OnCountDownFormat = null;
        public UnityAction<float> OnCountDown = null;
        public UnityAction OnCountDownOver = null;

        public float CountDownTime => countDownTime;
        public bool IsCountDowning => isCountDowning;

        private bool isCountDowning = false;
        private bool isCountDownOver = false;
        private float countDownTime = 0;
        private float startRealtime = 0;
        private float pauseRealtime = 0;

        public CountDownManager(HiAndByeGameManager gm) : base(gm)
        {
        }

        #region BASE_API
        public override void Init()
        {

        }
        public override void SystemUpdate()
        {
            if (isCountDowning)
            {
                var countDowning = countDownTime - (Time.realtimeSinceStartup - startRealtime);

                OnCountDown?.Invoke(countDowning);
                OnCountDownFormat?.Invoke(ConvertCountDownFormat(countDowning));

                if (countDowning <= 0)
                {
                    isCountDownOver = true;

                    StopCountDown();
                    OnCountDownOver?.Invoke();
                }
            }
        }
        public override void Clear()
        {
            
        }
        #endregion

        public void StartCountDown()
        {
            isCountDownOver = false;
            isCountDowning = true;
            startRealtime = Time.realtimeSinceStartup;
        }

        public void PauseCountDown()
        {
            pauseRealtime = Time.realtimeSinceStartup;
            isCountDowning = false;
        }

        public void ContinueCountDown()
        {
            isCountDowning = true;
            //把暫停的時間補正回來
            startRealtime += (Time.realtimeSinceStartup - pauseRealtime);
        }

        public void StopCountDown()
        {
            isCountDowning = false;
        }

        /// <summary>
        /// 檢查當前的倒數是否結束
        /// </summary>
        public bool CheckCountDownOver()
        {
            return isCountDownOver;
        }

        /// <summary>
        /// 設定倒數時間
        /// </summary>
        public float SetTime(int barnabusCount)
        {
            countDownTime = 10 + (barnabusCount / 24f) * 50;
            return countDownTime;
        }

        /// <summary>
        /// 轉成倒數需要的格式
        /// </summary>
        private string ConvertCountDownFormat(float time)
        {
            return time.ToString("#0.00");
        }
    }
}
