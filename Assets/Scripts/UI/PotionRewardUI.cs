using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Barnabus.UI
{
    public class PotionRewardUI : BaseGameUI
    {
        public UnityAction OnButtonNextLevelClick = null;
        public UnityAction OnButtonBackMainClick = null;
        public UnityAction OnButtonReplayClick = null;

        private Button buttonNextLevel = null;
        private Button buttonBackMain = null;
        private Button buttonReplay = null;
        private TMP_Text textPotionValue = null;

        private POTION_REWARD_MODE mode = POTION_REWARD_MODE.TWO_BUTTONS;

        public void Init(POTION_REWARD_MODE mode)
        {
            this.mode = mode;

            Init();
        }

        public override void Init()
        {
            buttonNextLevel = root.Find("Box/Buttons/Button_NextLevel").GetComponent<Button>();
            buttonBackMain = root.Find("Box/Buttons/Button_BackMain").GetComponent<Button>();
            buttonReplay = root.Find("Box/Buttons/Button_Replay").GetComponent<Button>();
            textPotionValue = root.Find("Box/TMPText_PotionValue").GetComponent<TMP_Text>();

            buttons.Add(buttonBackMain);
            buttons.Add(buttonReplay);
            buttons.Add(buttonNextLevel);

            buttonBackMain.onClick.AddListener(ProcessButtonBackMainClick);
            buttonReplay.onClick.AddListener(ProcessButtonReplayClick);
            buttonNextLevel.onClick.AddListener(ProcessButtonNextLevelClick);

            InitLayout();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        private void InitLayout()
        {
            switch(mode)
            {
                case POTION_REWARD_MODE.TWO_BUTTONS:
                    LayoutTwoButtons();
                    break;

                case POTION_REWARD_MODE.THREE_BUTTONS:
                    LayoutThreeButtons();
                    break;
            }
        }

        private void LayoutTwoButtons()
        {
            buttonNextLevel.gameObject.SetActive(false);
            //Replay按鈕移到NextLevel按鈕的位置
            buttonReplay.transform.localPosition = buttonNextLevel.transform.localPosition;
        }

        private void LayoutThreeButtons()
        {

        }

        private void ProcessButtonBackMainClick()
        {
            OnButtonBackMainClick?.Invoke();
        }

        private void ProcessButtonReplayClick()
        {
            OnButtonReplayClick?.Invoke();
        }

        private void ProcessButtonNextLevelClick()
        {
            OnButtonNextLevelClick?.Invoke();
        }

        public void SetPotionValue(int value)
        {
            textPotionValue.text = value.ToString();
        }
    }
}
