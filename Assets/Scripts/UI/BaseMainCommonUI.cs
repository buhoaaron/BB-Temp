using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public abstract class BaseMainCommonUI : BaseGameUI
    {
        public int Order = 20;
        public Button ButtonReturn = null;

        protected RectTransform UIRoot = null;
        protected Vector2 originScale = Vector2.zero;
        protected Vector2 originPos = Vector2.zero;

        public override void Init()
        {
            UIRoot = transform.Find("UIRoot").GetComponent<RectTransform>();
            ButtonReturn = UIRoot.Find("Button_Return").GetComponent<Button>();

            originScale = UIRoot.localScale;
            originPos = UIRoot.localPosition;

            buttons.Add(ButtonReturn);
        }

        public void Maximize()
        {
            GetComponent<Canvas>().sortingOrder = Order;
            UIRoot.localScale = Vector2.one;
            UIRoot.localPosition = Vector2.zero;
        }

        public void Minimize()
        {
            GetComponent<Canvas>().sortingOrder = 0;
            UIRoot.localScale = originScale;
            UIRoot.localPosition = originPos;
        }
    }
}
