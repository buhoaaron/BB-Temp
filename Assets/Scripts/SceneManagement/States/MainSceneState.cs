﻿using UnityEngine;
using Barnabus.UI;

namespace Barnabus.SceneManagement
{
    public class MainSceneState : BaseSceneState
    {
        private MainManager mainManager = null;

        private MainUI mainUI = null;
        private GameRoomUI gameRoomUI = null;
        private ShelfUI shelfUI = null;
        private BooksUI booksUI = null;
        private LessonsUI lessonsUI = null;
        private NavigationBarUI navigationBarUI = null;

        public MainSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            FindMainManagerAndInit();

            InitUI();
            InitShelfAllHub();
            InitProfile();

            AddButtonClickListener();

            CheckMainMenuType();
        }
        private void FindMainManagerAndInit()
        {
            //取得主選單管理器
            mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
            mainManager.Init();
            //將它交給GameManager強化功能
            controller.GameManager.SetMainManager(mainManager);
        }
        private void InitUI()
        {
            mainManager.MainUI.Init();
            mainManager.ShelfUI.Init();
            mainManager.BooksUI.Init();
            mainManager.LessonsUI.Init();
            mainManager.GameRoomUI.Init();

            var isMute = controller.GameManager.AudioManager.IsMute;
            mainManager.NavigationBarUI.Init(!isMute);

            mainUI = mainManager.MainUI;
            shelfUI = mainManager.ShelfUI;
            booksUI = mainManager.BooksUI;
            lessonsUI = mainManager.LessonsUI;
            gameRoomUI = mainManager.GameRoomUI;
            navigationBarUI = mainManager.NavigationBarUI;
        }

        private void CheckMainMenuType()
        {
            var scenCache = mainManager.PlayerDataManager.GetSceneCacheData();

            switch (scenCache.MainMenuType)
            {
                case MAIN_MENU.GAME_ROOM:
                    mainManager.MaximizeGameRoom();
                    break;
                case MAIN_MENU.SHELF:
                    mainManager.MaximizeShelf();
                    break;
                case MAIN_MENU.LESSONS:
                    mainManager.MaximizeLessons();
                    break;
            }

            mainManager.PlayerDataManager.ResetSceneCacheData();
        }

        private void InitShelfAllHub()
        {
            foreach(var hubController in shelfUI.HubControllers)
            {
                //設定Barnabus資料
                var playerBarnabusData = mainManager.GetBarnabusBaseData(hubController.ID);
                hubController.Init(playerBarnabusData, mainManager);
                hubController.Refresh();
            }
        }

        private void InitProfile()
        {
            var profileInfo = mainManager.PlayerDataManager.CurrentProfile;
            var playerIcon = controller.GameManager.AddressableAssetsManager.PlayerIcons;

            var spritePlayerIcon = playerIcon.GetIcon(profileInfo.color_id, profileInfo.skin_id);

            navigationBarUI.SetProfile(profileInfo, spritePlayerIcon);
        }

        public override void StateUpdate()
        {
            mainUI.UpdateUI();

            if (mainManager.CheckUnlock())
                GotoUnlockBarnabus();

            if (mainManager.CheckWakeUp())
                GotoWakeUpBarnabus();
        }

        public override void End()
        {
            RemoveButtonClickListener();

            mainUI.Clear();
        }

        private void AddButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.AddListener(mainManager.MaximizeGameRoom);
            mainUI.ButtonLessons.onClick.AddListener(mainManager.MaximizeLessons);
            mainUI.ButtonShelf.onClick.AddListener(mainManager.MaximizeShelf);
            mainUI.ButtonBooks.onClick.AddListener(mainManager.MaximizeBooks);

            gameRoomUI.ButtonReturn.onClick.AddListener(mainManager.MinimizeGameRoom);
            lessonsUI.ButtonReturn.onClick.AddListener(mainManager.MinimizeLessons);
            shelfUI.ButtonReturn.onClick.AddListener(mainManager.MinimizeShelf);
            booksUI.ButtonReturn.onClick.AddListener(mainManager.MinimizeBooks);

            navigationBarUI.OnButtonSoundClick = ProcessSoundMute;
            navigationBarUI.OnButtonHomeClick = ProcessBackHome;

            gameRoomUI.GameButtons[0].onClick.AddListener(GotoFace);
            gameRoomUI.GameButtons[1].onClick.AddListener(GotoMusic);
            gameRoomUI.GameButtons[2].onClick.AddListener(GotoDotToDot);
            gameRoomUI.GameButtons[3].onClick.AddListener(GotoHiAndBye);
            gameRoomUI.GameButtons[4].onClick.AddListener(GotoBreathing);

            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, mainUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameRoomUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, lessonsUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, shelfUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, booksUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, navigationBarUI.Buttons);
        }
        private void RemoveButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.RemoveListener(mainManager.MaximizeGameRoom);
            mainUI.ButtonLessons.onClick.RemoveListener(mainManager.MaximizeLessons);
            mainUI.ButtonShelf.onClick.RemoveListener(mainManager.MaximizeShelf);
            mainUI.ButtonBooks.onClick.RemoveListener(mainManager.MaximizeBooks);

            gameRoomUI.ButtonReturn.onClick.RemoveListener(mainManager.MinimizeGameRoom);
            lessonsUI.ButtonReturn.onClick.RemoveListener(mainManager.MinimizeLessons);
            shelfUI.ButtonReturn.onClick.RemoveListener(mainManager.MinimizeShelf);
            booksUI.ButtonReturn.onClick.RemoveListener(mainManager.MinimizeBooks);

            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, mainUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, gameRoomUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, lessonsUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, shelfUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, booksUI.Buttons);
            controller.GameManager.AudioManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, navigationBarUI.Buttons);
        }
        
        private void GotoFace()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(SCENE_STATE.LOADING_FACE));
            controller.SetState(SCENE_STATE.GAME_PREVIEW);
        }
        private void GotoMusic()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(SCENE_STATE.LOADING_MUSIC));
            controller.SetState(SCENE_STATE.GAME_PREVIEW);
        }
        private void GotoDotToDot()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(SCENE_STATE.LOADING_DOT_TO_DOT));
            controller.SetState(SCENE_STATE.GAME_PREVIEW);
        }
        private void GotoHiAndBye()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(SCENE_STATE.LOADING_HI_AND_BYE));
            controller.SetState(SCENE_STATE.GAME_PREVIEW);
        }
        private void GotoBreathing()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(SCENE_STATE.LOADING_BREATHING));
            controller.SetState(SCENE_STATE.GAME_PREVIEW);
        }
        private void GotoWakeUpBarnabus()
        {
            controller.SetState(SCENE_STATE.WAKE_UP);
        }
        private void GotoUnlockBarnabus()
        {
            controller.SetState(SCENE_STATE.WAKE_UP_WITH_UNLOCK);
        }

        private void ProcessSoundMute(bool isOn)
        {
            var isMute = !isOn;

            controller.GameManager.AudioManager.SetMuteAll(isMute);
        }

        private void ProcessBackHome()
        {
            mainManager.BackHome();
        }
    }
}
