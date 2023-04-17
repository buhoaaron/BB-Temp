using UnityEngine;
using Barnabus.Login.StateControl;

namespace Barnabus.Login
{
    public class LoginSceneManager : MonoBehaviour, IBaseSystem
    {
        [Header("Set Scene Camera")]
        public Camera SceneCamera = null;
        [Header("Set IdentificationUI")]
        public IdentificationUI IdentificationUI = null;

        private PrefabPool prefabPool = null;
        private LoginSceneStateController stateController = null;
        private PageManager pageManager = null;

        private MessageManager messageManager = null;

        #region BASE_API
        public void Init()
        {
            pageManager = new PageManager();
            messageManager = new MessageManager(this);
            prefabPool = transform.Find("PrefabPool").GetComponent<PrefabPool>();
            
            pageManager.Init();
            IdentificationUI.Init();
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

        public T CreateUI<T>(string label) where T : BaseLoginCommonUI
        {
            var prefab = prefabPool.GetPrefab(label);
            var ui = GameObject.Instantiate(prefab).GetComponent<T>();
            ui.SetCanvasCamera(SceneCamera);

            pageManager.AddPage(label, ui);

            return ui;
        }

        public BaseLoginCommonUI GetPage(string pageKey)
        {
            var page = pageManager.GetPage(pageKey);

            if (page == null)
                page = CreateUI<BaseLoginCommonUI>(pageKey);

            return page;
        }

        public MessageUI DoShowErrorMessage(int errorCode)
        {
           return messageManager.DoShowErrorMessage(errorCode);
        }

        public AllErrorMessageInfo LoadErrorMessageJson()
        {
            var infos = NewGameManager.Instance.JsonManager.DeserializeObject<AllErrorMessageInfo>(JsonText.ErrorMessages);

            messageManager.SetAllErrorMessageInfo(infos);

            return infos;
        }
    }
}
