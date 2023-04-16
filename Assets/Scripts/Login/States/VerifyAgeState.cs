using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class VerifyAgeState : BaseLoginSceneState
    {
        private VerifyAgeUI verifyAgeUI = null;

        public VerifyAgeState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            verifyAgeUI = CreateVerifyAgeUI();
            verifyAgeUI.Init();
            verifyAgeUI.HideSecurity();
            verifyAgeUI.Show();

            //設定事件
            verifyAgeUI.OnButtonPreviousClick = PreviousPage;
            verifyAgeUI.NumericKeypad.OnKeypadClick = verifyAgeUI.BirthYearUI.SetNumber;
            verifyAgeUI.OnButtonClearClick = verifyAgeUI.BirthYearUI.ResetNumbers;
            verifyAgeUI.OnButtonContinueClick = CheckBirthYearVaild;
        }

        private void PreviousPage()
        {
            verifyAgeUI.Hide();
            stateController.SetState(LOGIN_SCENE_STATE.SIGN_UP_ANDROID);
        }

        private void GetBirthYear()
        {
            Debug.Log(GetBirthYearInputResult());
        }

        protected virtual VerifyAgeUI CreateVerifyAgeUI()
        {
            var key = AddressablesLabels.CanvasVerifyAge;
            var ui = stateController.SceneManager.GetPage(key);

            if (ui == null)
                ui = stateController.SceneManager.CreateUI<VerifyAgeUI>(key);

            return ui as VerifyAgeUI;
        }

        private void CheckBirthYearVaild()
        {
            var empty = verifyAgeUI.BirthYearUI.CheckControllerEmpty();
            //是否有未輸入的欄位
            if (empty != null)
            {
                empty.DoShake();
                return;
            }

            GetBirthYear();
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
