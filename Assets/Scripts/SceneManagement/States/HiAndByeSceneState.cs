using UnityEngine;
using HiAndBye;
using HiAndBye.StateControl;

namespace Barnabus.SceneManagement
{
    public class HiAndByeSceneState : BaseSceneState
    {
        private HiAndByeGameManager gameManager = null;
        private GameRootUI gameRootUI = null;
        private GameResultUI gameResultUI = null;

        public HiAndByeSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            gameManager = GameObject.Find("HiAndByeGameManager").GetComponent<HiAndByeGameManager>();
            gameRootUI = GameObject.Find("Canvas").GetComponent<GameRootUI>();
            gameResultUI = gameRootUI.transform.Find("Result").GetComponent<GameResultUI>();
            //初始化遊戲相關系統
            gameManager.Init();
            gameRootUI.Init();
            gameResultUI.Init();
            //
            gameManager.GameRootUI = gameRootUI;
            gameManager.GameResultUI = gameResultUI;
            //註冊事件
            AddButtonClickListener();
            //啟動遊戲狀態機
            gameManager.StateController.SetState(GAME_STATE.GAME_INIT);
            gameManager.StateController.SetDebugEnabled(false);
        }

        private void AddButtonClickListener()
        {
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameRootUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameResultUI.Buttons);

            gameRootUI.buttonBackMain.onClick.AddListener(BackMainScene);
        }

        private void RemoveButtonClickListener()
        {
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, gameRootUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, gameResultUI.Buttons);

            gameRootUI.buttonBackMain.onClick.RemoveListener(BackMainScene);
        }

        private void BackMainScene()
        {
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }

        public override void StateUpdate()
        {
            gameManager.SystemUpdate();
        }

        public override void End()
        {
            RemoveButtonClickListener();
            gameManager.GetSpineAssets().Clear();
            gameManager.Clear();
        }
    }
}
