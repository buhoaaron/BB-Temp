using UnityEngine;
using Barnabus.UI;
using System.Collections.Generic;
using Barnabus.Shelf;

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

        public MainSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            FindMainManagerAndInit();

            InitUI();
            AddButtonClickListener();
            mainManager.LoadAsset(InitShelfAllHub);

            //Debug.Log(string.Format("你現在有{0}藥水", DataManager.Potions[PotionType.Red]));
            //DataManager.Potions.AddPotion(PotionType.Red, 10);
            //Debug.Log(string.Format("你現在有{0}藥水", DataManager.Potions[PotionType.Red]));
        }

        private void FindMainManagerAndInit()
        {
            mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
            mainManager.Init();
            //將它交給GameManager強化功能
            NewGameManager.Instance.SetMainManager(mainManager);
        }

        private void InitUI()
        {
            mainManager.MainUI.Init();
            mainManager.ShelfUI.Init();
            mainManager.BooksUI.Init();
            mainManager.LessonsUI.Init();
            mainManager.GameRoomUI.Init();

            mainUI = mainManager.MainUI;
            shelfUI = mainManager.ShelfUI;
            booksUI = mainManager.BooksUI;
            lessonsUI = mainManager.LessonsUI;
            gameRoomUI = mainManager.GameRoomUI;
        }

        private void InitShelfAllHub()
        {
            foreach(var hubController in shelfUI.HubControllers)
            {
                //設定Barnabus資料
                var barnabusBaseData = mainManager.GetBarnabusBaseData(hubController.ID);
                hubController.Init(barnabusBaseData, mainManager);
                hubController.Refresh();
            }
        }

        public override void StateUpdate()
        {
            mainUI.UpdateUI();
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

            gameRoomUI.GameButtons[0].onClick.AddListener(GotoFace);
            gameRoomUI.GameButtons[1].onClick.AddListener(GotoMusic);
            gameRoomUI.GameButtons[2].onClick.AddListener(GotoDotToDot);
            gameRoomUI.GameButtons[3].onClick.AddListener(GotoHiAndBye);

            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, mainUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, gameRoomUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, lessonsUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, shelfUI.Buttons);
            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, booksUI.Buttons);
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
        }
        
        private void GotoFace()
        {
            controller.SetState(SCENE_STATE.LOADING_FACE);
        }
        private void GotoMusic()
        {
            controller.SetState(SCENE_STATE.LOADING_MUSIC);
        }
        private void GotoDotToDot()
        {
            controller.SetState(SCENE_STATE.LOADING_DOT_TO_DOT);
        }
        private void GotoHiAndBye()
        {
            controller.SetState(SCENE_STATE.LOADING_HI_AND_BYE);
        }
    }
}
