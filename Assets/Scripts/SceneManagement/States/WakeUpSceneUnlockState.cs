
namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 喚醒場景(播放解鎖動畫)
    /// </summary>
    public class WakeUpSceneUnlockState : WakeUpSceneState
    {
        public WakeUpSceneUnlockState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            base.Begin();

            var playerUnlockBarnabusData = controller.GameManager.PlayerDataManager.UnlockBarnabusData;

            //扣藥水
            var potionExchange = playerUnlockBarnabusData.PotionExchange;
            controller.GameManager.PlayerDataManager.ReducePotionAmount(potionExchange);
            //解鎖角色
            var characterID = playerUnlockBarnabusData.CharacterID;
            controller.GameManager.PlayerDataManager.UnlockCharacter(characterID);
            //存檔
            controller.GameManager.PlayerDataManager.Save();
        }

        protected override void ProcessUnlockVideoUI()
        {
            wakeUpUI.UnlockVideoUI.OnMediaPlayerFinished = DoWakeUp;
            wakeUpUI.DoUnlock();
        }
    }
}
