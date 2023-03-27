using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Barnabus.UI;
using Spine.Unity;

namespace Barnabus.Shelf
{
    public class HubInfoUI : BaseGameUI
    {
        [Header("Set UI Component")]
        public Transform PotionRequire = null;
        public Transform PlayerPotions = null;

        public SkeletonGraphic SkeletonGraphicEgg = null;

        public Text TextPlayerPotionValue = null;
        public Text TextPotionRequire = null;
        public Text TextPotionRequireTip = null;
        public Text TextElement = null;

        public Button ButtonClose = null;
        public Button ButtonGameRoom = null;
        public Button ButtonUnlock = null;

        public UnityAction OnButtonCloseClick = null;
        public UnityAction OnButtonGameRoomClick = null;
        public UnityAction OnButtonUnlockClick = null;

        public HUB_STATE State;

        private Color32 colorNotEnoughPotion = new Color32(0xFF, 0x2B, 0x2B, 0xff);
        private Color32 colorEnoughPotion = Color.white;

        public void Init(HUB_STATE targetState)
        {
            State = targetState;

            Init();

            UpdateUI();
        }

        public override void Init()
        {
            buttons.Add(ButtonClose);
            buttons.Add(ButtonGameRoom);
            buttons.Add(ButtonUnlock);

            ButtonClose.onClick.AddListener(ProcessButtonCloseClick);
            ButtonGameRoom.onClick.AddListener(ProcessButtonGameRoomClick);
            ButtonUnlock.onClick.AddListener(ProcessButtonGameRoomClick);
        }
        public override void UpdateUI()
        {
            ButtonGameRoom.gameObject.SetActive(State == HUB_STATE.NOT_UNLOCK);
            ButtonUnlock.gameObject.SetActive(State == HUB_STATE.UNLOCK);

            TextPotionRequireTip.enabled = (State == HUB_STATE.NOT_UNLOCK);

            TextPotionRequire.color = (State == HUB_STATE.NOT_UNLOCK) ? colorNotEnoughPotion : colorEnoughPotion;
        }
        public override void Clear()
        {
            
        }

        public void SetPlayerPotions(int value)
        {
            TextPlayerPotionValue.text = string.Format("x{0}", value);
        }

        public void SetPotionRequire(int value)
        {
            TextPotionRequire.text = string.Format("x{0}", value);
        }

        public void SetElement(string element)
        {
            TextElement.text = element;
        }

        private void ProcessButtonCloseClick()
        {
            OnButtonCloseClick?.Invoke();

            Destroy();
        }

        private void ProcessButtonGameRoomClick()
        {
            OnButtonGameRoomClick?.Invoke();

            Destroy();
        }
        private void ProcessButtonUnlockClick()
        {
            OnButtonUnlockClick?.Invoke();

            Destroy();
        }
    }
}
