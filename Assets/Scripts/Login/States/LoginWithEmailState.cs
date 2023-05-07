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
            loginWithEmailUI = stateController.SceneManager.GetPage<LoginWithEmailUI>(PAGE.LOGIN_WITH_EMAIL);
            loginWithEmailUI.Show();

            loginWithEmailUI.OnButtonLoginClick = ProcessLogin;
            loginWithEmailUI.OnButtonPreviousClick = PreviousPage;

            sceneManager.NetworkManager.Dispatcher.OnReceiveLogin += OnLoginSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage += OnLoginFail;
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

            sceneManager.PostRequest(API_PATH.Login, data);
        }

        private void OnLoginSuccess(ReceiveLogin receiveLogin)
        {
            var accountInfo = new FamiliesAccountInfo(receiveLogin.meandmine_id, receiveLogin.access_token, 
                                              receiveLogin.birth_year, receiveLogin.players_list);

            sceneManager.NetworkManager.UpdateFamiliesAccountInfo(accountInfo);

            loginWithEmailUI.Hide();

            NextPage();
        }

        private void OnLoginFail(ReceiveErrorMessage errorMessage)
        {
            stateController.SceneManager.DoShowErrorMessage(errorMessage.error);
        }
        #endregion

        public override void End()
        {
            sceneManager.NetworkManager.Dispatcher.OnReceiveLogin -= OnLoginSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage -= OnLoginFail;
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
