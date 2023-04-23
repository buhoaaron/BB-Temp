namespace Barnabus.Network
{
    public abstract class BaseNetworkManager : IBaseSystem
    {
        protected NetworkManager networkManager = null;

        public BaseNetworkManager(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        #region BASE_API
        public abstract void Init();
        public abstract void SystemUpdate();
        public abstract void Clear();
        #endregion
    }
}
