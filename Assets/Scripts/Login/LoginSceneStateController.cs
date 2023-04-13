using StateMachine;

namespace Barnabus.Login.StateControl
{
    /// <summary>
    /// Login場景的狀態機
    /// </summary>
    public partial class LoginSceneStateController : StateController
    {
        public LoginSceneManager SceneManager = null;

        public LoginSceneStateController(LoginSceneManager lsm)
        {
            SceneManager = lsm;
        }

        public void SetState(LOGIN_SCENE_STATE stateName)
        {
            BaseLoginSceneState state = CaseState(stateName);

            base.SetState(state);
        }
    }
}
