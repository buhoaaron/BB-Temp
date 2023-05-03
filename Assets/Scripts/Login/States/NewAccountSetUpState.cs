using UnityEngine;
using Barnabus.Network;
using System.Collections.Generic;

namespace Barnabus.Login.StateControl
{
    public class NewAccountSetUpState : BaseLoginSceneState
    {
        private NewAccountSetUpUI newAccountSetUpUI = null;

        private int selectColorId = 1;
        private int selectSkinId = 1;

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

            newAccountSetUpUI.OnColorChanged = ChangeColor;
            newAccountSetUpUI.OnSkinChanged = ChangeSkin;
        }

        private void ChangeColor(int index)
        {
            var id = index + 1;

            if (selectColorId != id)
            {
                selectColorId = id;

                //更新Skin Sprite
                UpdateSkinSprites(selectColorId);
            }
        }

        private void UpdateSkinSprites(int colorId)
        {
            Debug.Log("更新Skin Sprite");

            for (int skinId = 1; skinId <= newAccountSetUpUI.ListToggleSkin.Count; skinId++)
            {
                var skinSprite = sceneManager.PlayerIcons.GetIcon(colorId, skinId);
                var toggle = newAccountSetUpUI.ListToggleSkin[skinId - 1];

                toggle.image.sprite = skinSprite;
            }
        }

        private void ChangeSkin(int index)
        {
            var id = index + 1;

            if (selectSkinId != id)
            {
                selectSkinId = id;
            }
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
