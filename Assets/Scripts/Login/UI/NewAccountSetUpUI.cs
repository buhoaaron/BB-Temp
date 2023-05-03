using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

namespace Barnabus.Login
{
    public class NewAccountSetUpUI : BaseLoginCommonUI
    {
        public List<Toggle> ListToggleSkin => listToggleSkin;

        public UnityAction OnButtonPreviousClick = null;
        public UnityAction OnButtonCreateClick = null;
        public UnityAction<int> OnColorChanged = null;
        public UnityAction<int> OnSkinChanged = null;

        [Header("Set UI Components")]
        public Button ButtonPrevious = null;
        public Button ButtonCreate = null;
        public TMP_InputField InputFieldFirstName = null;
        public TMP_InputField InputFieldLaseInitial = null;
        public TMP_Dropdown DropdownGrade = null;
        public TMP_Dropdown DropdownStateCurriculum = null;
        public ToggleGroup ToggleGroupColor = null;
        public ToggleGroup ToggleGroupSkin = null;

        private List<Toggle> listToggleColor = null;
        private List<Toggle> listToggleSkin = null;

        public override void Init()
        {
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
            ButtonCreate.onClick.AddListener(ProcessButtonCreateClick);

            listToggleColor = new List<Toggle>(ToggleGroupColor.GetComponentsInChildren<Toggle>());
            listToggleSkin = new List<Toggle>(ToggleGroupSkin.GetComponentsInChildren<Toggle>());

            foreach(var toggle in listToggleColor)
                toggle.onValueChanged.AddListener(ProcessColorChanged);

            foreach (var toggle in listToggleSkin)
                toggle.onValueChanged.AddListener(ProcessSkinChanged);
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
        private void ProcessColorChanged(bool isOn)
        {
            if (!isOn) return;

            var toggleOn = ToggleGroupColor.GetFirstActiveToggle();
            var index =  listToggleColor.IndexOf(toggleOn);
            OnColorChanged?.Invoke(index);
        }
        private void ProcessSkinChanged(bool isOn)
        {
            if (!isOn) return;

            var toggleOn = ToggleGroupSkin.GetFirstActiveToggle();
            var index = listToggleSkin.IndexOf(toggleOn);
            OnSkinChanged?.Invoke(index);
        }
    }
}
