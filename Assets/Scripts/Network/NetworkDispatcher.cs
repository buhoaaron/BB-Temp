using System;
using Newtonsoft.Json; 

namespace Barnabus.Network
{
    /// <summary>
    /// 封包分配器
    /// </summary>
    public class NetworkDispatcher : BaseNetworkManager
    {
        public Action<ReceiveLogin> OnReceiveLogin = null;
        public Action<ReceiveSignUp> OnReceiveSignUp = null;
        public Action<ReceiveCreatePlayer> OnReceiveCreatePlayer = null;

        public Action<ReceiveErrorMessage> OnReceiveErrorMessage = null;
        public NetworkDispatcher(NetworkManager networkManager) : base(networkManager)
        {
            
        }

        #region BASE_API
        public override void Init()
        {

        }
        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        public void Dispatch(API_PATH path, string result)
        {
            switch (path)
            {
                case API_PATH.SignUp:
                    var receiveSignUp = DeserializeObject<ReceiveSignUp>(path, result);
                    OnReceiveSignUp?.Invoke(receiveSignUp);
                    break;
                case API_PATH.Login:
                    var receiveLogin = DeserializeObject<ReceiveLogin>(path, result);
                    OnReceiveLogin?.Invoke(receiveLogin);
                    break;
                case API_PATH.CreatePlayer:
                    var receiveCreatePlayer = DeserializeObject<ReceiveCreatePlayer>(path, result);
                    OnReceiveCreatePlayer?.Invoke(receiveCreatePlayer);
                    break;
            }
        }

        private T DeserializeObject<T>(API_PATH path, string result) where T: BaseReceivePacket
        {
            try
            {
                if (string.IsNullOrEmpty(result))
                    throw new JsonException("The json data is an empty string or null");

                T packet = JsonConvert.DeserializeObject<T>(result);

                return packet;
            }
            catch(JsonException ex)
            {
                UnityEngine.Debug.LogError(string.Format("{0} DeserializeObject Fail: {1}", path, ex));
                return null;
            }
        }

        public void NotifyError(long stateCode, string result)
        {
            var receiveErrorMessage = JsonConvert.DeserializeObject<ReceiveErrorMessage>(result);
            receiveErrorMessage.StatusCode = stateCode;

            OnReceiveErrorMessage?.Invoke(receiveErrorMessage);
        }
    }
}
