using UnityEngine;

namespace HiAndBye.StateControl
{
    public class GameStartState : BaseGameState
    {
        private float realTime = 0;
        public GameStartState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            realTime = Time.realtimeSinceStartup;
        }

        public override void StateUpdate()
        {
            //TO_FIX: 開場表演預留
            if (Time.realtimeSinceStartup - realTime >= 0.5f)
            {
                StartCountDown();

                ShowCountDownTime();

                gameStateController.SetState(GAME_STATE.SET_QUESTION);
            }
        }

        public override void End()
        { }

        private void StartCountDown()
        {
            var countDownManager = gameStateController.HiAndByeGameManager.CountDownManager;

            countDownManager.StartCountDown();
            countDownManager.OnCountDownFormat = gameStateController.HiAndByeGameManager.GameRootUI.SetTextTime;
        }
        /// <summary>
        /// 顯示倒數時間元件
        /// </summary>
        private void ShowCountDownTime()
        {
            gameStateController.HiAndByeGameManager.GameRootUI.DoShowTime();
        }
    }
}
