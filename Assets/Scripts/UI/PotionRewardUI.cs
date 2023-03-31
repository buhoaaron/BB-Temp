using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Barnabus.UI
{
    public class PotionRewardUI : BaseGameUI
    {
        public UnityAction OnButtonBackMainClick = null;
        public UnityAction OnButtonReplayClick = null;

        private Button buttonBackMain = null;
        private Button buttonReplay = null;
        private TMP_Text textPotionValue = null;

        public override void Init()
        {
            buttonBackMain = root.Find("Box/Buttons/Button_BackMain").GetComponent<Button>();
            buttonReplay = root.Find("Box/Buttons/Button_Replay").GetComponent<Button>();
            textPotionValue = root.Find("Box/TMPText_PotionValue").GetComponent<TMP_Text>();

            buttons.Add(buttonBackMain);
            buttons.Add(buttonReplay);

            buttonBackMain.onClick.AddListener(ProcessButtonBackMainClick);
            buttonReplay.onClick.AddListener(ProcessButtonReplayClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
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

        public void SetPotionValue(int value)
        {
            textPotionValue.text = value.ToString();
        }
    }
}
