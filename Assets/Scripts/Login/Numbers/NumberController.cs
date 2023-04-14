using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Barnabus.Login
{
    public class NumberController : MonoBehaviour, IBaseController
    {
        public int Number = 0;
        public UnityAction<NumberController> OnClick = null;
        
        private Button button = null; 

        #region BASE_API
        public void Init()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ProcessButtonClick);
        }
        public void Refresh()
        {

        }
        public void Clear()
        {

        }
        #endregion

        private void ProcessButtonClick()
        {
            OnClick?.Invoke(this);
        }
    }
}
