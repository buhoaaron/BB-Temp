using UnityEngine;
using Barnabus.Network;

namespace Barnabus.Login.StateControl
{
    public class VerifyAgeState : BaseLoginSceneState
    {
        private VerifyAgeUI verifyAgeUI = null;

        private int birthYear = 0;
        public VerifyAgeState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            verifyAgeUI = stateController.SceneManager.GetPage<VerifyAgeUI>(PAGE.VERIFY_AGE);
            verifyAgeUI.HideSecurity();
            verifyAgeUI.Show();

            //設定事件
            verifyAgeUI.OnButtonPreviousClick = PreviousPage;
            verifyAgeUI.OnButtonContinueClick = ProcessBirthYearVaild;

            verifyAgeUI.SecurityUI.OnButtonCloseClick = verifyAgeUI.HideSecurity;

            sceneManager.NetworkManager.Dispatcher.OnReceiveSignUp += OnSignUpSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage += OnSignUpFail;
        }

        /// <summary>
        /// 處理輸入是否有效進行下一個流程
        /// </summary>
        private void ProcessBirthYearVaild()
        {
            //輸入是否有效
            if (!verifyAgeUI.BirthYearKeypad.CheckBirthYearVaild())
                return;

            birthYear = verifyAgeUI.BirthYearKeypad.GetBirthYearInputResult();
            var isAdult = sceneManager.CheckAdultAge(birthYear);

            if (!isAdult)
            {
                //未滿18跳標語
                verifyAgeUI.DoPopUpSecurity();
                return;
            }

            SendSignUp();
        }

        #region SEND_SIGN_UP
        private void SendSignUp()
        {
            var email = sceneManager.CurrentSignUpInfo.EmailAddress;
            var password = sceneManager.CurrentSignUpInfo.Password;
            var data = new SendSignUp(email, password, birthYear);

            sceneManager.PostRequest(API_PATH.SignUp, data);
        }
        private void OnSignUpSuccess(ReceiveSignUp receiveSignUp)
        {
            var networkInfo = new NetworkInfo(receiveSignUp.meandmine_id, receiveSignUp.access_token);

            sceneManager.NetworkManager.UpdatePlayerNetworkInfo(networkInfo);

            NextPage();
        }

        private void OnSignUpFail(ReceiveErrorMessage errorMessage)
        {
            stateController.SceneManager.DoShowErrorMessage(errorMessage.error);
        }
        #endregion

        public override void NextPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.ACCOUNT);
        }

        public override void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_ANDROID);
        }

        

        public override void End()
        {
            sceneManager.NetworkManager.Dispatcher.OnReceiveSignUp -= OnSignUpSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage -= OnSignUpFail;

            verifyAgeUI.BirthYearKeypad.ResetNumbers();
            verifyAgeUI.Hide();
        }
    }
}
