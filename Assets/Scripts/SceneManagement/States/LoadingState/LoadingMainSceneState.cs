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
            //讀取角色基本資料
            LoadBarnabusBaseData();
            //讀取玩家擁有的角色資料
            LoadPlayerBarnabus();
            //讀取玩家擁有的藥水資料
            LoadPlayerPotions();
        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.MAIN);
        }

        private void LoadBarnabusBaseData()
        {
            controller.GameManager.CustomStartCoroutine(controller.GameManager.BarnabusCardManager.LoadBarnabusListAsync());
            controller.GameManager.BarnabusCardManager.LoadJson();
        }

        private void LoadPlayerBarnabus()
        {
            controller.GameManager.PlayerDataManager.LoadPlayerBarnabus();
        }

        private void LoadPlayerPotions()
        {
            controller.GameManager.PlayerDataManager.LoadPlayerPotions();
        }
    }
}
