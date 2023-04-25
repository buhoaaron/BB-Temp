using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Barnabus.UI
{
    /// <summary>
    /// PWD開關
    /// </summary>
    [RequireComponent(typeof(UIButtonChangeSprite))]
    public class ShowPasswordSwitch : MonoBehaviour
    {
        public bool IsShowPassword { get; private set; } = false;
        public TMP_InputField Target = null;

        private Button button = null;
        private void Awake()
        {
            Debug.Assert(Target != null, "ShowPasswordSwitch: you have to set Target.");

            Target.asteriskChar = '●';

            button = GetComponent<Button>();
            button.onClick.AddListener(ProcessShowPassword);
        }

        private void ProcessShowPassword()
        {
            IsShowPassword = !IsShowPassword;

            Target.contentType = IsShowPassword ? TMP_InputField.ContentType.Alphanumeric
                                                : TMP_InputField.ContentType.Password;

            Target.ForceLabelUpdate();

            var changeSprite = button.GetComponent<UIButtonChangeSprite>();
            changeSprite.Change(System.Convert.ToInt16(IsShowPassword));
        }
    }
}
