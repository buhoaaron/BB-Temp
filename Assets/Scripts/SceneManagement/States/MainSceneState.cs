using UnityEngine;
using Barnabus.UI;

namespace Barnabus.SceneManagement
{
    public class MainSceneState : BaseSceneState
    {
        private MainUI mainUI = null;

        private GameRoomUI gameRoomUI = null;
        private ShelfUI shelfUI = null;
        private LabUI labUI = null;
        public MainSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            InitUI();

            shelfUI.Hide();
            labUI.Hide();
            gameRoomUI.Hide();

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

            labUI = GameObject.Find("Canvas_Lab").GetComponent<LabUI>();
            labUI.Init();

            gameRoomUI = GameObject.Find("Canvas_GameRoom").GetComponent<GameRoomUI>();
            gameRoomUI.Init();
        }
        private void AddButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.AddListener(ShowGameRoom);
            mainUI.ButtonLab.onClick.AddListener(ShowLab);
            mainUI.ButtonShelf.onClick.AddListener(ShowShelf);

            gameRoomUI.ButtonReturn.onClick.AddListener(HideGameRoom);
            labUI.ButtonReturn.onClick.AddListener(HideLab);
            shelfUI.ButtonReturn.onClick.AddListener(HideShelf);
        }
        private void RemoveButtonClickListener()
        {
            mainUI.ButtonGameRoom.onClick.RemoveListener(ShowGameRoom);
            mainUI.ButtonLab.onClick.RemoveListener(ShowLab);
            mainUI.ButtonShelf.onClick.RemoveListener(ShowShelf);

            gameRoomUI.ButtonReturn.onClick.RemoveListener(HideGameRoom);
            labUI.ButtonReturn.onClick.RemoveListener(HideLab);
            shelfUI.ButtonReturn.onClick.RemoveListener(HideShelf);
        }
        private void ShowShelf()
        {
            shelfUI.Show();
        }
        private void HideShelf()
        {
            shelfUI.Hide();
        }
        private void ShowLab()
        {
            labUI.Show();
        }
        private void HideLab()
        {
            labUI.Hide();
        }
        private void ShowGameRoom()
        {
            gameRoomUI.Show();
        }
        private void HideGameRoom()
        {
            gameRoomUI.Hide();
        }
    }
}
