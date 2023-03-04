namespace Barnabus.SceneManagement
{
    public class LoadingMainSceneState : BaseLoadingSceneState
    {
        public LoadingMainSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void Begin()
        {
            base.Begin();
            
            NewGameManager.Instance.LoadBarnabusListAsync();
        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.MAIN);
        }
    }
}
