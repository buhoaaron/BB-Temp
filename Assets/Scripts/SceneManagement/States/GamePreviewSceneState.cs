using UnityEngine;
using Barnabus.UI;

namespace Barnabus.SceneManagement
{
    public class GamePreviewSceneState : BaseSceneState
    {
        private GamePreviewUI gamePreviewUI = null;

        public GamePreviewSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            gamePreviewUI = GameObject.FindObjectOfType<GamePreviewUI>();
            gamePreviewUI.Init();

            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gamePreviewUI.Buttons);

            gamePreviewUI.OnButtonStartClick = Jump;
        }

        public override void StateUpdate()
        {
            gamePreviewUI.UpdateUI();
        }

        public override void End()
        {
            gamePreviewUI.Clear();
        }

        private void Jump()
        {
            var sceneCache = controller.GameManager.PlayerDataManager.GetSceneCacheData();
            //跳轉玩家選擇的遊戲
            controller.SetState(sceneCache.GamePreviewJumpState); 
        }
    }
}
