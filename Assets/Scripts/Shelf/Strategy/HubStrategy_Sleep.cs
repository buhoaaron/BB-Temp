
namespace Barnabus.Shelf
{
    public class HubStrategy_Sleep : BaseHubStrategy
    {
        public HubStrategy_Sleep(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.SLEEP;
        }
        
        public override void Refresh()
        {
            hubController.SkeletonGraphicBarnabus.enabled = true;
            hubController.SkeletonGraphicEgg.enabled = false;

            var characterID = hubController.BarnabusData.CharacterID;
            ChangeBarnabusSpine(characterID);
        }

        protected virtual void ChangeBarnabusSpine(int characterID)
        {
            hubController.ChangeBarnabusSpine(characterID);
        }

        public override void ProcessHubClick()
        {
            hubController.MainManager.PlayerDataManager.SetUnlockInfo(hubController.BarnabusData);

            hubController.MainManager.GotoWakeUpState();
        }
    }
}
