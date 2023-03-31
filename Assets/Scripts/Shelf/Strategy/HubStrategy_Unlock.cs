using UnityEngine;
using RenderHeads.Media.AVProVideo;

namespace Barnabus.Shelf
{
    public class HubStrategy_Unlock : BaseHubStrategy
    {
        private HubInfoUI hubInfoUI = null;

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
            hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.MainManager.GetPotionAmount());
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);

            hubInfoUI.DoPopUp();

            hubInfoUI.SkeletonGraphicEgg.AnimationState.SetAnimation(0, "idle_Green", true);

            hubInfoUI.OnButtonUnlockClick = ProcessUnlock;
        }

        private void ProcessUnlock()
        {
            hubController.MainManager.GotoUnlockState();
        }
    }
}
