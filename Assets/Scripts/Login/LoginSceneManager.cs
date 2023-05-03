using UnityEngine;
using Barnabus.Login.StateControl;
using Barnabus.Network;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Barnabus.Login
{
    public class LoginSceneManager : MonoBehaviour, IBaseSystem
    {
        [Header("Set Scene Camera")]
        public Camera SceneCamera = null;
        [Header("Set IdentificationUI")]
        public IdentificationUI IdentificationUI = null;

        public NetworkManager NetworkManager { private set; get; } = null;
        public JsonManager JsonManager { private set; get; } = null;

        public ProfileManager ProfileManager => profileManager;

        public SignUpInfo CurrentSignUpInfo = null;

        public PlayerIcons PlayerIcons { private set; get; } = null;

        private PrefabPool prefabPool = null;
        private LoginSceneStateController stateController = null;
        private PageManager pageManager = null;

        private MessageManager messageManager = null;
        private VerifyAgeManager verifyAgeManager = null;
        private ProfileManager profileManager = null;

        public void Init(NetworkManager nm, JsonManager jm)
        {
            NetworkManager = nm;
            JsonManager = jm;

            Init();
        }

        #region BASE_API
        public void Init()
        {
            pageManager = new PageManager();
            messageManager = new MessageManager(this);
            verifyAgeManager = new VerifyAgeManager(this);
            profileManager = new ProfileManager(this);
            prefabPool = transform.Find("PrefabPool").GetComponent<PrefabPool>();
            
            pageManager.Init();
            IdentificationUI.Init();
            verifyAgeManager.Init();
            profileManager.Init();

            messageManager.Init(prefabPool.GetPrefab(AddressablesLabels.CanvasMessage));

            stateController = new LoginSceneStateController(this);
            stateController.SetState(LOGIN_SCENE_STATE.IDENTIFICATION);
        }
        public void SystemUpdate()
        {
            stateController.StateUpdate();
        }
        public void Clear()
        {
           
        }
        #endregion

        #region PAGE_MANAGER
        public T CreateUI<T>(string label) where T : BaseLoginCommonUI
        {
            var prefab = prefabPool.GetPrefab(label);
            var ui = GameObject.Instantiate(prefab).GetComponent<T>();
            ui.SetCanvasCamera(SceneCamera);

            pageManager.AddPage(label, ui);

            return ui;
        }
        public void ResetPages()
        {
            pageManager.ResetPages();
        }
        public T GetPage<T>(string pageKey) where T : BaseLoginCommonUI
        {
            var page = pageManager.GetPage(pageKey) as T;

            if (page == null)
            {
                page = CreateUI<T>(pageKey);
                page.Init();
            }

            return page;
        }
        #endregion

        #region MESSAGE_MANAGER

        public MessageUI DoShowErrorMessage(string title, string msg)
        {
            return messageManager.DoShowErrorMessage(title, msg);
        }

        public MessageUI DoShowErrorMessage(string msg)
        {
            return messageManager.DoShowErrorMessage(msg);
        }

        public MessageUI DoShowErrorMessage(int errorCode)
        {
           return messageManager.DoShowErrorMessage(errorCode);
        }

        public AllErrorMessageInfo LoadErrorMessageJson()
        {
            var infos = JsonManager.DeserializeObject<AllErrorMessageInfo>(JsonText.ErrorMessages);

            messageManager.SetAllErrorMessageInfo(infos);

            return infos;
        }

        #endregion

        #region NETWORK

        public void PostRequest(API_PATH path, BaseSendPacket sendPacket, NetworkCallbacks callbacks = null)
        {
            NewGameManager.Instance.NetworkManager.PostRequest(path, sendPacket, callbacks);
        }

        #endregion

        #region PROFILE
        public void LoadProfileJson()
        {
            profileManager.LoadStateCurriculum();
            profileManager.LoadGrade();
        }

        #endregion

        public bool CheckAdultAge(int birthyear)
        {
            return verifyAgeManager.CheckAdultAge(birthyear);
        }

        public void LoadPlayerIcons()
        {
            StartCoroutine(ILoadPlayerIcons());
        }
        private IEnumerator ILoadPlayerIcons()
        {
            var handle = Addressables.LoadAssetAsync<Sprite[]>(AddressablesLabels.PlayerIconSprites);

            yield return handle;

            PlayerIcons = new PlayerIcons(handle.Result);
        }
    }
}
