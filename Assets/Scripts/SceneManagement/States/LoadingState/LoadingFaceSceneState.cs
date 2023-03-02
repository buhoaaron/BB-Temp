namespace Barnabus.SceneManagement
{
    public class LoadingFaceSceneState : BaseLoadingSceneState
    {
        public LoadingFaceSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.FACE);
        }
    }
}
