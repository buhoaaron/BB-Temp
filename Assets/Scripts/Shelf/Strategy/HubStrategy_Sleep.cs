

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
            hubController.ImageChar.enabled = true;
            hubController.SkeletonGraphicEgg.enabled = false;
        }

        public override void ProcessHubClick()
        {

        }
    }
}
