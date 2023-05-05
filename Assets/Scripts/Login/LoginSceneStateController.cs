using StateMachine;

namespace Barnabus.Login.StateControl
{
    /// <summary>
    /// Login場景的狀態機
    /// </summary>
    public partial class LoginSceneStateController : StateController
    {
        public LoginSceneManager SceneManager = null;
        /// <summary>
        /// 上一個狀態
        /// </summary>
        protected BaseLoginSceneState previousState = null;

        public LoginSceneStateController(LoginSceneManager lsm)
        {
            SceneManager = lsm;
        }

        public void SetState(LOGIN_SCENE_STATE stateName)
        {
            //紀錄狀態
            if (currentState != null)
                previousState = currentState as BaseLoginSceneState;

            BaseLoginSceneState state = CaseState(stateName);

            base.SetState(state);
        }
        /// <summary>
        /// 回到上一狀態
        /// </summary>
        public void BackToPreviousState()
        {
            if (previousState != null)
                base.SetState(previousState);
        }
    }
}
