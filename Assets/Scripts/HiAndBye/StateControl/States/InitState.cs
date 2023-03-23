using UnityEngine;

namespace HiAndBye.StateControl
{
    public class InitState : BaseGameState
    {
        public InitState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var gameManager = gameStateController.HiAndByeGameManager;

            //取得玩家擁有角色數
            var playerBarnabusCount = gameManager.GetPlayerBarnabusCount();
            Debug.LogFormat("你擁有{0}個角色", playerBarnabusCount);

            var countDownTime = gameManager.CountDownManager.SetTime(playerBarnabusCount);
            Debug.LogFormat("獲得作答時間: {0}", countDownTime);

            //設定組別
            var batch = gameManager.GetBatch(1);
            gameManager.QuestionManager.SetBatch(batch);

            gameManager.GameResultUI.Hide();

            gameManager.AnswerManager.Clear();
            gameManager.QuestionManager.Clear();

            gameManager.GameRootUI.ResetCharacter();
            gameManager.GameRootUI.ResetTextTime();
            gameManager.GameRootUI.SetTextCorrectNum(0);
        }

        public override void StateUpdate()
        {
            gameStateController.SetState(GAME_STATE.GAME_START);
        }

        public override void End()
        { }
    }
}
