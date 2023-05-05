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
            newAccountSetUpUI = stateController.SceneManager.GetPage<NewAccountSetUpUI>(PAGE.NEW_ACCOUNT_SETUP);
            newAccountSetUpUI.Show();

            //設定下拉選單內容
            newAccountSetUpUI.DropdownGrade.AddOptions(sceneManager.ProfileManager.GetGradeList());
            newAccountSetUpUI.DropdownStateCurriculum.AddOptions(sceneManager.ProfileManager.GetStateCurriculumList());
            //設定按鈕事件
            newAccountSetUpUI.OnButtonPreviousClick = PreviousPage;
            newAccountSetUpUI.OnButtonCreateClick = SendCreatePlayer;
            newAccountSetUpUI.OnColorChanged = ChangeColor;
            newAccountSetUpUI.OnSkinChanged = ChangeSkin;

            sceneManager.NetworkManager.Dispatcher.OnReceiveCreatePlayer += OnSendCreatePlayerSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage += OnSendCreatePlayerFail;
        }

        private void ChangeColor(int index)
        {
            var id = index + 1;

            if (selectColorId != id)
            {
                selectColorId = id;

                //Refresh Skin Sprites
                UpdateSkinSprites(selectColorId);
            }
        }

        private void UpdateSkinSprites(int colorId)
        {
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
            stateController.SetState(LOGIN_SCENE_STATE.PARENTS_ONBOARDING);
        }

        public override void PreviousPage()
        {
            stateController.BackToPreviousState();
        }

        public override void End()
        {
            sceneManager.NetworkManager.Dispatcher.OnReceiveCreatePlayer -= OnSendCreatePlayerSuccess;
            sceneManager.NetworkManager.Dispatcher.OnReceiveErrorMessage -= OnSendCreatePlayerFail;

            newAccountSetUpUI.InputReset();
            newAccountSetUpUI.Hide();
        }

        #region SEND_CREATE_PLAYER
        private void SendCreatePlayer()
        {
            var userId = sceneManager.NetworkManager.NetworkInfo.Meandmineid;
            var isParentOwned = true;
            var firstName = newAccountSetUpUI.InputFieldFirstName.text;
            var lastName = newAccountSetUpUI.InputFieldLaseInitial.text;
            var grade = newAccountSetUpUI.DropdownGrade.captionText.text;
            var countryCode = "US";
            var state = newAccountSetUpUI.DropdownStateCurriculum.captionText.text;
            var avatar = new AvatarInfo(selectSkinId.ToString(), selectColorId.ToString(), "1");

            var data = new SendCreatePlayer(userId, isParentOwned, firstName, lastName, grade, countryCode, state, avatar);

            sceneManager.PostRequest(API_PATH.CreatePlayer, data);
        }
        private void OnSendCreatePlayerSuccess(ReceiveSignUp receiveSignUp)
        {
            var networkInfo = new NetworkInfo(receiveSignUp.meandmine_id, receiveSignUp.access_token);

            sceneManager.NetworkManager.UpdatePlayerNetworkInfo(networkInfo);

            NextPage();
        }

        private void OnSendCreatePlayerFail(ReceiveErrorMessage errorMessage)
        {
            stateController.SceneManager.DoShowErrorMessage(errorMessage.error);
        }
        #endregion
    }
}
