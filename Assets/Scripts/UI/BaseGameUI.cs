using UnityEngine;

namespace Barnabus.UI
{
    public abstract class BaseGameUI : MonoBehaviour, IBaseUI
    {
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
