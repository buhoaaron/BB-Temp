using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.SceneManagement
{
    public class LoadingDotToDotSceneState : BaseLoadingSceneState
    {
        public LoadingDotToDotSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.DOT_TO_DOT);
        }
    }
}
