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

            //根據階級給予玩家藥水
            var potions = gameManager.RankManager.GetRankInfo().Potions;
            potionRewardUI.SetPotionValue(potions);
            //更新玩家資料
            gameManager.IncreasePlayerPotionAndSave(potions);
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
