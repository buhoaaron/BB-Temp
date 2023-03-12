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
            var correctNum = gameStateController.HiAndByeGameManager.AnswerManager.CorrectNum;
            var incorrectNum = gameStateController.HiAndByeGameManager.AnswerManager.IncorrectNum;

            gameResultUI.SetScore(correctNum, incorrectNum);

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
