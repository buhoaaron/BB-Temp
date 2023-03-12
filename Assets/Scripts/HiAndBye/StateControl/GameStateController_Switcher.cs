using UnityEngine;

namespace HiAndBye.StateControl
{
    public partial class GameStateController
    {
        public BaseGameState CaseGameState(GAME_STATE stateName)
        {
            switch (stateName)
            {
                case GAME_STATE.GAME_INIT:
                    return new InitState(this);
                case GAME_STATE.GAME_START:
                    return new GameStartState(this);
                case GAME_STATE.SET_QUESTION:
                    return new SetQuestionState(this);
                case GAME_STATE.ANSWER_QUESTION:
                    return new AnswerQuestionState(this);
                case GAME_STATE.CHECK_ANSWER:
                    return new CheckAnswerState(this);
                case GAME_STATE.RESULT:
                    return new ResultState(this);
                default:
                    Debug.LogError(string.Format("No state found for {0}", stateName));
                    return null;
            }
        }
    }
}
