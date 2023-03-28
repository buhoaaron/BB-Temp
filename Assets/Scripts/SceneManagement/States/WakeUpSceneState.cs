using Barnabus.UI;
using UnityEngine;

namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 喚醒場景
    /// </summary>
    public class WakeUpSceneState : BaseSceneState
    {
        protected WakeUpUI wakeUpUI = null;

        public WakeUpSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            wakeUpUI = GameObject.Find("Canvas_WakeUp").GetComponent<WakeUpUI>();
            wakeUpUI.Init();

            wakeUpUI.OnButtonReturnClick = BackMainAndOpenShelf;
            wakeUpUI.OnButtonLessonsClick = BackMainAndOpenLessons;
            wakeUpUI.OnButtonGameRoomClick = BackMainAndOpenGameRoom;

            controller.GameManager.AudioManager.AddButton(AUDIO_NAME.BUTTON_CLICK, wakeUpUI.Buttons);

            ProcessUnlockVideoUI();
        }

        protected virtual void ProcessUnlockVideoUI()
        {
            wakeUpUI.CloseUnlockVideoUI();

            DoWakeUp();
        }

        protected void DoWakeUp()
        {
            wakeUpUI.SpineBarnabus.AnimationState.SetAnimation(0, "s4", false);
            wakeUpUI.SpineBarnabus.AnimationState.AddAnimation(0, "s2", true, 0);

            controller.GameManager.AudioManager.PlaySound(AUDIO_NAME.ANGER_SOUND_01, 1.0f);
        }

        private void BackMainAndOpenShelf()
        {
            controller.GameManager.GameSceneData = new GameSceneData(MAIN_MENU.SHELF);
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
        private void BackMainAndOpenGameRoom()
        {
            controller.GameManager.GameSceneData = new GameSceneData(MAIN_MENU.GAME_ROOM);
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
        private void BackMainAndOpenLessons()
        {
            controller.GameManager.GameSceneData = new GameSceneData(MAIN_MENU.LESSONS);
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
    }
}
