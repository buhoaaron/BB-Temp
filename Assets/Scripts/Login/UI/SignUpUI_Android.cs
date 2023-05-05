using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;
using TMPro;

namespace Barnabus.Login
{
    public class SignUpUI_Android : BaseLoginCommonUI
    {
        public UnityAction OnButtonGoogleClick = null;
        public UnityAction OnButtonFacebookClick = null;
        public UnityAction OnButtonPreviousClick = null;
        public UnityAction OnButtonCreateClick = null;

        [Header("Set UI Components")]
        public Button ButtonGoogle = null;
        public Button ButtonFacebook = null;
        public Button ButtonPrevious = null;
        public Button ButtonCreate = null;

        public TMP_InputField InputFieldEmail = null;
        public PasswordField InputFieldPassword = null;

        public Toggle ToggleOK = null;

        public override void Init()
        {
            ButtonGoogle.onClick.AddListener(ProcessButtonGoogleClick);
            ButtonFacebook.onClick.AddListener(ProcessButtonFacebookClick);
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.AddListener(ProcessButtonCreateClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonGoogle.onClick.RemoveListener(ProcessButtonGoogleClick);
            ButtonFacebook.onClick.RemoveListener(ProcessButtonFacebookClick);
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.RemoveListener(ProcessButtonCreateClick);
        }

        public SignUpInfo GetSignUpInfo()
        {
            var email = InputFieldEmail.text;
            var password = InputFieldPassword.text;

            return new SignUpInfo(email, password);
        }

        private void ProcessButtonGoogleClick()
        {
            OnButtonGoogleClick?.Invoke();
        }
        private void ProcessButtonFacebookClick()
        {
            OnButtonFacebookClick?.Invoke();
        }
        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }
        private void ProcessButtonCreateClick()
        {
            OnButtonCreateClick?.Invoke();
        }

        public bool CheckToggleStatus()
        {
            return ToggleOK.isOn;
        }
    }
}
