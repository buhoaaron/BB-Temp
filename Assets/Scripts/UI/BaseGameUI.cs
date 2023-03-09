using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Barnabus.UI
{
    public abstract class BaseGameUI : MonoBehaviour, IBaseUI
    {
        public List<Button> Buttons => buttons;
        protected List<Button> buttons = new List<Button>();

        public abstract void Init();
        public abstract void UpdateUI();
        public abstract void Clear();

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
