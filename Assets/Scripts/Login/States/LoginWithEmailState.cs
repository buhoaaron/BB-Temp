using UnityEngine;
using Barnabus.Network;

namespace Barnabus.Login.StateControl
{
    public class LoginWithEmailState : BaseLoginSceneState
    {
        private LoginWithEmailUI loginWithEmailUI = null;

        public LoginWithEmailState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            loginWithEmailUI = CreateLoginWithEmailUI();
            loginWithEmailUI.Show();

            loginWithEmailUI.OnButtonLoginClick = ProcessLogin;
        }

        private void ProcessLogin()
        {
            //檢查Email格式
            var isValidEmail = EmailChecker.CheckValidEmail(loginWithEmailUI.GetLoginInfo().EmailAddress);
            if (!isValidEmail)
            {
                stateController.SceneManager.DoShowErrorMessage(1);
                return;
            }

            SendLogin();
        }

        #region SEND_LOGIN
        private void SendLogin()
        {
            var loginInfo = loginWithEmailUI.GetLoginInfo();
            var data = new SendLogin(loginInfo.EmailAddress, loginInfo.Password);

            var callbacks = new NetworkCallbacks();
            callbacks.OnSuccess = OnLoginSuccess;
            callbacks.OnFail = OnLoginFail;

            sceneManager.PostRequest(API_PATH.Login, data, callbacks);
        }

        private void OnLoginSuccess(string text)
        {
            NextPage();
        }

        private void OnLoginFail(ReceiveErrorMessage errorMessage)
        {
            stateController.SceneManager.DoShowErrorMessage(errorMessage.error);
        }
        #endregion

        public override void End()
        {
            loginWithEmailUI.Hide();
        }

        private void NextPage()
        {
            
        }

        private void PreviousPage()
        {
            
        }

        protected virtual LoginWithEmailUI CreateLoginWithEmailUI()
        {
            var key = AddressablesLabels.CanvasLoginWithEmail;
            var ui = stateController.SceneManager.GetPage(key) as LoginWithEmailUI;

            return ui;
        }
    }
}
