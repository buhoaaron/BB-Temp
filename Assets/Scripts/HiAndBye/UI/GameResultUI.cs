using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;
using TMPro;

namespace HiAndBye
{
    public class GameResultUI : BaseGameUI
    {
        public UnityAction OnButtonOKClick = null;

        private TMP_Text textCorrectHiNum = null;
        private TMP_Text textCorrectByeNum = null;
        private TMP_Text textIncorrect = null;
        private Button buttonOK = null;

        public override void Init()
        {
            textCorrectHiNum = root.Find("Score/Image_CorrectHi/Text_number").GetComponent<TMP_Text>();
            textCorrectByeNum = root.Find("Score/Image_CorrectBye/Text_number").GetComponent<TMP_Text>();
            textIncorrect = root.Find("Score/Text_Incorrect/Text_number").GetComponent<TMP_Text>();
            buttonOK = root.Find("OKButton").GetComponent<Button>();

            buttons.Add(buttonOK);

            buttonOK.onClick.AddListener(ProcessButtonOKClick);
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

        private void ProcessButtonOKClick()
        {
            OnButtonOKClick?.Invoke();
        }
    }
}
