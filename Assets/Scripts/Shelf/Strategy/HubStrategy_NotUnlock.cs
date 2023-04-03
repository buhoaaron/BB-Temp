using UnityEngine;

namespace Barnabus.Shelf
{
    public class HubStrategy_NotUnlock : BaseHubStrategy
    {
        public HubStrategy_NotUnlock(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.NOT_UNLOCK;
        }

        public override void Refresh()
        {
            hubController.SkeletonGraphicBarnabus.enabled = false;
            hubController.SkeletonGraphicEgg.enabled = true;
        }

        public override void ProcessHubClick()
        {
            var hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.MainManager.GetPotionAmount());
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);
            
            hubInfoUI.DoPopUp();
            hubInfoUI.OnButtonGameRoomClick = GotoGameRoom;

            InitHubLayout(hubInfoUI);
        }

        private void GotoGameRoom()
        {
            hubController.MainManager.MaximizeGameRoom();
            hubController.MainManager.MinimizeShelf();
        }

        private void InitHubLayout(HubInfoUI hubInfoUI)
        {
            hubInfoUI.NotOpen.gameObject.SetActive(false);
        }
    }
}
