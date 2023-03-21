using StateMachine;

namespace HiAndBye.StateControl
{
    /// <summary>
    /// Hi&Bye遊戲的狀態機
    /// </summary>
    public partial class GameStateController : StateController
    {
        public HiAndByeGameManager HiAndByeGameManager = null;

        public GameStateController(HiAndByeGameManager gm)
        {
            HiAndByeGameManager = gm;
        }

        public void SetState(GAME_STATE stateName)
        {
            BaseGameState state = CaseGameState(stateName);

            base.SetState(state);
        }
    }
}
