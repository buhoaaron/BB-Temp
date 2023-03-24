namespace HiAndBye.StateControl
{
    public class ResultState : BaseGameState
    {
        public ResultState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var gameResultUI = gameStateController.HiAndByeGameManager.GameResultUI;
            var answerInfo = gameStateController.HiAndByeGameManager.AnswerManager.GetAnswerInfo();

            gameResultUI.SetScore(answerInfo.CorrectHiNum, answerInfo.CorrectByeNum, answerInfo.IncorrectNum);

            gameResultUI.Show();
            gameResultUI.DoPopUp();

            gameResultUI.OnButtonBackMainClick = BackMain;
            gameResultUI.OnButtonReplayClick = Replay;
        }

        public override void StateUpdate()
        { }

        public override void End()
        { }

        private void Replay()
        {
            gameStateController.SetState(GAME_STATE.GAME_INIT);
        }

        private void BackMain()
        {
            gameStateController.HiAndByeGameManager.BackMainScene();
        }
    }
}
