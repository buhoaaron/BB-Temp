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
        }
        public override void NextPage()
        {

        }
        public override void PreviousPage()
        {

        }
    }
}
