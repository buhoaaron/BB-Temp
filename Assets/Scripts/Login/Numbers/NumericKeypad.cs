using System.Collections.Generic;
using Barnabus.UI;
using UnityEngine.Events;

namespace Barnabus.Login
{
    public class NumericKeypad : BaseGameUI
    {
        public UnityAction<int> OnKeypadClick = null;

        private List<NumberController> numberControllers = null;

        #region BASE_API
        public override void Init()
        {
            //取得底下的數字按鍵
            numberControllers = new List<NumberController>(GetComponentsInChildren<NumberController>());

            foreach (NumberController numberController in numberControllers)
            {
                numberController.Init();
                numberController.OnClick = ProcessNumericKeypadClick;
            }
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        private void ProcessNumericKeypadClick(NumberController numberController)
        {
            OnKeypadClick?.Invoke(numberController.Number);
        }
    }
}
