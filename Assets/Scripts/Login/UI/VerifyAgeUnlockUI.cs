using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class VerifyAgeUnlockUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonCloseClick = null;

        [Header("Set UI Components")]
        public Button ButtonClose = null;

        public BirthYearKeypad BirthYearKeypad = null;

        public override void Init()
        {
            ButtonClose.onClick.AddListener(ProcessButtonCloseClick);

            BirthYearKeypad.Init();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonClose.onClick.RemoveListener(ProcessButtonCloseClick);

            BirthYearKeypad.Clear();
        }
       
        private void ProcessButtonCloseClick()
        {
            OnButtonCloseClick?.Invoke();
        }
    }
}
