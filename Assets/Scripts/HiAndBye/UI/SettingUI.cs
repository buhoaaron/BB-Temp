using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace HiAndBye
{
    public class SettingUI : BaseGameUI
    {
        public UnityAction OnButtonYesClick = null;
        public UnityAction OnButtonNoClick = null;

        private Button buttonYes = null;
        private Button buttonNo = null;

        public override void Init()
        {
            buttonYes = root.Find("Box/BackHomeButton").GetComponent<Button>();
            buttonNo = root.Find("Box/GameResume").GetComponent<Button>();

            buttons.Add(buttonYes);
            buttons.Add(buttonNo);

            buttonYes.onClick.AddListener(ProcessButtonYesClick);
            buttonNo.onClick.AddListener(ProcessButtonNoClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        private void ProcessButtonYesClick()
        {
            OnButtonYesClick?.Invoke();
        }

        private void ProcessButtonNoClick()
        {
            OnButtonNoClick?.Invoke();
        }
    }
}
