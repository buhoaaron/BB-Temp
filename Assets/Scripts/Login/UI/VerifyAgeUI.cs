using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class VerifyAgeUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonPreviousClick = null;
        public UnityAction OnButtonContinueClick = null;

        [Header("Set UI Components")]
        public Button ButtonPrevious = null;
        public Button ButtonContinue = null;

        public BirthYearKeypad BirthYearKeypad = null;
        public VerifyAgeSecurityUI SecurityUI = null;

        public override void Init()
        {
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonContinue.onClick.AddListener(ProcessButtonContinueClick);

            BirthYearKeypad.Init();
            SecurityUI.Init();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
            ButtonContinue.onClick.RemoveListener(ProcessButtonContinueClick);

            BirthYearKeypad.Clear();
            SecurityUI.Clear();
        }
        
        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }
        private void ProcessButtonContinueClick()
        {
            OnButtonContinueClick?.Invoke();
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
