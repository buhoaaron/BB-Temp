using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;
using TMPro;

namespace Barnabus.Login
{
    public class LoginWithEmailUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonLoginClick = null;
        public UnityAction OnButtonPreviousClick = null;

        [Header("Set UI Components")]
        public Button ButtonLogin = null;
        public Button ButtonPrevious = null;

        public TMP_InputField InputFieldEmail = null;
        public TMP_InputField InputFieldPassword = null;

        public override void Init()
        {
            ButtonLogin.onClick.AddListener(ProcessButtonLoginClick);
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonLogin.onClick.RemoveListener(ProcessButtonLoginClick);
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
        }

        public LoginInfo GetLoginInfo()
        {
            var email = InputFieldEmail.text;
            var password = InputFieldPassword.text;

            return new LoginInfo(email, password);
        }

        private void ProcessButtonLoginClick()
        {
            OnButtonLoginClick?.Invoke();
        }

        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }
    }
}
