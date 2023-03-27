using Barnabus.UI;

namespace Barnabus.Shelf
{
    public abstract class BaseHubStrategy
    {
        public HUB_STATE State { get; protected set; }

        protected HubController hubController = null;
        protected ShelfUI shelfUI = null;

        public BaseHubStrategy(HubController hubController)
        {
            this.hubController = hubController;
            this.shelfUI = hubController.MainManager.ShelfUI;
        }

        public abstract void Refresh();

        public abstract void ProcessHubClick();
    }
}
