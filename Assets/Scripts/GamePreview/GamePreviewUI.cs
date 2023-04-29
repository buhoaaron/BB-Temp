using UnityEngine.Events;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

namespace Barnabus.UI
{
    public class GamePreviewUI : BaseGameUI
    {
        public UnityAction OnButtonStartClick = null;

        public MediaPlayer MediaPlayer = null;
        public DisplayUGUI Display = null;

        public Button ButtonStart = null;

        public override void Init()
        {
            buttons.Add(ButtonStart);

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
