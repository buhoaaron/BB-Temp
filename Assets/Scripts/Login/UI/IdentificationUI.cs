using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Barnabus.UI;

namespace Barnabus.Login
{
    public class IdentificationUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonFamiliesClick = null;
        public UnityAction OnButtonEducatorsClick = null;
        public UnityAction OnButtonStudentsClick = null;
        public UnityAction OnButtonHaveAccountClick = null;

        [Header("Set UI Components")]
        public Button ButtonFamilies = null;
        public Button ButtonEducators = null;
        public Button ButtonStudents = null;
        public Button ButtonHaveAccount = null;

        public Button ButtonSkip = null;

        public override void Init()
        {
            buttons.Add(ButtonFamilies);
            buttons.Add(ButtonEducators);
            buttons.Add(ButtonStudents);
            buttons.Add(ButtonHaveAccount);

            ButtonFamilies.onClick.AddListener(ProcessButtonFamiliesClick);
            ButtonEducators.onClick.AddListener(ProcessButtonEducatorsClick);
            ButtonStudents.onClick.AddListener(ProcessButtonStudentsClick);
            ButtonHaveAccount.onClick.AddListener(ProcessButtonHaveAccountClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonFamilies.onClick.RemoveListener(ProcessButtonFamiliesClick);
            ButtonEducators.onClick.RemoveListener(ProcessButtonEducatorsClick);
            ButtonStudents.onClick.RemoveListener(ProcessButtonStudentsClick);
            ButtonHaveAccount.onClick.RemoveListener(ProcessButtonHaveAccountClick);
        }
        
        private void ProcessButtonFamiliesClick()
        {
            OnButtonFamiliesClick?.Invoke();
        }
        private void ProcessButtonEducatorsClick()
        {
            OnButtonEducatorsClick?.Invoke();
        }
        private void ProcessButtonStudentsClick()
        {
            OnButtonStudentsClick?.Invoke();
        }
        private void ProcessButtonHaveAccountClick()
        {
            OnButtonHaveAccountClick?.Invoke();
        }
    }
}
