using UnityEngine;
using Barnabus.UI;
using System.Collections.Generic;

namespace Barnabus.SceneManagement
{
    public class MainSceneState : BaseSceneState
    {
        private MainUI mainUI = null;

        private GameRoomUI gameRoomUI = null;
        private ShelfUI shelfUI = null;
        private BooksUI booksUI = null;
        private LessonsUI lessonsUI = null;

        private List<Object> buttons = null;

        public MainSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            InitUI();

            AddButtonClickListener();
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

        private void InitUI()
        {
            mainUI = GameObject.Find("Canvas_Main").GetComponent<MainUI>();
            mainUI.Init();

            shelfUI = GameObject.Find("Canvas_Shelf").GetComponent<ShelfUI>();
            shelfUI.Init();

            booksUI = GameObject.Find("Canvas_Books").GetComponent<BooksUI>();
            booksUI.Init();

            lessonsUI = GameObject.Find("Canvas_Lessons").GetComponent<LessonsUI>();
            lessonsUI.Init();

            gameRoomUI = GameObject.Find("Canvas_GameRoom").GetComponent<GameRoomUI>();
            gameRoomUI.Init();
        }
        private void AddButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.AddListener(MaximizeGameRoom);
            mainUI.ButtonLessons.onClick.AddListener(MaximizeLessons);
            mainUI.ButtonShelf.onClick.AddListener(MaximizeShelf);
            mainUI.ButtonBooks.onClick.AddListener(MaximizeBooks);

            gameRoomUI.ButtonReturn.onClick.AddListener(MinimizeGameRoom);
            lessonsUI.ButtonReturn.onClick.AddListener(MinimizeLessons);
            shelfUI.ButtonReturn.onClick.AddListener(MinimizeShelf);
            booksUI.ButtonReturn.onClick.AddListener(MinimizeBooks);

            gameRoomUI.GameButtons[0].onClick.AddListener(GotoFace);
            gameRoomUI.GameButtons[1].onClick.AddListener(GotoMusic);
            gameRoomUI.GameButtons[2].onClick.AddListener(GotoDotToDot);
            gameRoomUI.GameButtons[3].onClick.AddListener(GotoHiAndBye);

            foreach (var button in mainUI.Buttons)
                controller.GameManager.AudioSourceManager.AddButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in gameRoomUI.Buttons)
                controller.GameManager.AudioSourceManager.AddButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in lessonsUI.Buttons)
                controller.GameManager.AudioSourceManager.AddButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in shelfUI.Buttons)
                controller.GameManager.AudioSourceManager.AddButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in booksUI.Buttons)
                controller.GameManager.AudioSourceManager.AddButton(AUDIO_NAME.BUTTON_CLICK, button);
        }
        private void RemoveButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.RemoveListener(MaximizeGameRoom);
            mainUI.ButtonLessons.onClick.RemoveListener(MaximizeLessons);
            mainUI.ButtonShelf.onClick.RemoveListener(MaximizeShelf);
            mainUI.ButtonBooks.onClick.RemoveListener(MaximizeBooks);

            gameRoomUI.ButtonReturn.onClick.RemoveListener(MinimizeGameRoom);
            lessonsUI.ButtonReturn.onClick.RemoveListener(MinimizeLessons);
            shelfUI.ButtonReturn.onClick.RemoveListener(MinimizeShelf);
            booksUI.ButtonReturn.onClick.RemoveListener(MinimizeBooks);

            foreach (var button in mainUI.Buttons)
                controller.GameManager.AudioSourceManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in gameRoomUI.Buttons)
                controller.GameManager.AudioSourceManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in lessonsUI.Buttons)
                controller.GameManager.AudioSourceManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in shelfUI.Buttons)
                controller.GameManager.AudioSourceManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, button);
            foreach (var button in booksUI.Buttons)
                controller.GameManager.AudioSourceManager.RemoveButton(AUDIO_NAME.BUTTON_CLICK, button);
        }
        private void MaximizeShelf()
        {
            shelfUI.Maximize();
        }
        private void MinimizeShelf()
        {
            shelfUI.Minimize();
        }
        private void MaximizeLessons()
        {
            lessonsUI.Maximize();
        }
        private void MinimizeLessons()
        {
            lessonsUI.Minimize();
        }
        private void MaximizeGameRoom()
        {
            gameRoomUI.Maximize();
        }
        private void MinimizeGameRoom()
        {
            gameRoomUI.Minimize();
        }
        private void MaximizeBooks()
        {
            booksUI.Maximize();
        }
        private void MinimizeBooks()
        {
            booksUI.Minimize();
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
