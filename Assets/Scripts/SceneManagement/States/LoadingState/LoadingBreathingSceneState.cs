namespace Barnabus.SceneManagement
{
    public class LoadingBreathingSceneState : BaseLoadingSceneState
    {
        public LoadingBreathingSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.BREATHING);
        }
    }
}
