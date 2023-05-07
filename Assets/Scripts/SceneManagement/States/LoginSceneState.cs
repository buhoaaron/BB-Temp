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
            manager.Init(controller.GameManager);
            
            //TOFIX: 測試用SKip鍵，後續可拔
            manager.IdentificationUI.ButtonSkip.onClick.AddListener(SkipLogin);

            manager.LoadErrorMessageJson();
            manager.LoadProfileJson();
        }

        public override void StateUpdate()
        {
            manager.SystemUpdate();

            if (manager.IsLoginFlowComplete)
                GoMain();
        }

        public override void End()
        {
            manager.IdentificationUI.ButtonSkip.onClick.RemoveListener(SkipLogin);
            manager.Clear();
        }

        private void GoMain()
        {
            controller.SetState(SCENE_STATE.LOADING_MAIN);
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
