using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;
using TMPro;
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
        public Button ButtonShowPassword = null;

        public TMP_InputField InputFieldEmail = null;
        public TMP_InputField InputFieldPassword = null;

        public Toggle ToggleOK = null;

        private bool isShowPassword = false;

        public override void Init()
        {
            ButtonGoogle.onClick.AddListener(ProcessButtonGoogleClick);
            ButtonFacebook.onClick.AddListener(ProcessButtonFacebookClick);
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.AddListener(ProcessButtonCreateClick);

            ButtonShowPassword.onClick.AddListener(ProcessShowPassword);
            InputFieldPassword.asteriskChar = '●';
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonShowPassword.onClick.RemoveListener(ProcessShowPassword);
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

        private void ProcessShowPassword()
        {
            isShowPassword = !isShowPassword;

            InputFieldPassword.contentType = isShowPassword ? TMP_InputField.ContentType.Alphanumeric
                                                            : TMP_InputField.ContentType.Password;

            InputFieldPassword.ForceLabelUpdate();

            var changeSprite = ButtonShowPassword.GetComponent<UIButtonChangeSprite>();
            changeSprite.Change(System.Convert.ToInt16(isShowPassword));
        }

        public bool CheckToggleStatus()
        {
            return ToggleOK.isOn;
        }
    }
}
