using UnityEngine.UI;
using UnityEngine;

namespace Barnabus.UI
{
    public class GameRoomUI : BaseMainCommonUI
    {
        public Button[] GameButtons = null;
        public override void Init()
        {
            base.Init();

            buttons.AddRange(GameButtons);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
