using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public class MainUI : BaseGameUI
    {
        public Button ButtonGameRoom = null;
        public Button ButtonShelf = null;
        public Button ButtonLab = null;
        public override void Init()
        {
            ButtonGameRoom = transform.Find("SceneChangeButtons/GameRoomButton").GetComponent<Button>();
            ButtonShelf = transform.Find("SceneChangeButtons/ShelfButton").GetComponent<Button>();
            ButtonLab = transform.Find("SceneChangeButtons/LabButton").GetComponent<Button>();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
