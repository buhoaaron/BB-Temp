using StateMachine;

namespace HiAndBye.StateControl
{
    public class BaseGameState : BaseState
    {
        protected GameStateController gameStateController = null;
        protected HiAndByeGameManager gameManager = null;

        public BaseGameState(GameStateController controller) : base(controller)
        {
            gameStateController = controller;
            gameManager = gameStateController.HiAndByeGameManager;
        }
    }
}
