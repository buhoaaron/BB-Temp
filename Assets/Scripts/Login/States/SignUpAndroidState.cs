using System.Net.Mail;
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
            Debug.Log(signUpUI.GetSignUpInfo().ToString());

            //檢查Email格式
            var isValidEmail = IsValidEmail(signUpUI.GetSignUpInfo().EmailAddress);
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
        private void NextPage()
        {
            signUpUI.Hide();
            stateController.SetState(LOGIN_SCENE_STATE.VERIFY_AGE);
        }

        protected void PreviousPage()
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
            var key = AddressablesLabels.CanvasSignUpAndroid;
            var ui = stateController.SceneManager.GetPage(key) as SignUpUI_Android;

            return ui;
        }

        private bool CheckToggleStatus()
        {
            var isOn = signUpUI.CheckToggleStatus();

            if (!isOn)
                Debug.Log("You must agree to the Terms of Use.");

            return isOn;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
