using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    public class NameController : MonoBehaviour
    {
        [SerializeField]
        private EmotionFaceController controller;
       /* [SerializeField]
        private GameObject nameSelector;*/

        [Space(10)]
       /* [SerializeField]
        private string prefix;
        [SerializeField]
        private string suffix;*/
        [SerializeField]
        private List<string> wordList;

        [Header("NameButton")]
        [SerializeField]
        private NameButton nameButtonPrefab;
        [SerializeField]
        private Transform nameButtonContainer;
        [SerializeField]
        private GameObject confirmButton;
        /* [SerializeField]
         private Sprite selectedSprite;
         [SerializeField]
         private Sprite unselectedSprite;*/
        [SerializeField]
        private Color buttonLockedColor;

        private List<NameButton> nameButtons = new();
        private NameButton selectedNameButton = null;

        private int moodQuestLevel;
        public int CurrentMoodQuest { get; set; }

        private void Start()
        {
            moodQuestLevel = DataManager.MoodQuestLevel;
            GenerateNameButtons();
            confirmButton.SetActive(false);
            OnClick_NameButton(nameButtons[0]);
            
            //HideNameSelector();
        }

      /*  public void ShowNameSelector() { nameSelector.SetActive(true); }
        public void HideNameSelector() { nameSelector.SetActive(false); }*/

        public void OnClick_ConfirmSelect()
        {
            controller.SetFileName(selectedNameButton.text.text);
            //HideNameSelector();
        }

        public void GenerateNameButtons()
        {
            controller.ClearList();

            for (int i = 0; i < nameButtons.Count; i++) Destroy(nameButtons[i].gameObject);
            nameButtons.Clear();

            NameButton nameButton;
            for (int i = 0; i < wordList.Count; i++)
            {
                nameButton = Instantiate(nameButtonPrefab, nameButtonContainer);
                nameButton.backgroundImage.color = Color.clear;
                nameButton.id = i;
                nameButton.SetText(/*prefix + suffix + */wordList[i]);
                nameButton.SetOnClick(OnClick_NameButton);
                if (!IsNameButtonUnlocked(i))
                {
                    nameButton.SetEnable(false);
                    nameButton.text.color = buttonLockedColor;
                }
                nameButtons.Add(nameButton);
            }
        }

        private void OnClick_NameButton(NameButton button)
        {
            if (selectedNameButton) selectedNameButton.backgroundImage.color = Color.clear;

            CurrentMoodQuest = button.id;
            selectedNameButton = button;
            selectedNameButton.backgroundImage.color = Color.yellow;

            confirmButton.SetActive(true);

            
            controller.SetFileName(selectedNameButton.text.text);
        }

        private bool IsNameButtonUnlocked(int buttonIndex)
        {
            return (buttonIndex <= moodQuestLevel);
        }
    }
}