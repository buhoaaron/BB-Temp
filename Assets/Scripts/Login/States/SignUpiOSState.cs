using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class SignUpiOSState : SignUpAndroidState
    {
        public SignUpiOSState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            base.Begin();

            var signUpUI_iOS = signUpUI as SignUpUI_iOS;
        }

        protected override SignUpUI_Android CreateSignUpUI()
        {
            var ui = stateController.SceneManager.GetPage<SignUpUI_iOS>(PAGE.SIGN_UP_IOS);

            return ui;
        }
    }
}
