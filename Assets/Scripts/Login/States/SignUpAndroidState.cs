using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class SignUpAndroidState : BaseLoginSceneState
    {
        protected SignUpUI_Android signUpUI = null;

        public SignUpAndroidState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            signUpUI = CreateSignUpUI();
            signUpUI.Show();

            signUpUI.OnButtonPreviousClick = PreviousPage;
            signUpUI.OnButtonCreateClick = CheckCreateAccount;
        }

        protected void CheckCreateAccount()
        {
            //獲取玩家輸入的資料
            sceneManager.CurrentSignUpInfo = signUpUI.GetSignUpInfo();

            //檢查Email格式
            var isValidEmail = EmailChecker.CheckValidEmail(signUpUI.GetSignUpInfo().EmailAddress);
            if (!isValidEmail)
            {
                stateController.SceneManager.DoShowErrorMessage(1);
                return;
            }

            //檢查使用者規章及隱私條款是否同意
            if (!CheckToggleStatus())
            {
                stateController.SceneManager.DoShowErrorMessage(2);
                return;
            }

            NextPage();
        }
        public override void NextPage()
        {
            signUpUI.Hide();
            stateController.SetState(LOGIN_SCENE_STATE.VERIFY_AGE);
        }

        public override void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.IDENTIFICATION);
        }

        public override void StateUpdate()
        {

        }

        public override void End()
        {
            
        }

        protected virtual SignUpUI_Android CreateSignUpUI()
        {
            var ui = stateController.SceneManager.GetPage<SignUpUI_Android>(PAGE.SIGN_UP_ANDROID);

            return ui;
        }

        private bool CheckToggleStatus()
        {
            var isOn = signUpUI.CheckToggleStatus();

            if (!isOn)
                Debug.Log("You must agree to the Terms of Use.");

            return isOn;
        }
    }
}
