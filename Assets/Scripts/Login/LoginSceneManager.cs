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
        public bool IsLoginFlowComplete { private set; get; } = false;

        public NewGameManager GameManager = null;
        public NetworkManager NetworkManager { private set; get; } = null;
        public JsonManager JsonManager { private set; get; } = null;

        public ProfileManager ProfileManager => profileManager;

        public SignUpInfo CurrentSignUpInfo = null;

        public PlayerIcons PlayerIcons { private set; get; } = null;

        private PagePrefabPool pagePrefabPool = null;
        private LoginSceneStateController stateController = null;
        private PageManager pageManager = null;

        private MessageManager messageManager = null;
        private VerifyAgeManager verifyAgeManager = null;
        private ProfileManager profileManager = null;

        public void Init(NewGameManager gm)
        {
            GameManager = gm;
            NetworkManager = GameManager.NetworkManager;
            JsonManager = GameManager.JsonManager;

            Init();
        }

        #region BASE_API
        public void Init()
        {
            pageManager = new PageManager();
            messageManager = new MessageManager(this);
            verifyAgeManager = new VerifyAgeManager(this);
            profileManager = new ProfileManager(this);
            pagePrefabPool = transform.Find("PrefabPool").GetComponent<PagePrefabPool>();
            
            pageManager.Init();
            IdentificationUI.Init();
            verifyAgeManager.Init();
            profileManager.Init();

            messageManager.Init(pagePrefabPool.GetPrefab(PAGE.MESSAGE));

            stateController = new LoginSceneStateController(this);
            stateController.SetState(LOGIN_SCENE_STATE.IDENTIFICATION);
        }
        public void SystemUpdate()
        {
            stateController.StateUpdate();
        }
        public void Clear()
        {
            IsLoginFlowComplete = false;
        }
        #endregion

        #region PAGE_MANAGER
        private T CreateUI<T>(PAGE name) where T : BaseLoginCommonUI
        {
            var prefab = pagePrefabPool.GetPrefab(name);
            var ui = GameObject.Instantiate(prefab).GetComponent<T>();
            ui.SetCanvasCamera(SceneCamera);

            pageManager.AddPage(name, ui);

            return ui;
        }
        public void ResetPages()
        {
            pageManager.ResetPages();
        }
        public T GetPage<T>(PAGE name) where T : BaseLoginCommonUI
        {
            var page = pageManager.GetPage(name) as T;

            if (page == null)
            {
                page = CreateUI<T>(name);
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

        public void PostRequest(API_PATH path, BaseSendPacket sendPacket)
        {
            NewGameManager.Instance.NetworkManager.PostRequest(path, sendPacket);
        }

        #endregion

        #region PROFILE
        public void LoadProfileJson()
        {
            profileManager.LoadStateCurriculum();
            profileManager.LoadGrade();
        }

        #endregion

        /// <summary>
        /// 完成所有Login流程需要的行為
        /// </summary>
        public void CompleteLogin()
        {
            IsLoginFlowComplete = true;
        }

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
