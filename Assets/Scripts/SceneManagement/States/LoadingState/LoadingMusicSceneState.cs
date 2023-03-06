using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.SceneManagement
{
    public class LoadingMusicSceneState : BaseLoadingSceneState
    {
        public LoadingMusicSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.MUSIC);
        }
    }
}
