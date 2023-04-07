using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;
using Spine.Unity;
using TMPro;

using DG.Tweening;

namespace HiAndBye
{
    public class GameRootUI : BaseGameUI
    {
        public UnityAction<QUESTION_TYPE> OnButtonHiClick = null;
        public UnityAction<QUESTION_TYPE> OnButtonByeClick = null;
        public UnityAction OnButtonBackMainClick = null;
        public Button ButtonHi => buttons[0];
        public Button ButtonBye => buttons[1];
        public Button buttonBackMain => buttons[2];

        private readonly int dropDownDistanceY = 1000;
        private readonly int dropDownPosY = -400;
        private readonly float dropDownDuration = 0.8f;

        private TMP_Text textTime = null;
        private TMP_Text textVocab = null;
        private TMP_Text textCorrectNum = null;
        private SkeletonGraphic skeletonGraphicBarnabus = null;
        private Text debugTextAnswer = null;
        private RectTransform transCharacter = null;

        private Vector2 transCharacterInitPos = Vector2.zero;

        public override void Init()
        {
            var buttonHi = transform.Find("Buttons/Button_Hi").GetComponent<Button>();
            var buttonBye = transform.Find("Buttons/Button_Bye").GetComponent<Button>();
            var buttonBackMain = transform.Find("Buttons/Button_BackMain").GetComponent<Button>();

            textTime = transform.Find("Time/TMPText_Value").GetComponent<TMP_Text>();
            textVocab = transform.Find("Vocab/TMPText_Content").GetComponent<TMP_Text>();
            textCorrectNum = transform.Find("NumberOfCorrect/TMPText_Count").GetComponent<TMP_Text>();
            transCharacter = transform.Find("CharacterWithMask/Character").GetComponent<RectTransform>();
            skeletonGraphicBarnabus = transform.Find("CharacterWithMask/Character/SkeletonGraphic_Char").GetComponent<SkeletonGraphic>();

            debugTextAnswer = transform.Find("Debug/Text_Answer").GetComponent<Text>();

            transCharacterInitPos = transCharacter.localPosition;

            buttons.Add(buttonHi);
            buttons.Add(buttonBye);
            buttons.Add(buttonBackMain);

            buttonHi.onClick.AddListener(ProcessButtonHiClick);
            buttonBye.onClick.AddListener(ProcessButtonByeClick);
            buttonBackMain.onClick.AddListener(ProcessButtonBackMainClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void ResetTextTime()
        {
            UITool.SetAlpha(textTime, 0);
        }

        public void ResetCharacter()
        {
            textVocab.rectTransform.localScale = Vector3.zero;
            transCharacter.localPosition = new Vector2(transCharacterInitPos.x, transCharacterInitPos.y + dropDownDistanceY);
        }
        private void ProcessButtonHiClick()
        {
            OnButtonHiClick?.Invoke(QUESTION_TYPE.HI);
        }
        private void ProcessButtonByeClick()
        {
            OnButtonByeClick?.Invoke(QUESTION_TYPE.BYE);
        }
        private void ProcessButtonBackMainClick()
        {
            OnButtonBackMainClick?.Invoke();
        }
        public void DoDropDown(TweenCallback onComplete)
        {
            ResetCharacter();

            var tweener1 = transCharacter.DOLocalMoveY(dropDownPosY, dropDownDuration).SetEase(Ease.OutBounce);
            tweener1.onComplete = onComplete;

            var tweener2 = textVocab.rectTransform.DOScale(1, 0.3f);
        }
        public void DoShowTime(TweenCallback onComplete = null)
        {
            var tweener = textTime.DOFade(1, 0.2f);
            tweener.onComplete = onComplete;
        }
        public void SetTextTime(string value)
        {
            textTime.text = value;
        }

        public void SetTextVocab(string value)
        {
            textVocab.text = value;
        }
        public void SetTextCorrectNum(int value)
        {
            textCorrectNum.text = string.Format("Correct: {0}", value);
        }
        public void SetBarnabusSpine(SkeletonDataAsset asset, string face = "Anger")
        {
            skeletonGraphicBarnabus.skeletonDataAsset = asset;
            skeletonGraphicBarnabus.initialSkinName = face;
            skeletonGraphicBarnabus.Initialize(true);
        }
        public void SetDebugTextAnswer(string value)
        {
            debugTextAnswer.text = string.Format("Answer: {0}", value);
        }
    }
}
