using Barnabus.UI;
using Spine;
using UnityEngine;

namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 喚醒場景
    /// </summary>
    public class WakeUpSceneState : BaseSceneState
    {
        protected WakeUpUI wakeUpUI = null;
        protected EventData soundEventData;
        protected PlayerBarnabusData WakeUpBarnabusData;
        public WakeUpSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            wakeUpUI = GameObject.Find("Canvas_WakeUp").GetComponent<WakeUpUI>();
            wakeUpUI.Init();

            WakeUpBarnabusData = controller.GameManager.PlayerDataManager.UnlockBarnabusData;
            //喚醒
            WakeUpBarnabusData.IsWokenUp = true;
            //更換玩家選擇的角色
            wakeUpUI.ChangeBarnabusSpine(WakeUpBarnabusData.CharacterID);
            wakeUpUI.SetBarnabusName(WakeUpBarnabusData.Name);

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
            wakeUpUI.SpineBarnabus.AnimationState.SetAnimation(0, SpineLabels.BarnabusAction2, false);
            wakeUpUI.SpineBarnabus.AnimationState.AddAnimation(0, SpineLabels.BarnabusIdle, true, 0);

            soundEventData = wakeUpUI.SpineBarnabus.Skeleton.Data.FindEvent("sound");
            wakeUpUI.SpineBarnabus.AnimationState.Event += HandleAnimationStateEvent;
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
        {
            bool eventMatch = (soundEventData == e.Data); 
            if (eventMatch)
                controller.GameManager.AudioManager.PlaySound(WakeUpBarnabusData.SoundKey);
        }

        private void BackMainAndOpenShelf()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(MAIN_MENU.SHELF));
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
        private void BackMainAndOpenGameRoom()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(MAIN_MENU.GAME_ROOM));
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
        private void BackMainAndOpenLessons()
        {
            controller.GameManager.PlayerDataManager.SetSceneCacheData(new GameSceneCacheData(MAIN_MENU.LESSONS));
            controller.SetState(SCENE_STATE.LOADING_MAIN);
        }
    }
}
