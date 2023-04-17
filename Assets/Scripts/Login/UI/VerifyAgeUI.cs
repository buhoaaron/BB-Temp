using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class VerifyAgeUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonPreviousClick = null;
        public UnityAction OnButtonContinueClick = null;
        public UnityAction OnButtonClearClick = null;

        [Header("Set UI Components")]
        public Button ButtonPrevious = null;
        public Button ButtonContinue = null;
        public Button ButtonClear = null;

        public NumericKeypad NumericKeypad = null;
        public VerifyAgeSecurityUI SecurityUI = null;
        public BirthYearUI BirthYearUI = null;
        public override void Init()
        {
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonContinue.onClick.AddListener(ProcessButtonContinueClick);
            ButtonClear.onClick.AddListener(ProcessButtonClearClick);

            NumericKeypad.Init();
            SecurityUI.Init();
            BirthYearUI.Init();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
            ButtonContinue.onClick.RemoveListener(ProcessButtonContinueClick);
            ButtonClear.onClick.RemoveListener(ProcessButtonClearClick);

            NumericKeypad.Clear();
            SecurityUI.Clear();
            BirthYearUI.Clear();
        }
        
        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }
        private void ProcessButtonContinueClick()
        {
            OnButtonContinueClick?.Invoke();
        }
        private void ProcessButtonClearClick()
        {
            OnButtonClearClick?.Invoke();
        }

        public void HideSecurity()
        {
            SecurityUI.Hide();
        }
        public void DoPopUpSecurity()
        {
            SecurityUI.Show();
            SecurityUI.DoPopUp();
        }
    }
}
