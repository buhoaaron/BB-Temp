using UnityEngine;
using Barnabus.UI;

namespace Barnabus.SceneManagement
{
    public class BaseLoadingSceneState : BaseSceneState
    {
        protected bool isShowLoading = false;
        protected SceneLoadingUI sceneLoadingUI = null;

        public BaseLoadingSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }

        public override void Begin()
        {
            sceneLoadingUI = GameObject.FindObjectOfType<SceneLoadingUI>();
            sceneLoadingUI.Init();

            if (!isShowLoading)
                sceneLoadingUI.Hide();
        }

        public override void StateUpdate()
        {
            
        }

        public override void End()
        {
            
        }

        public override void LoadingSceneUpdate(float prograss)
        {
            SetLoadingPrograss(prograss);
        }

        protected void SetLoadingPrograss(float loadingPrograss)
        {
            Debug.Log("loadingPrograss = " + loadingPrograss);
            sceneLoadingUI.SetPrograss(loadingPrograss);
        }
    }
}
