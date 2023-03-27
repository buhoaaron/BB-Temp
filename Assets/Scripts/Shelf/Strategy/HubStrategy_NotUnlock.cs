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
            hubController.ImageChar.enabled = false;
            hubController.SkeletonGraphicEgg.enabled = true;
        }

        public override void ProcessHubClick()
        {
            var hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.FakePotions);
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);
            
            hubInfoUI.DoPopUp();
            hubInfoUI.OnButtonGameRoomClick = GotoGameRoom;
        }

        private void GotoGameRoom()
        {
            hubController.MainManager.MaximizeGameRoom();
            hubController.MainManager.MinimizeShelf();
        }
    }
}
