using HiAndBye.Question;

namespace HiAndBye.StateControl
{
    public class SetQuestionState : BaseGameState
    {
        private SetQuestionInfo setQuestionInfo = null;
        public SetQuestionState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            setQuestionInfo = gameStateController.HiAndByeGameManager.QuestionManager.RandomQuestion();

            SetQuestion();

            DoDropDown();
        }
        private void SetQuestion()
        {
            var gameRootUI = gameStateController.HiAndByeGameManager.GameRootUI;

            gameRootUI.SetBarnabusSpine(setQuestionInfo.BarnabusSkeletonDataAsset, setQuestionInfo.BarnabusFace);
            gameRootUI.SetTextVocab(setQuestionInfo.BarnabusVocab);
            gameRootUI.SetDebugTextAnswer(setQuestionInfo.QuestionType.ToString());
        }
        private void DoDropDown()
        {
            var gameRootUI = gameStateController.HiAndByeGameManager.GameRootUI;

            gameRootUI.DoDropDown(OnDropDownComplete);
        }
        private void OnDropDownComplete()
        {
            PlayBarnabusSound();
            GotoAnswerQuestionState();
        }
        private void PlayBarnabusSound()
        {
            gameStateController.HiAndByeGameManager.PlaySound(setQuestionInfo.BarnabusVoice);
        }
        private void GotoAnswerQuestionState()
        {
            gameStateController.SetState(GAME_STATE.ANSWER_QUESTION);
        }
        public override void StateUpdate()
        { }

        public override void End()
        { }
    }
}
