using UnityEngine;

namespace HiAndBye.StateControl
{
    public class ResultState : BaseGameState
    {
        public ResultState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            Debug.Log("Result!!");

            var gameResultUI = gameManager.GameResultUI;
            var answerInfo = gameManager.AnswerManager.GetAnswerInfo();

            gameResultUI.Show();
            gameResultUI.SetScore(answerInfo.CorrectHiNum, answerInfo.CorrectByeNum, answerInfo.IncorrectEmtionsCount);
            gameResultUI.DoPopUp();

            gameResultUI.OnButtonOKClick = GotoGetPotion;

            CreateIncorrectBarnabus(answerInfo);
        }

        public override void StateUpdate()
        { }

        public override void End()
        {
            gameManager.IncorrentBarnabusBuilder.Destroy();

            gameManager.GameResultUI.Hide();
        }

        private void CreateIncorrectBarnabus(AnswerResultInfo answerResultInfo)
        {
            var incorrectEmtionsCount = answerResultInfo.IncorrectEmtionsCount;
            var controllers = gameManager.IncorrentBarnabusBuilder.Create(incorrectEmtionsCount);
            foreach (var controller in controllers)
            {
                int index = controllers.IndexOf(controller);
                var info = answerResultInfo.ListIncorrectBarnabus[index];

                controller.SetBarnabus(gameManager.GetBarnabusSprite(info.BarnabusBaseData.Name));
                controller.SetName(info.BarnabusBaseData.Name);

                controller.OnButtonBarnabusClick = () => 
                {
                    gameManager.PlaySound(info.BarnabusVoice);
                };
            }
        }

        private void GotoGetPotion()
        {
            gameStateController.SetState(GAME_STATE.POTION_REWARD);
        }
    }
}
