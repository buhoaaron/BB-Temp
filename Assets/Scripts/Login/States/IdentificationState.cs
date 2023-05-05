using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class IdentificationState : BaseLoginSceneState
    {
        private IdentificationUI identificationUI = null;

        public IdentificationState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            identificationUI = sceneManager.IdentificationUI;
            //待Shift表演完後重置所有Page
            identificationUI.DoShift(false, ResetPage);
            identificationUI.OnButtonFamiliesClick = CheckPlatformAndGoSignUpState;
            identificationUI.OnButtonHaveAccountClick = GotoLoginState;

            identificationUI.AutoPlayPageView(true);
        }

        private void ResetPage()
        {
            sceneManager.ResetPages();
        }

        public override void StateUpdate()
        {

        }

        public override void NextPage()
        {

        }

        public override void PreviousPage()
        {

        }

        public override void End()
        {
            identificationUI.AutoPlayPageView(false);
            identificationUI.DoShift(true);
        }

        private void CheckPlatformAndGoSignUpState()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
                stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_IOS);
            else
                stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_ANDROID);
        }

        private void GotoLoginState()
        {
            stateController.SetState(LOGIN_SCENE_STATE.LOGIN);
        }
    }
}
