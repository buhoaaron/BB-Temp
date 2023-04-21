using UnityEngine;

namespace Barnabus.Network
{
    public class ConnectingManager : IBaseSystem
    {
        private NetworkManager networkManager = null;
        private ConnectingUI connectingUI = null;
        public ConnectingManager(NetworkManager networkManager) 
        {
            this.networkManager = networkManager;
        }

        #region BASE_API
        public void Init()
        {
            Debug.Assert(networkManager.ConnectingUIPrefab != null, "you must set ConnectingUIPrefab.");
        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        { 

        }
        #endregion

        public ConnectingUI CreateConnectingTip()
        {
            if (connectingUI != null)
                return connectingUI;

            connectingUI = GameObject.Instantiate(networkManager.ConnectingUIPrefab).GetComponent<ConnectingUI>();

            connectingUI.Init();
            connectingUI.DoPopUp(delay: 0.5f);

            return connectingUI;
        }

        public void CloseConnectingTip()
        {
            if (connectingUI != null)
            {
                connectingUI.Destroy();
                connectingUI = null;
            }
        }
    }
}
