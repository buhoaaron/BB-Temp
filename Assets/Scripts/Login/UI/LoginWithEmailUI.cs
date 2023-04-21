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

        [Header("Set UI Components")]
        public Button ButtonLogin = null;

        public TMP_InputField InputFieldEmail = null;
        public TMP_InputField InputFieldPassword = null;

        public override void Init()
        {
            ButtonLogin.onClick.AddListener(ProcessButtonLoginClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonLogin.onClick.RemoveListener(ProcessButtonLoginClick);
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
    }
}
