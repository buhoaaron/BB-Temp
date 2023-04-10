using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public class NavigationBarUI : BaseGameUI
    {
        [Header("Set UI Components")]
        public Button ButtonHome = null;
        public Button ButtonShop = null;
        public Button ButtonPlayer = null;
        public Button ButtonSound = null;
        public Button ButtonSwitch = null;

        public UnityAction OnButtonHomeClick = null;
        public UnityAction OnButtonShopClick = null;
        public UnityAction OnButtonPlayerClick = null;
        public UnityAction<bool> OnButtonSoundClick = null;
        public UnityAction OnButtonSwitchClick = null;

        private bool isOn = true;

        public void Init(bool isOn)
        {
            Init();

            this.isOn = isOn;

            ChangeButtonSoundSprite(isOn);
        }

        public override void Init()
        {
            buttons.Add(ButtonHome);
            buttons.Add(ButtonShop);
            buttons.Add(ButtonPlayer);
            buttons.Add(ButtonSound);
            buttons.Add(ButtonSwitch);

            ButtonHome.onClick.AddListener(ProcessButtonHomeClick);
            ButtonShop.onClick.AddListener(ProcessButtonShopClick);
            ButtonPlayer.onClick.AddListener(ProcessButtonPlayerClick);
            ButtonSound.onClick.AddListener(ProcessButtonSoundClick);
            ButtonSwitch.onClick.AddListener(ProcessButtonSwitchClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        private void ProcessButtonHomeClick()
        {
            OnButtonHomeClick?.Invoke();
        }
        private void ProcessButtonShopClick()
        {
            OnButtonShopClick?.Invoke();
        }
        private void ProcessButtonPlayerClick()
        {
            OnButtonPlayerClick?.Invoke();
        }

        private void ProcessButtonSoundClick()
        {
            isOn = !isOn;

            ChangeButtonSoundSprite(isOn);

            OnButtonSoundClick?.Invoke(isOn);
        }

        private void ChangeButtonSoundSprite(bool isOn)
        {
            var changeSprite = ButtonSound.GetComponent<UIButtonChangeSprite>();
            var soundOnSpriteIndex = 0;
            var soundOffSpriteIndex = 1;
            changeSprite.Change(isOn ? soundOnSpriteIndex : soundOffSpriteIndex);
        }

        private void ProcessButtonSwitchClick()
        {
            OnButtonSwitchClick?.Invoke();
        }


    }
}
