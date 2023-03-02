using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.SceneManagement
{
    public class LoadingHiAndByeSceneState : BaseLoadingSceneState
    {
        public LoadingHiAndByeSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.HI_AND_BYE);
        }
    }
}
