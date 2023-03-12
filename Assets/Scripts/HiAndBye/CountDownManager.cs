﻿using UnityEngine;
using UnityEngine.Events;

namespace HiAndBye
{
    public class CountDownManager : HiAndByeBaseManager
    {
        public UnityAction<float> OnCountDown = null;
        public UnityAction OnCountDownOver = null;
        public bool IsCountDowning => isCountDowning;

        private bool isCountDowning = false;
        private float countDownTime = 0;
        private float realtime = 0;

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
                var countDowning = countDownTime - (Time.realtimeSinceStartup - realtime);
                OnCountDown?.Invoke(countDowning);

                if (countDowning <= 0)
                {
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
            isCountDowning = true;
            realtime = Time.realtimeSinceStartup;
        }

        public void StopCountDown()
        {
            isCountDowning = false;
        }

        /// <summary>
        /// 設定倒數時間
        /// </summary>
        /// <param name="barnabusCount">barnabus的數量</param>
        public float SetTime(int barnabusCount)
        {
            countDownTime = 10 + (barnabusCount/24) * 50;
            return countDownTime;
        }
    }
}
