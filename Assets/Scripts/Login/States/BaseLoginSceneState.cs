using StateMachine;

namespace Barnabus.Login.StateControl
{
    public class BaseLoginSceneState : BaseState
    {
        protected LoginSceneStateController stateController = null;
        protected LoginSceneManager sceneManager = null;

        public BaseLoginSceneState(LoginSceneStateController controller) : base(controller)
        {
            stateController = controller;
            sceneManager = stateController.SceneManager;
        }
    }
}
