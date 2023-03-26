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
            //設定玩家擁有的角色
            SetPlayerBarnabus();
            //讀取玩家的藥水
            DataManager.LoadPotions();
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

        private void SetPlayerBarnabus()
        {
            var allData = controller.GameManager.BarnabusCardManager.GetAllBarnabusBaseData();
            controller.GameManager.PlayersBarnabusManager.SetPlayerBarnabusData(allData);
        }
    }
}
