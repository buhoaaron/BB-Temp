using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

namespace Barnabus.Network
{
    public class NetworkManager : MonoBehaviour, IBaseSystem
    {
        public bool IsNetworkReady
        {
            get
            {
                return NetworkConfig != null && NetworkPaths != null;
            }
        }
        public GameObject ConnectingUIPrefab = null;

        public NetworkInfo NetworkInfo { get; private set; } = null;
        public NetworkConfig NetworkConfig { get; private set; } = null;
        public NetworkPaths NetworkPaths { get; private set; } = null;
        public NetworkDispatcher Dispatcher { get; private set; } = null;

        private ConnectingManager connectingManager = null;
        private PathParser pathParser = null;
        
        private string postRequestTag = "POST";

        public void LoadNetworkConfig()
        {
            var path = PathHelper.GetStreamingAssetsPath(AppFiles.NetworkConfig);

            GetRequest(path, (text) => 
            {
                NetworkConfig = JsonConvert.DeserializeObject<NetworkConfig>(text);
            });
        }

        public void LoadNetworkPaths()
        {
            var path = PathHelper.GetStreamingAssetsPath(AppFiles.NetworkPaths);

            GetRequest(path, (text) =>
            {
                NetworkPaths = JsonConvert.DeserializeObject<NetworkPaths>(text);
            });
        }

        #region BASE_API
        public void Init()
        {
            connectingManager = new ConnectingManager(this);
            pathParser = new PathParser(this);
            Dispatcher = new NetworkDispatcher(this);

            connectingManager.Init();
            pathParser.Init();
            Dispatcher.Init();
        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        {

        }
        #endregion

        #region POST
        public void PostRequest(API_PATH path, BaseSendPacket sendPacket, NetworkCallbacks callbacks = null)
        {
            string url = pathParser.CaseUrl(path);
            string jsonStr = JsonConvert.SerializeObject(sendPacket);

            Debug.unityLogger.Log(postRequestTag, string.Format("PostRequest {0}, json: {1}", url, jsonStr));
            //開啟連線提示
            connectingManager.CreateConnectingTip();
            //送出請求
            StartCoroutine(IPostRequest(path, url, jsonStr, callbacks));
        }

        private IEnumerator IPostRequest(API_PATH path, string url, string jsonStr, NetworkCallbacks callbacks)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(jsonStr);
            UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(bytes),
                downloadHandler = new DownloadHandlerBuffer()
            };
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            //關閉連線提示
            connectingManager.CloseConnectingTip();

            //顯示訊息
            string result = request.downloadHandler.text;
            var output = string.Format("PostRequest callback: statusCode {0}, Value: {1}", request.responseCode, result);
            Debug.unityLogger.Log(postRequestTag, output);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                var receiveErrorMessage = JsonConvert.DeserializeObject<ReceiveErrorMessage>(result);
                receiveErrorMessage.StatusCode = request.responseCode;

                callbacks?.OnFail?.Invoke(receiveErrorMessage);
            }
            else
            {
                //callbacks?.OnSuccess?.Invoke(result);

                Dispatcher.Dispatch(path, result);
            }
        }
        #endregion

        #region GET
        public Coroutine GetRequest(string url, Action<string> onSuccess)
        {
            Debug.LogFormat("GetRequest url: {0}", url);

            return StartCoroutine(IGetRequest(url, onSuccess));
        }

        private IEnumerator IGetRequest(string url, Action<string> onSuccess)
        {
            var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError(request.error);
            }
        }
        #endregion

        public void UpdatePlayerNetworkInfo(NetworkInfo info)
        {
            NetworkInfo = info;
        }

        public string CaseUrl(API_PATH path)
        {
            return pathParser.CaseUrl(path);
        }
    }
}
