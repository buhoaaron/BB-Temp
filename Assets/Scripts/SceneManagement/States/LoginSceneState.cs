using UnityEngine;
using Barnabus.Login;

namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 登入場景狀態
    /// </summary>
    public class LoginSceneState : BaseSceneState
    {
        protected LoginSceneManager manager;

        public LoginSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        { }

        public override void Begin()
        {
            manager = GameObject.FindObjectOfType<LoginSceneManager>();
            manager.Init();

            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, manager.IdentificationUI.Buttons);

            manager.IdentificationUI.ButtonSkip.onClick.AddListener(SkipLogin);
        }

        public override void StateUpdate()
        {
            manager.SystemUpdate();
        }

        public override void End()
        {
            manager.IdentificationUI.ButtonSkip.onClick.RemoveListener(SkipLogin);
            manager.Clear();
        }

        /// <summary>
        /// (Debug)跳過登入流程
        /// </summary>
        private void SkipLogin()
        {
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
    }
}
