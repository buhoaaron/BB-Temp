﻿using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class VerifyAgeState : BaseLoginSceneState
    {
        private VerifyAgeUI verifyAgeUI = null;
        private BirthYearNumberController currentBirthYearNumber = null;

        public VerifyAgeState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            verifyAgeUI = CreateVerifyAgeUI();
            verifyAgeUI.HideSecurity();
            verifyAgeUI.Show();

            //設定事件
            verifyAgeUI.OnButtonPreviousClick = PreviousPage;
            verifyAgeUI.NumericKeypad.OnKeypadClick = InputNumber;
            verifyAgeUI.OnButtonClearClick = ResetNumbers;
            verifyAgeUI.OnButtonContinueClick = CheckBirthYearVaild;

            verifyAgeUI.SecurityUI.OnButtonCloseClick = verifyAgeUI.HideSecurity;

            HighlightBirthYearField();
        }

        public override void End()
        {
            ResetNumbers();
            verifyAgeUI.Hide();
        }

        private void NextPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.ACCOUNT);
        }

        private void PreviousPage()
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

            var isAdult = CheckAdultAge();

            if (isAdult)
            {
                //TODO: 送signup給Server
                NextPage();
            }
            else
            {
                //未滿18跳標語
                verifyAgeUI.DoPopUpSecurity();
            }
        }

        private bool CheckAdultAge()
        {
            return sceneManager.CheckAdultAge(GetBirthYearInputResult());
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
        protected virtual VerifyAgeUI CreateVerifyAgeUI()
        {
            var key = AddressablesLabels.CanvasVerifyAge;
            var ui = stateController.SceneManager.GetPage(key) as VerifyAgeUI;

            return ui;
        }
    }
}
