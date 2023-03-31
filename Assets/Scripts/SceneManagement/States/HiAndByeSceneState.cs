using UnityEngine;
using HiAndBye;
using HiAndBye.StateControl;
using Barnabus.UI;

namespace Barnabus.SceneManagement
{
    public class HiAndByeSceneState : BaseSceneState
    {
        private HiAndByeGameManager gameManager = null;
        private GameRootUI gameRootUI = null;
        private GameResultUI gameResultUI = null;
        private PotionRewardUI potionRewardUI = null;
        private SettingUI settingUI = null;

        public HiAndByeSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            gameManager = GameObject.Find("HiAndByeGameManager").GetComponent<HiAndByeGameManager>();
            gameRootUI = GameObject.Find("Canvas").GetComponent<GameRootUI>();
            gameResultUI = gameRootUI.transform.Find("GameResult").GetComponent<GameResultUI>();
            potionRewardUI = gameRootUI.transform.Find("PotionReward").GetComponent<PotionRewardUI>();
            settingUI = GameObject.Find("SettingCanvas").GetComponent<SettingUI>();

            //初始化遊戲相關系統
            gameManager.Init();
            gameRootUI.Init();
            gameResultUI.Init();
            settingUI.Init();
            potionRewardUI.Init();
            //
            gameManager.GameRootUI = gameRootUI;
            gameManager.GameResultUI = gameResultUI;
            gameManager.SettingUI = settingUI;
            gameManager.PotionRewardUI = potionRewardUI;
            //註冊事件
            AddButtonClickListener();
            //啟動遊戲狀態機
            gameManager.StateController.SetState(GAME_STATE.GAME_INIT);
            gameManager.StateController.SetDebugEnabled(false);

            gameManager.LoadRankInfo();
        }

        private void AddButtonClickListener()
        {
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameRootUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameResultUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, settingUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, potionRewardUI.Buttons);
        }

        private void RemoveButtonClickListener()
        {
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, gameRootUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, gameResultUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, settingUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, potionRewardUI.Buttons);
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
