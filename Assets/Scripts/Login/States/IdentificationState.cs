using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class IdentificationState : BaseLoginSceneState
    {
        public IdentificationState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            stateController.SceneManager.IdentificationUI.DoShift(false);
            //stateController.SceneManager.IdentificationUI.Show();
            stateController.SceneManager.IdentificationUI.OnButtonFamiliesClick = CheckPlatformAndGoSignUp;
        }

        public override void StateUpdate()
        {

        }

        public override void End()
        {
            //stateController.SceneManager.IdentificationUI.Hide();
            stateController.SceneManager.IdentificationUI.DoShift(true);
        }

        private void CheckPlatformAndGoSignUp()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
                stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_IOS);
            else
                stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_ANDROID);
        }
    }
}
