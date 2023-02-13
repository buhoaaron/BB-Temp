using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Barnabus.EmotionMusic
{
    public class DataInfo : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI dataName;
        [SerializeField]
        private TextMeshProUGUI lastModifyDate;
        [SerializeField]
        private TMP_InputField renameInput;
        [SerializeField]
        private TextMeshProUGUI defaultName;
        [SerializeField]
        private Image checkBox;
        [SerializeField]
        private Sprite uncheckSprite;
        [SerializeField]
        private Sprite checkedSprite;

        [SerializeField]
        private List<Image> barnabusDisplays;

        private int id;
        private Action<int> OnClickCallback;

        public void SetID(int id) { this.id = id; }

        public void SetInfo(string dataName, string lastModifyDate)
        {
            this.dataName.text = dataName;
            this.lastModifyDate.text = lastModifyDate;
            renameInput.gameObject.SetActive(false);
        }

        public void SetOnClickCallback(Action<int> OnClickCallback) { this.OnClickCallback = OnClickCallback; }

        public void StartRename()
        {
            renameInput.text = dataName.text;
            defaultName.text = dataName.text;
            renameInput.gameObject.SetActive(true);
        }

        public void CancelRename()
        {
            renameInput.gameObject.SetActive(false);
        }

        public void CompleteRename()
        {
            dataName.text = GetNewName();
            renameInput.gameObject.SetActive(false);
        }

        public string GetNewName()
        {
            if (renameInput.text != "") return renameInput.text;
            else return dataName.text;
        }

        public void OnClick()
        {
            OnClickCallback?.Invoke(id);
        }

        public void SetSelected(bool isSelected)
        {
            if (isSelected) checkBox.sprite = checkedSprite;
            else checkBox.sprite = uncheckSprite;
        }

        public void SetCharacter(int index, Character character)
        {
            if (character == null) barnabusDisplays[index].gameObject.SetActive(false);
            else
            {
                barnabusDisplays[index].gameObject.SetActive(true);
                barnabusDisplays[index].sprite = character.icon;
            }
        }
    }
}