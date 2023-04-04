using UnityEngine;

namespace Barnabus.Shelf
{
    /// <summary>
    /// 未開放
    /// </summary>
    public class HubStrategy_NotOpen : BaseHubStrategy
    {
        public HubStrategy_NotOpen(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.NOT_OPEN;
        }

        public override void Refresh()
        {
            hubController.SkeletonGraphicBarnabus.enabled = false;
            hubController.SkeletonGraphicEgg.enabled = true;

            var darkColor = new Color32(0x64, 0x64, 0x64, 0xff);
            hubController.ChangeColor(darkColor);
        }

        public override void ProcessHubClick()
        {
            var hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.MainManager.GetPotionAmount());
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);

            hubInfoUI.DoPopUp();

            InitHubLayout(hubInfoUI);
        }

        private void InitHubLayout(HubInfoUI hubInfoUI)
        {
            hubInfoUI.ButtonGameRoom.gameObject.SetActive(false);
            hubInfoUI.PotionRequire.gameObject.SetActive(false);
        }
    }
}
