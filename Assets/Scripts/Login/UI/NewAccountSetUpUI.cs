using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq;

namespace Barnabus.Login
{
    public class NewAccountSetUpUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonPreviousClick = null;
        public UnityAction OnButtonCreateClick = null;

        [Header("Set UI Components")]
        public Button ButtonPrevious = null;
        public Button ButtonCreate = null;
        public TMP_InputField InputFieldFirstName = null;
        public TMP_InputField InputFieldLaseInitial = null;
        public TMP_Dropdown DropdownGrade = null;
        public TMP_Dropdown DropdownStateCurriculum = null;
        public ToggleGroup ToggleGroupColor = null;
        public ToggleGroup ToggleGroupSkin = null;

        public override void Init()
        {
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.AddListener(ProcessButtonCreateClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.RemoveListener(ProcessButtonCreateClick);
        }

        public void InputReset()
        {
            InputFieldFirstName.text = "";
            InputFieldLaseInitial.text = "";
            DropdownGrade.value = -1;
            DropdownStateCurriculum.value = -1;
        }
        
        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }

        private void ProcessButtonCreateClick()
        {
            OnButtonCreateClick?.Invoke();
        }
    }
}
