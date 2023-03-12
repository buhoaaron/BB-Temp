using UnityEngine;

namespace HiAndBye.StateControl
{
    public class CheckAnswerState : BaseGameState
    {
        public CheckAnswerState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var answerManager = gameStateController.HiAndByeGameManager.AnswerManager;
            answerManager.OnUpdateCorrectNum = UpdateCorrectNum;
            answerManager.CheckAnswer();
        }

        private void UpdateCorrectNum(int num)
        {
            gameStateController.HiAndByeGameManager.GameRootUI.SetTextCorrectNum(num);
        }

        public override void StateUpdate()
        {
            gameStateController.SetState(GAME_STATE.SET_QUESTION);
        }

        public override void End()
        { }
    }
}
