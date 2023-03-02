using UnityEngine.UI;

namespace Barnabus.UI
{
    public class GameRoomUI : BaseGameUI
    {
        public Button ButtonReturn = null;
        public override void Init()
        {
            ButtonReturn = transform.Find("Return").GetComponent<Button>();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
