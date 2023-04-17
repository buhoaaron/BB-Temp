using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace Barnabus.Login
{
    public class SignUpUI_iOS : SignUpUI_Android
    {
        public UnityAction OnButtonAppleClick = null;

        public Button ButtonApple = null;

        public override void Init()
        {
            base.Init();

            buttons.Add(ButtonApple);

            ButtonApple.onClick.AddListener(ProcessButtonAppleClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            base.Clear();

            ButtonApple.onClick.RemoveListener(ProcessButtonAppleClick);
        }
        private void ProcessButtonAppleClick()
        {
            OnButtonAppleClick?.Invoke();
        }
    }
}
