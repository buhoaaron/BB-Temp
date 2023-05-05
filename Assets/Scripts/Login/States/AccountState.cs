using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class AccountState : BaseLoginSceneState
    {
        private AccountUI accountUI = null;

        public AccountState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            accountUI = stateController.SceneManager.GetPage<AccountUI>(PAGE.ACCOUNT);
            accountUI.Show();

            accountUI.OnButtonNewAccountClick = GoCreateProfile;
        }

        private void GoCreateProfile()
        {
            stateController.SetState(LOGIN_SCENE_STATE.CREATE_PROFILE);
        }

        public override void NextPage()
        {

        }
        public override void PreviousPage()
        {

        }

        public override void End()
        {
            accountUI.Hide();
        }
    }
}
