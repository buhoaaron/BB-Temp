using UnityEngine;
using Barnabus.Shelf;
using System.Collections.Generic;

namespace Barnabus.UI
{
    public class ShelfUI : BaseMainCommonUI
    {
        public List<HubController> HubControllers => listHubController;

        private Transform allHub = null;
        private List<HubController> listHubController = null;

        public override void Init()
        {
            base.Init();

            allHub = transform.Find("UIRoot/AllHub");
            //取得掛在Hub上的所有控制器
            var arrayHubController = allHub.GetComponentsInChildren<HubController>();
            listHubController = new List<HubController>(arrayHubController);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }
    }
}
