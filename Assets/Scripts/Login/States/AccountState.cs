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
            accountUI = CreateVerifyAgeUI();
            accountUI.Init();
        }

        protected virtual AccountUI CreateVerifyAgeUI()
        {
            var key = AddressablesLabels.CanvasAccount;
            var ui = stateController.SceneManager.GetPage(key) as AccountUI;

            return ui;
        }
    }
}
