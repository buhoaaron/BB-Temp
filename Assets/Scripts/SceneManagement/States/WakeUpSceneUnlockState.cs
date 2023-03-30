
namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 喚醒場景(播放解鎖動畫)
    /// </summary>
    public class WakeUpSceneUnlockState : WakeUpSceneState
    {
        public WakeUpSceneUnlockState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        protected override void ProcessUnlockVideoUI()
        {
            wakeUpUI.UnlockVideoUI.OnMediaPlayerFinished = DoWakeUp;
            wakeUpUI.DoUnlock();
        }
    }
}
