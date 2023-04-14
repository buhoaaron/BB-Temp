using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace Barnabus.Login
{
    public class VerifyAgeSecurityUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonCloseClick = null;

        [Header("Set UI Components")]
        public Button ButtonClose = null;

        public override void Init()
        {
            ButtonClose.onClick.AddListener(ProcessButtonCloseClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        private void ProcessButtonCloseClick()
        {
            OnButtonCloseClick?.Invoke();
        }
    }
}
