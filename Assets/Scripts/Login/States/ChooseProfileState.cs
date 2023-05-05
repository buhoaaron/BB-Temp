using UnityEngine;
using Barnabus.Network;
using System.Collections.Generic;

namespace Barnabus.Login.StateControl
{
    public class ChooseProfileState : BaseLoginSceneState
    {
        private const int MAX_PROFILES = 5;
        private ChooseProfileUI chooseProfileUI = null;
        public ChooseProfileState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            var parentInfo = stateController.SceneManager.NetworkManager.NetworkInfo;
            chooseProfileUI = stateController.SceneManager.GetPage<ChooseProfileUI>(PAGE.CHOOSE_PROFILE);
            chooseProfileUI.Show();

            //設定事件
            chooseProfileUI.OnButtonParentDashboardClick = GotoDashboard;
            chooseProfileUI.OnButtonPreviousClick = PreviousPage;

            //創建玩家Profiles
            CreateProfiles(parentInfo);

            //表演Profiles
            chooseProfileUI.DoShowProfiles();
        }

        public override void StateUpdate()
        {

        }

        public override void NextPage()
        {
            
        }

        public override void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.LOGIN);
        }

        private void GotoDashboard()
        {

        }

        private void GotoSetUpAccount()
        {
            stateController.SetState(LOGIN_SCENE_STATE.CREATE_PROFILE);
        }

        public override void End()
        {
            chooseProfileUI.DestroyAllProfile();
            chooseProfileUI.Hide();
        }

        /// <summary>
        /// 創建帳號擁有的profiles物件及狀態
        /// </summary>
        private void CreateProfiles(NetworkInfo playerInfo)
        {
            CreateNormalProfiles(playerInfo);
            CreateAddProfile(playerInfo);
        }

        private void CreateNormalProfiles(NetworkInfo playerInfo)
        {
            var profileCount = playerInfo.GetProfileCount();
            var profileControllers = chooseProfileUI.CreateProfiles(profileCount);

            //設定狀態
            foreach(var controller in profileControllers)
            {
                int index = profileControllers.IndexOf(controller);

                var info = playerInfo.Profiles[index];
                info.SpriteIcon = sceneManager.PlayerIcons.GetIcon(info.color_id, info.skin_id);

                controller.SetState(PROFILE_STATE.NORMAL, info);
                controller.OnButtonClick = CompleteLogin;
            }
        }
        private void CompleteLogin()
        {
            sceneManager.CompleteLogin();
        }

        private void CreateAddProfile(NetworkInfo playerInfo)
        {
            var canAdd = CheckProfileAdd(playerInfo.GetProfileCount());

            if (!canAdd)
                return;

            var profileController = chooseProfileUI.CreateProfile();

            //設定狀態
            profileController.SetState(PROFILE_STATE.ADD);
            profileController.OnButtonClick = GotoSetUpAccount;
        }

        /// <summary>
        /// 是否還能夠Add profile
        /// </summary>
        /// <returns></returns>
        private bool CheckProfileAdd(int profileCount)
        {
            return profileCount < MAX_PROFILES;
        }
    }
}
