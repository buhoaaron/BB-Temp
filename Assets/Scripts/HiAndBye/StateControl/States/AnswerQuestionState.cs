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
            gameRootUI = gameStateController.HiAndByeGameManager.GameRootUI;

            gameRootUI.OnButtonHiClick = SetAndGotoCheckAnswer;
            gameRootUI.OnButtonByeClick = SetAndGotoCheckAnswer;

            gameRootUI.ButtonHi.enabled = true;
            gameRootUI.ButtonBye.enabled = true;

            
        }

        public override void StateUpdate()
        {
            if (!IsCountDowning())
                gameStateController.SetState(GAME_STATE.RESULT);
        }

        public override void End()
        {
            gameRootUI.OnButtonHiClick = null;
            gameRootUI.OnButtonByeClick = null;

            gameRootUI.ButtonHi.enabled = false;
            gameRootUI.ButtonBye.enabled = false;
        }

        private void SetAndGotoCheckAnswer(QUESTION_TYPE playerAnswer)
        {
            gameStateController.HiAndByeGameManager.AnswerManager.SetPlayerAnswer(playerAnswer);

            gameStateController.SetState(GAME_STATE.CHECK_ANSWER);
        }

        private bool IsCountDowning()
        {
            return gameStateController.HiAndByeGameManager.CountDownManager.IsCountDowning;
        }
    }
}
