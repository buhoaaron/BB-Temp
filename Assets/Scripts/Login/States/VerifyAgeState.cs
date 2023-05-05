using UnityEngine;
using Barnabus.Network;

namespace Barnabus.Login.StateControl
{
    public class VerifyAgeState : BaseLoginSceneState
    {
        private VerifyAgeUI verifyAgeUI = null;
        private BirthYearNumberController currentBirthYearNumber = null;

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
            verifyAgeUI.NumericKeypad.OnKeypadClick = InputNumber;
            verifyAgeUI.OnButtonClearClick = ResetNumbers;
            verifyAgeUI.OnButtonContinueClick = CheckBirthYearVaild;

            verifyAgeUI.SecurityUI.OnButtonCloseClick = verifyAgeUI.HideSecurity;

            sceneManager.NetworkManager.Dispatcher.OnReceiveSignUp += OnSignUpSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage += OnSignUpFail;

            HighlightBirthYearField();
        }

        /// <summary>
        /// 檢查輸入是否有效
        /// </summary>
        private void CheckBirthYearVaild()
        {
            //是否有未輸入的欄位
            if (currentBirthYearNumber != null)
            {
                //振動提示
                currentBirthYearNumber.DoShake();
                return;
            }

            birthYear = GetBirthYearInputResult();
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

        private void InputNumber(int number)
        {
            verifyAgeUI.BirthYearUI.SetNumber(number);

            HighlightBirthYearField();
        }

        private void ResetNumbers()
        {
            verifyAgeUI.BirthYearUI.ResetNumbers();

            HighlightBirthYearField();
        }

        /// <summary>
        /// 高光使用者即將輸入的欄位
        /// </summary>
        private void HighlightBirthYearField()
        {
            currentBirthYearNumber?.ChangeSwitchSprite(false);
            currentBirthYearNumber = verifyAgeUI.BirthYearUI.CheckControllerEmpty();
            currentBirthYearNumber?.ChangeSwitchSprite(true);
        }

        public override void End()
        {
            sceneManager.NetworkManager.Dispatcher.OnReceiveSignUp -= OnSignUpSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage -= OnSignUpFail;

            ResetNumbers();
            verifyAgeUI.Hide();
        }

        private int GetBirthYearInputResult()
        {
            var controllers = verifyAgeUI.BirthYearUI.Controllers;

            float result = 0;

            var digits = controllers[3].GetNumber();
            var tensDigit = controllers[2].GetNumber();
            var hundredsDigit = controllers[1].GetNumber();
            var thousandsDigit = controllers[0].GetNumber();

            result += digits * Mathf.Pow(10, 0);
            result += tensDigit * Mathf.Pow(10, 1);
            result += hundredsDigit * Mathf.Pow(10, 2);
            result += thousandsDigit * Mathf.Pow(10, 3);

            return (int)result;
        }
    }
}
