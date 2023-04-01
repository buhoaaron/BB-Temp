namespace Barnabus.SceneManagement
{
    public class StartSceneState : BaseSceneState
    {
        public StartSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        { }

        public override void Begin()
        {

        }

        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.LOADING_BASE);
        }

        public override void End()
        { 
        
        }

        
    }
}
