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
            //建立玩家擁有的角色資料
            CreatePlayerBarnabusData();
            //建立玩家擁有的藥水資料
            CreatePlayerPotionData();
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

        private void CreatePlayerBarnabusData()
        {
            var allData = controller.GameManager.BarnabusCardManager.GetAllBarnabusBaseData();
            controller.GameManager.PlayersBarnabusManager.InitPlayerBarnabusData(allData);

            controller.GameManager.PlayersBarnabusManager.LoadPlayerBarnabusData();
        }

        private void CreatePlayerPotionData()
        {
            //讀取玩家的藥水
            //DataManager.LoadPotions();
        }
    }
}
