using UnityEngine;

namespace Barnabus.Network
{
    public class ConnectingManager : BaseNetworkManager
    {
        private ConnectingUI connectingUI = null;
        public ConnectingManager(NetworkManager networkManager) :base(networkManager)
        {

        }

        #region BASE_API
        public override void Init()
        {
            Debug.Assert(networkManager.ConnectingUIPrefab != null, "you must set ConnectingUIPrefab.");
        }
        public override void SystemUpdate()
        {

        }
        public override void Clear()
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
