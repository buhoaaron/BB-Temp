using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class ParentsOnboardingUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonStartClick = null;

        [Header("Set UI Components")]
        public Button ButtonStart = null;

        public override void Init()
        {
            ButtonStart.onClick.AddListener(ProcessButtonStartClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonStart.onClick.RemoveListener(ProcessButtonStartClick);
        }

        private void ProcessButtonStartClick()
        {
            OnButtonStartClick?.Invoke();
        }
    }
}
