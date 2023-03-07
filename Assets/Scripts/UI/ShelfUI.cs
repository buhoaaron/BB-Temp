using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public class ShelfUI : BaseGameUI
    {
        public Button ButtonReturn = null;
        public override void Init()
        {
            ButtonReturn = transform.Find("Button_Return").GetComponent<Button>();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
