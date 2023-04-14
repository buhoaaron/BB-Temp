using Barnabus.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.Login
{
    public class BirthYearUI : BaseGameUI
    {
        [Header("Set all BirthdayNumberController")]
        public List<BirthYearNumberController> Controllers = null;

        #region BASE_API
        public override void Init()
        {
            foreach (var controller in Controllers)
                controller.Init();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        /// <summary>
        /// 查看目前待輸入的欄位控制器(null即代表都有輸入了)
        /// </summary>
        public BirthYearNumberController CheckControllerEmpty()
        {
            foreach (var controller in Controllers)
            {
                if (controller.CheckEmpty())
                    return controller;
            }

            return null;
        }

        /// <summary>
        /// 清空號碼
        /// </summary>
        public void ResetNumbers()
        {
            foreach (var controller in Controllers)
            {
                controller.Clear();
            }
        }

        /// <summary>
        /// 設定號碼
        /// </summary>
        public void SetNumber(int number)
        {
            foreach (var controller in Controllers)
            {
                var isEmpty = controller.CheckEmpty();
                //有空欄位才填入號碼
                if (isEmpty)
                {
                    controller.SetNumber(number);
                    break;
                }
            }
        }
    }
}
