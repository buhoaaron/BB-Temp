using UnityEngine;

namespace HiAndBye.StateControl
{
    public class AnswerQuestionState : BaseGameState
    {
        private GameRootUI gameRootUI = null;

        public AnswerQuestionState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            gameRootUI = gameManager.GameRootUI;

            gameRootUI.OnButtonHiClick = SetAndGotoCheckAnswer;
            gameRootUI.OnButtonByeClick = SetAndGotoCheckAnswer;
            gameRootUI.OnButtonBackMainClick = GotoPauseState;

            gameRootUI.ButtonHi.enabled = true;
            gameRootUI.ButtonBye.enabled = true;
            gameRootUI.buttonBackMain.enabled = true;
        }

        public override void StateUpdate()
        {
            if (gameManager.CountDownManager.CheckCountDownOver())
            {
                GotoResultState();
            }
        }

        public override void End()
        {
            gameRootUI.OnButtonHiClick = null;
            gameRootUI.OnButtonByeClick = null;
            gameRootUI.OnButtonBackMainClick = null;

            gameRootUI.ButtonHi.enabled = false;
            gameRootUI.ButtonBye.enabled = false;
            gameRootUI.buttonBackMain.enabled = false;
        }

        private void SetAndGotoCheckAnswer(QUESTION_TYPE playerAnswer)
        {
            gameStateController.HiAndByeGameManager.AnswerManager.SetPlayerAnswer(playerAnswer);

            gameStateController.SetState(GAME_STATE.CHECK_ANSWER);
        }

        private void GotoResultState()
        {
            gameStateController.SetState(GAME_STATE.RESULT);
        }

        private void GotoPauseState()
        {
            gameStateController.SetState(GAME_STATE.PAUSE);
        }

        private bool IsCountDowning()
        {
            return gameStateController.HiAndByeGameManager.CountDownManager.IsCountDowning;
        }
    }
}
