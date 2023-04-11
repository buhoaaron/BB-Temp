namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 讀取登入場景狀態
    /// </summary>
    public class LoadingLoginSceneState : BaseLoadingSceneState
    {
        public LoadingLoginSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void Begin()
        {
            base.Begin();
        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.LOGIN);
        }
    }
}
