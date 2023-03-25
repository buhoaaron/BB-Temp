namespace HiAndBye.StateControl
{
    public class PotionRewardState : BaseGameState
    {
        public PotionRewardState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var potionRewardUI = gameManager.PotionRewardUI;

            potionRewardUI.Show();
            potionRewardUI.DoPopUp();

            potionRewardUI.OnButtonBackMainClick = BackMain;
            potionRewardUI.OnButtonReplayClick = GameReplay;

            potionRewardUI.SetPotionValue(5);
        }

        private void BackMain()
        {
            gameManager.BackMainScene();
        }

        private void GameReplay()
        {
            gameStateController.SetState(GAME_STATE.GAME_INIT);
        }

        public override void StateUpdate()
        { }

        public override void End()
        {
            gameManager.PotionRewardUI.Hide();
        }
    }
}
