
namespace Barnabus.Shelf
{
    public class HubStrategy_Idle : HubStrategy_Sleep
    {
        public HubStrategy_Idle(HubController hubController) : base(hubController)
        {
            State = HUB_STATE.IDLE;
        }
        
        public override void Refresh()
        {
            base.Refresh();
        }

        protected override void ChangeBarnabusSpine(int characterID)
        {
            hubController.ChangeBarnabusSpine(characterID, "s2");
        }
    }
}
