using UnityEngine;

namespace HiAndBye.StateControl
{
    public class PauseState : BaseGameState
    {
        public PauseState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            gameManager.CountDownManager.PauseCountDown();

            gameManager.SettingUI.Show();
            gameManager.SettingUI.DoPopUp();

            gameManager.SettingUI.OnButtonYesClick = BackMain;
            gameManager.SettingUI.OnButtonNoClick = GameResume;
        }

        private void BackMain()
        {
            gameManager.BackMainScene();
        }

        private void GameResume()
        {
            gameStateController.SetState(GAME_STATE.ANSWER_QUESTION);
        }

        public override void StateUpdate()
        {

        }

        public override void End()
        {
            gameManager.SettingUI.Hide();

            gameManager.CountDownManager.ContinueCountDown();
        }
    }
}
