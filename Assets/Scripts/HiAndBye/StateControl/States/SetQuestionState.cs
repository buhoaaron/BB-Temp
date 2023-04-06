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
            setQuestionInfo = gameManager.QuestionManager.RandomQuestion();

            var gameTime = gameManager.CountDownManager.CountDownTime;
            var correctNum = gameManager.AnswerManager.GetAnswerInfo().CorrectNum;
            var interferenceEffectInfo = gameManager.InterferenceManager.RandomInterferenceEffect(gameTime, correctNum);

            SetQuestion();

            DoDropDown();
        }
        private void SetQuestion()
        {
            var gameRootUI = gameManager.GameRootUI;

            gameRootUI.SetBarnabusSpine(setQuestionInfo.BarnabusSkeletonDataAsset, setQuestionInfo.BarnabusFace);
            gameRootUI.SetTextVocab(setQuestionInfo.BarnabusVocab);
            gameRootUI.SetDebugTextAnswer(setQuestionInfo.QuestionType.ToString());
        }
        private void DoDropDown()
        {
            gameManager.GameRootUI.DoDropDown(OnDropDownComplete);
        }
        private void OnDropDownComplete()
        {
            PlayBarnabusSound();
            GotoAnswerQuestionState();
        }
        private void PlayBarnabusSound()
        {
            gameManager.PlaySound(setQuestionInfo.BarnabusVoice);
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
