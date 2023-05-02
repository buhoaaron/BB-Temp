using UnityEngine;
using Barnabus.Network;
using System.Collections.Generic;

namespace Barnabus.Login.StateControl
{
    public class NewAccountSetUpState : BaseLoginSceneState
    {
        private NewAccountSetUpUI newAccountSetUpUI = null;

        public NewAccountSetUpState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var key = AddressablesLabels.CanvasNewAccountSetUp;
            newAccountSetUpUI = stateController.SceneManager.GetPage<NewAccountSetUpUI>(key);
            newAccountSetUpUI.Show();

            //設定下拉選單內容
            newAccountSetUpUI.DropdownGrade.AddOptions(sceneManager.ProfileManager.GetGradeList());
            newAccountSetUpUI.DropdownStateCurriculum.AddOptions(sceneManager.ProfileManager.GetStateCurriculumList());

            newAccountSetUpUI.OnButtonPreviousClick = PreviousPage;
        }

        public override void NextPage()
        {

        }

        public override void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.CHOOSE_PROFILE);
        }

        public override void End()
        {
            newAccountSetUpUI.InputReset();
            newAccountSetUpUI.Hide();
        }
    }
}
