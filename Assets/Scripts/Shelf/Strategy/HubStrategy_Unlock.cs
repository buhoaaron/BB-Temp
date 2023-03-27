

namespace Barnabus.Shelf
{
    public class HubStrategy_Unlock : BaseHubStrategy
    {
        public HubStrategy_Unlock(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.UNLOCK;
        }

        public override void Refresh()
        {
            hubController.ImageChar.enabled = false;
            hubController.SkeletonGraphicEgg.enabled = true;

            SetEggIdleAnimation();
        }

        private void SetEggIdleAnimation()
        {
            hubController.SkeletonGraphicEgg.AnimationState.SetAnimation(0, "idle_Green", true);
        }

        public override void ProcessHubClick()
        {
            var hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.FakePotions);
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);

            hubInfoUI.DoPopUp();

            hubInfoUI.SkeletonGraphicEgg.AnimationState.SetAnimation(0, "idle_Green", true);

            //hubInfoUI.OnButtonGameRoomClick = GotoGameRoom;
        }

    }
}
