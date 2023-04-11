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
            //讀取音樂音效靜音設定
            LoadAudioMuteSetting();
        }
        public override void StateUpdate()
        {
            controller.SetState(SCENE_STATE.LOADING_LOGIN);
        }

        private void LoadBarnabusBaseData()
        {
            controller.GameManager.CustomStartCoroutine(controller.GameManager.BarnabusCardManager.LoadBarnabusListAsync());
            controller.GameManager.BarnabusCardManager.LoadJson();
        }

        private void LoadAudioMuteSetting()
        {
            var isMuteSound = controller.GameManager.PlayerDataManager.GetMuteSound();
            var isMuteBGM = controller.GameManager.PlayerDataManager.GetMuteBGM();

            controller.GameManager.AudioManager.SetMuteAll(isMuteSound || isMuteBGM);
        }
    }
}
