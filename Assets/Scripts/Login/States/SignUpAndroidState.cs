using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public class SignUpAndroidState : BaseLoginSceneState
    {
        protected SignUpUI_Android signUpUI = null;

        public SignUpAndroidState(LoginSceneStateController controller) : base(controller)
        {
        }

        public override void Begin()
        {
            signUpUI = CreateSignUpUI();
            signUpUI.Init();

            signUpUI.OnButtonPreviousClick = PreviousPage;
            signUpUI.OnButtonCreateClick = CheckCreateAccount;
        }

        protected void CheckCreateAccount()
        {
            //獲取玩家輸入的資料
            Debug.Log(signUpUI.GetSignUpInfo().ToString());

            //檢查使用者規章及隱私條款是否同意
            if (!CheckToggleStatus())
                return;

            //TODO: 
        }

        protected void PreviousPage()
        {
            stateController.SetState(LOGIN_SCENE_STATE.IDENTIFICATION);
        }

        public override void StateUpdate()
        {

        }

        public override void End()
        {
            signUpUI.Destroy();
        }

        protected virtual SignUpUI_Android CreateSignUpUI()
        {
            return stateController.SceneManager.CreateUI<SignUpUI_Android>(AddressablesLabels.CanvasSignUpAndroid);
        }

        private bool CheckToggleStatus()
        {
            var isOn = signUpUI.CheckToggleStatus();

            if (!isOn)
                Debug.Log("You must agree to the Terms of Use.");

            return isOn;
        }
    }
}
