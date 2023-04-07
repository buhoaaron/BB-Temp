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

        public WakeUpSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        {
            wakeUpUI = GameObject.Find("Canvas_WakeUp").GetComponent<WakeUpUI>();
            wakeUpUI.Init();

            var playerWakeUpBarnabusData = controller.GameManager.PlayerDataManager.UnlockBarnabusData;
            //喚醒
            playerWakeUpBarnabusData.IsWokenUp = true;
            //更換玩家選擇的角色
            wakeUpUI.ChangeBarnabusSpine(playerWakeUpBarnabusData.CharacterID);
            wakeUpUI.SetBarnabusName(playerWakeUpBarnabusData.Name);

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

        //TOFIX: 待美術統一EventName後要重構
        EventData eventData;
        protected void DoWakeUp()
        {
            wakeUpUI.SpineBarnabus.AnimationState.SetAnimation(0, SpineLabels.BarnabusAction2, false);
            wakeUpUI.SpineBarnabus.AnimationState.AddAnimation(0, SpineLabels.BarnabusIdle, true, 0);

            eventData = wakeUpUI.SpineBarnabus.Skeleton.Data.FindEvent("s_anger");
            wakeUpUI.SpineBarnabus.AnimationState.Event += HandleAnimationStateEvent;
        }

        private void HandleAnimationStateEvent(TrackEntry trackEntry, Spine.Event e)
        {
            bool eventMatch = (eventData == e.Data); 
            if (eventMatch)
            {
                controller.GameManager.AudioManager.PlaySound(AUDIO_NAME.ANGER_SOUND_01);
            }
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
