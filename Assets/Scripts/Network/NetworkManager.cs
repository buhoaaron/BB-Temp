using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;

namespace Barnabus.Network
{
    public class NetworkManager : BaseBarnabusManager
    {
        public bool IsNetworkReady
        {
            get
            {
                return NetworkConfig != null && NetworkPaths != null;
            }
        }

        public NetworkConfig NetworkConfig { get; private set; } = null;
        public NetworkPaths NetworkPaths { get; private set; } = null;

        private PathParser pathParser = null;
        private string postRequestTag = "POST";

        public NetworkManager(NewGameManager gm) : base(gm)
        {
            
        }

        public void LoadNetworkConfig()
        {
            var path = Path.Combine(Application.streamingAssetsPath, AppFiles.NetworkConfig);

            GameManager.NetworkManager.GetRequest(path, (text) => 
            {
                NetworkConfig = JsonConvert.DeserializeObject<NetworkConfig>(text);
            });
        }

        public void LoadNetworkPaths()
        {
            var path = Path.Combine(Application.streamingAssetsPath, AppFiles.NetworkPaths);

            GameManager.NetworkManager.GetRequest(path, (text) =>
            {
                NetworkPaths = JsonConvert.DeserializeObject<NetworkPaths>(text);
            });
        }

        #region BASE_API
        public override void Init()
        {
            pathParser = new PathParser(this);
            pathParser.Init();
        }
        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        #region POST
        public void PostRequest(API_PATH path, BaseSendPacket sendPacket, NetworkCallbacks callbacks = null)
        {
            string url = pathParser.CaseUrl(path);
            string jsonStr = JsonConvert.SerializeObject(sendPacket);

            Debug.unityLogger.Log(postRequestTag, string.Format("PostRequest {0}, json: {1}", url, jsonStr));

            GameManager.CustomStartCoroutine(IPostRequest(url, jsonStr, callbacks));
        }

        private IEnumerator IPostRequest(string url, string jsonStr, NetworkCallbacks callbacks)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(jsonStr);
            UnityWebRequest request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
            {
                uploadHandler = new UploadHandlerRaw(bytes),
                downloadHandler = new DownloadHandlerBuffer()
            };
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            //顯示訊息
            string result = request.downloadHandler.text;
            var output = string.Format("PostRequest callback: statusCode {0}, Value: {1}", request.responseCode, result);
            Debug.unityLogger.LogError(postRequestTag, output);

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                var receiveErrorMessage = JsonConvert.DeserializeObject<ReceiveErrorMessage>(result);
                receiveErrorMessage.StatusCode = request.responseCode;

                callbacks?.OnFail?.Invoke(receiveErrorMessage);
            }
            else
            {
                callbacks?.OnSuccess?.Invoke(result);
            }
        }
        #endregion

        #region GET
        public Coroutine GetRequest(string url, Action<string> onSuccess)
        {
            Debug.LogFormat("GetRequest url: {0}", url);

            return GameManager.CustomStartCoroutine(IGetRequest(url, onSuccess));
        }

        private IEnumerator IGetRequest(string url, Action<string> onSuccess)
        {
            var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
        }
        #endregion

        public string CaseUrl(API_PATH path)
        {
            return pathParser.CaseUrl(path);
        }
    }
}
