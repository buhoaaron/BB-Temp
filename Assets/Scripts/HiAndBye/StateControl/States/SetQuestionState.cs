using HiAndBye.Question;

namespace HiAndBye.StateControl
{
    public class SetQuestionState : BaseGameState
    {
        private GameRootUI gameRootUI = null;
        private SetQuestionInfo setQuestionInfo = null;
        public SetQuestionState(GameStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            gameRootUI = gameManager.GameRootUI;
            setQuestionInfo = gameManager.QuestionManager.RandomQuestion();

            DoDropDown();

            SetQuestion();
            SetInterferenceEffect();
        }
        private void SetQuestion()
        {
            gameRootUI.SetBarnabusSpine(setQuestionInfo.BarnabusSkeletonDataAsset, setQuestionInfo.BarnabusFace);
            gameRootUI.SetTextVocab(setQuestionInfo.BarnabusVocab);
            gameRootUI.SetDebugTextAnswer(setQuestionInfo.QuestionType.ToString());
        }
        private void SetInterferenceEffect()
        {
            var gameTime = gameManager.CountDownManager.CountDownTime;
            var correctNum = gameManager.AnswerManager.GetAnswerInfo().CorrectNum;
            var interferenceEffectInfo = gameManager.InterferenceManager.RandomInterferenceEffect(gameTime, correctNum);

            if (interferenceEffectInfo != null)
            {
                var particle = gameRootUI.GetInterferenceParticle(interferenceEffectInfo.EffectType);
                particle.Play();
            }
        }

        private void DoDropDown()
        {
            gameRootUI.DoDropDown(OnDropDownComplete);
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
