using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Barnabus.SceneManagement
{
    public class BaseLoadingSceneState : BaseSceneState
    {
        protected Text textLoading = null;

        public BaseLoadingSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }

        public override void Begin()
        {

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
            
        }
    }
}
