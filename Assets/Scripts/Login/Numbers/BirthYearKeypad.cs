using Barnabus.UI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

namespace Barnabus.Login
{
    public class BirthYearKeypad : BaseGameUI
    {
        public UnityAction<int> OnInputCompleted = null;

        public Button ButtonClear = null;
        public NumericKeypad NumericKeypad = null;
        public BirthYearUI BirthYearUI = null;

        private BirthYearNumberController currentBirthYearNumber = null;

        #region BASE_API
        public override void Init()
        {
            NumericKeypad.Init();
            BirthYearUI.Init();

            ButtonClear.onClick.AddListener(ProcessButtonClearClick);

            NumericKeypad.OnKeypadClick = InputNumber;

            HighlightBirthYearField();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            NumericKeypad.Clear();
            BirthYearUI.Clear();

            ButtonClear.onClick.RemoveListener(ProcessButtonClearClick);
        }
        #endregion

        private void ProcessButtonClearClick()
        {
            ResetNumbers();
        }

        private void InputNumber(int number)
        {
            BirthYearUI.SetNumber(number);

            var controller = HighlightBirthYearField();

            //是否輸入完成
            if (controller == null)
                OnInputCompleted?.Invoke(GetBirthYearInputResult());
        }

        public void ResetNumbers()
        {
            BirthYearUI.ResetNumbers();

            HighlightBirthYearField();
        }

        /// <summary>
        /// 高光使用者即將輸入的欄位
        /// </summary>
        private BirthYearNumberController HighlightBirthYearField()
        {
            currentBirthYearNumber?.ChangeSwitchSprite(false);
            currentBirthYearNumber = BirthYearUI.CheckControllerEmpty();
            currentBirthYearNumber?.ChangeSwitchSprite(true);

            return currentBirthYearNumber;
        }

        public int GetBirthYearInputResult()
        {
            var controllers = BirthYearUI.Controllers;

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

        public bool CheckBirthYearVaild()
        {
            if (currentBirthYearNumber != null)
            {
                //振動提示
                currentBirthYearNumber.DoShake();
                return false;
            }

            return true;
        }
    }
}
