using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace HiAndBye
{
    public class GameResultUI : BaseGameUI
    {
        public UnityAction OnButtonBackMainClick = null;
        public UnityAction OnButtonReplayClick = null;

        private Text textCorrectHiNum = null;
        private Text textCorrectByeNum = null;
        private Text textIncorrect = null;
        private Button buttonBackMain = null;
        private Button buttonOK = null;

        public override void Init()
        {
            textCorrectHiNum = root.Find("Score/Text_CorrectHi/Text_number").GetComponent<Text>();
            textCorrectByeNum = root.Find("Score/Text_CorrectBye/Text_number").GetComponent<Text>();
            textIncorrect = root.Find("Score/Text_Incorrect/Text_number").GetComponent<Text>();
            buttonBackMain = root.Find("QuitButton").GetComponent<Button>();
            buttonOK = root.Find("OKButton").GetComponent<Button>();

            buttons.Add(buttonBackMain);
            buttons.Add(buttonOK);

            ResetPopUp();

            buttonOK.onClick.AddListener(ProcessButtonReplayClick);
            buttonBackMain.onClick.AddListener(ProcessButtonBackMainClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
        public void SetScore(int correctHiNum, int correctByeNum, int incorrectNum)
        {
            textCorrectHiNum.text = correctHiNum.ToString();
            textCorrectByeNum.text = correctByeNum.ToString();
            textIncorrect.text = incorrectNum.ToString();
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
