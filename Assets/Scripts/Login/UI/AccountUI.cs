using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class AccountUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonNewAccountClick = null;
        public UnityAction OnButtonHasAnAccountClick = null;

        [Header("Set UI Components")]
        public Button ButtonNewAccount = null;
        public Button ButtonHasAnAccount = null;

        public override void Init()
        {
            ButtonNewAccount.onClick.AddListener(ProcessButtonNewAccountClick);
            ButtonHasAnAccount.onClick.AddListener(ProcessButtonHasAnAccountClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonNewAccount.onClick.RemoveListener(ProcessButtonNewAccountClick);
            ButtonHasAnAccount.onClick.RemoveListener(ProcessButtonHasAnAccountClick);
        }
        
        private void ProcessButtonNewAccountClick()
        {
            OnButtonNewAccountClick?.Invoke();
        }
        private void ProcessButtonHasAnAccountClick()
        {
            OnButtonHasAnAccountClick?.Invoke();
        }
    }
}
