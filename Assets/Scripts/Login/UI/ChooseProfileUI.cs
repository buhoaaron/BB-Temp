using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Barnabus.Login
{
    public class ChooseProfileUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonParentDashboardClick = null;
        public UnityAction OnButtonPreviousClick = null;

        [Header("Set UI Components")]
        public Button ButtonParentDashboard = null;
        public Button ButtonPrevious = null;
        public RectTransform TransProfiles = null;

        private ProfileBuilder profileBuilder = null;
        private List<ProfileController> profileControllers = null;

        public override void Init()
        {
            profileControllers = new List<ProfileController>();

            profileBuilder = GetComponent<ProfileBuilder>();
            profileBuilder.Init();

            ButtonParentDashboard.onClick.AddListener(ProcessButtonParentDashboardClick);
            ButtonPrevious.onClick.AddListener(ProcessButtonPreviousClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
            ButtonParentDashboard.onClick.RemoveListener(ProcessButtonParentDashboardClick);
            ButtonPrevious.onClick.RemoveListener(ProcessButtonPreviousClick);
        }
        
        private void ProcessButtonParentDashboardClick()
        {
            OnButtonParentDashboardClick?.Invoke();
        }
        private void ProcessButtonPreviousClick()
        {
            OnButtonPreviousClick?.Invoke();
        }

        public List<ProfileController> CreateProfiles(int count)
        {
            var result = profileBuilder.Create(count);

            profileControllers.AddRange(result);

            return result;
        }

        public ProfileController CreateProfile()
        {
            var result = profileBuilder.CreateSingle();

            profileControllers.Add(result);

            return result;
        }

        public void DestroyAllProfile()
        {
            profileBuilder.Destroy();

            profileControllers.Clear();
        }
    }
}
