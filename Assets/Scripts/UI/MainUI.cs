using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public class MainUI : BaseGameUI
    {
        public Button ButtonGameRoom = null;
        public Button ButtonShelf = null;
        public Button ButtonLessons = null;
        public Button ButtonBooks = null;
        public override void Init()
        {
            ButtonGameRoom = transform.Find("SceneChangeButtons/GameRoomButton").GetComponent<Button>();
            ButtonShelf = transform.Find("SceneChangeButtons/ShelfButton").GetComponent<Button>();
            ButtonLessons = transform.Find("SceneChangeButtons/ClassRoomButton").GetComponent<Button>();
            ButtonBooks = transform.Find("SceneChangeButtons/LibraryButton").GetComponent<Button>();

            buttons.Add(ButtonGameRoom);
            buttons.Add(ButtonShelf);
            buttons.Add(ButtonLessons);
            buttons.Add(ButtonBooks);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
