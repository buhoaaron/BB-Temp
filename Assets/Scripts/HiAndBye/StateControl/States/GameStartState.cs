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
            gameStateController.HiAndByeGameManager.CountDownManager.StartCountDown();

            gameStateController.HiAndByeGameManager.CountDownManager.OnCountDownOver = () =>
            {

            };

            gameStateController.HiAndByeGameManager.CountDownManager.OnCountDown = (time) =>
            {
                gameStateController.HiAndByeGameManager.GameRootUI.SetTextTime(time.ToString("#0.00"));
            };
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
