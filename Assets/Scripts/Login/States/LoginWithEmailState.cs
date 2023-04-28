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
            var pageKey = AddressablesLabels.CanvasLoginWithEmail;

            loginWithEmailUI = stateController.SceneManager.GetPage<LoginWithEmailUI>(pageKey);
            loginWithEmailUI.Show();

            loginWithEmailUI.OnButtonLoginClick = ProcessLogin;
            loginWithEmailUI.OnButtonPreviousClick = PreviousPage;

            sceneManager.NetworkManager.Dispatcher.OnReceiveLogin += OnLoginSuccess;
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

        private void OnLoginSuccess(ReceiveLogin receiveLogin)
        {
            var networkInfo = new NetworkInfo(receiveLogin.meandmineid, receiveLogin.access_token, receiveLogin.players_list);

            sceneManager.NetworkManager.UpdatePlayerNetworkInfo(networkInfo);

            NextPage();
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
            sceneManager.NetworkManager.Dispatcher.OnReceiveLogin -= OnLoginSuccess;
        }

        public override void NextPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.CHOOSE_PROFILE);
        }

        public override void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.IDENTIFICATION);
        }
    }
}
