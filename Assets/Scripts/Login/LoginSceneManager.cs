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

        #region BASE_API
        public void Init()
        {
            pageManager = new PageManager();
            prefabPool = transform.Find("PrefabPool").GetComponent<PrefabPool>();

            pageManager.Init();
            IdentificationUI.Init();

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
            return pageManager.GetPage(pageKey);
        }
    }
}
