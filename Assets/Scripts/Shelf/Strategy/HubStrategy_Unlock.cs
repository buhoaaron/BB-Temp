
namespace Barnabus.Shelf
{
    public class HubStrategy_Unlock : BaseHubStrategy
    {
        private string eggUnlockAnimation = SpineLabels.EggUnlockAnimation;

        public HubStrategy_Unlock(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.UNLOCK;
        }

        public override void Refresh()
        {
            hubController.SkeletonGraphicBarnabus.enabled = false;
            hubController.SkeletonGraphicEgg.enabled = true;

            SetEggIdleAnimation();
        }

        private void SetEggIdleAnimation()
        {
            var spineEgg = hubController.ChangeEggSkin(hubController.BarnabusData.ColorName);
            spineEgg.AnimationState.SetAnimation(0, eggUnlockAnimation, true);
        }

        public override void ProcessHubClick()
        {
            var hubInfoUI = hubController.MainManager.CreateHubInfoUIAndInit(hubController.State);

            hubInfoUI.SetPlayerPotions(hubController.MainManager.GetPotionAmount());
            hubInfoUI.SetPotionRequire(hubController.BarnabusData.PotionExchange);
            hubInfoUI.SetElement(hubController.BarnabusData.Element);

            hubInfoUI.DoPopUp();

            var spineEgg = hubInfoUI.ChangeEggSkin(hubController.BarnabusData.ColorName);
            spineEgg.AnimationState.SetAnimation(0, eggUnlockAnimation, true);

            hubInfoUI.OnButtonUnlockClick = ProcessUnlock;

            InitHubLayout(hubInfoUI);
        }

        private void ProcessUnlock()
        {
            hubController.MainManager.PlayerDataManager.SetUnlockInfo(hubController.BarnabusData);

            hubController.MainManager.GotoUnlockState();
        }

        private void InitHubLayout(HubInfoUI hubInfoUI)
        {
            hubInfoUI.NotOpen.gameObject.SetActive(false);
        }
    }
}
