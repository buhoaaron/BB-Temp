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
            var key = AddressablesLabels.CanvasAccount;
            accountUI = stateController.SceneManager.GetPage<AccountUI>(key);
        }
    }
}
