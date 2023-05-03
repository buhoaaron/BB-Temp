using UnityEngine;
using Barnabus.Network;
using System.Collections.Generic;

namespace Barnabus.Login.StateControl
{
    public class ParentsOnboardingState : BaseLoginSceneState
    {
        private ParentsOnboardingUI parentsOnboardingUI = null;

        public ParentsOnboardingState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            parentsOnboardingUI = stateController.SceneManager.GetPage<ParentsOnboardingUI>(PAGE.PARENTS_ONBOARDING);
            parentsOnboardingUI.Show();

            parentsOnboardingUI.OnButtonStartClick = CompleteLogin;
        }

        private void CompleteLogin()
        {
            stateController.SceneManager.CompleteLogin();
        }

        public override void NextPage()
        {

        }

        public override void PreviousPage()
        {
            //stateController.SetState(LOGIN_SCENE_STATE.CHOOSE_PROFILE);
        }

        public override void End()
        {
            parentsOnboardingUI.Hide();
        }
    }
}
