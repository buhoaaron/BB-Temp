using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace HiAndBye
{
    public class GameResultUI : BaseGameUI
    {
        public UnityAction OnButtonBackMainClick = null;
        public UnityAction OnButtonReplayClick = null;

        private Text textCorrect = null;
        private Text textIncorrect = null;
        private Button buttonBackMain = null;
        private Button buttonReplay = null;

        public override void Init()
        {
            textCorrect = root.Find("Score/Text_Correct").GetComponent<Text>();
            textIncorrect = root.Find("Score/Text_Incorrect").GetComponent<Text>();
            buttonBackMain = root.Find("QuitButton").GetComponent<Button>();
            buttonReplay = root.Find("ReplayButton").GetComponent<Button>();

            buttons.Add(buttonBackMain);
            buttons.Add(buttonReplay);

            ResetPopUp();

            buttonReplay.onClick.AddListener(ProcessButtonReplayClick);
            buttonBackMain.onClick.AddListener(ProcessButtonBackMainClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
        public void SetScore(int correctNum, int incorrectNum)
        {
            textCorrect.text = string.Format("Correct: {0}", correctNum);
            textIncorrect.text = string.Format("Incorrect: {0}", incorrectNum);
        }

        private void ProcessButtonBackMainClick()
        {
            OnButtonBackMainClick?.Invoke();
        }

        private void ProcessButtonReplayClick()
        {
            OnButtonReplayClick?.Invoke();
        }
    }
}
