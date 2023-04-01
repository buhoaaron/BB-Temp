namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 讀取遊戲基本資料
    /// </summary>
    public class LoadingBaseState : BaseLoadingSceneState
    {
        public LoadingBaseState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void Begin()
        {
            base.Begin();
            //讀取角色基本資料
            LoadBarnabusBaseData();
        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }

        private void LoadBarnabusBaseData()
        {
            controller.GameManager.CustomStartCoroutine(controller.GameManager.BarnabusCardManager.LoadBarnabusListAsync());
            controller.GameManager.BarnabusCardManager.LoadJson();
        }
    }
}
