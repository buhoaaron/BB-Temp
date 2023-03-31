using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;

namespace Barnabus.UI
{
    public class WakeUpUI : BaseGameUI
    {
        public UnityAction OnButtonLessonsClick = null;
        public UnityAction OnButtonGameRoomClick = null;
        public UnityAction OnButtonReturnClick = null;

        [Header("Set UI Components")]
        public UnlockVideoUI UnlockVideoUI = null;
        public Button ButtonLessons;
        public Button ButtonReturn;
        public Button ButtonGameRoom;
        public TMP_Text TextBarnabusName;

        public SkeletonGraphic SpineBarnabus = null;

        private CanvasGroup rootCanvasGroup = null;

        public override void Init()
        {
            rootCanvasGroup = root.GetComponent<CanvasGroup>();

            buttons.Add(ButtonLessons);
            buttons.Add(ButtonReturn);
            buttons.Add(ButtonGameRoom);

            ButtonLessons.onClick.AddListener(ProcessButtonLessonsClick);
            ButtonGameRoom.onClick.AddListener(ProcessButtonGameRoomClick);
            ButtonReturn.onClick.AddListener(ProcessButtonReturnClick);

            UnlockVideoUI.OnMediaPlayerStarted = ProcessMediaPlayerStarted;
            UnlockVideoUI.Init();
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void CloseUnlockVideoUI()
        {
            UnlockVideoUI.Hide();
        }

        public void DoUnlock()
        {
            //先隱藏角色喚醒介面，待解鎖動畫出現後再顯現
            rootCanvasGroup.alpha = 0;

            UnlockVideoUI.Play();
        }

        private void ProcessMediaPlayerStarted()
        {
            //開始播放解鎖動畫的同時顯現角色喚醒介面
            rootCanvasGroup.alpha = 1;
        }

        private void ProcessButtonLessonsClick()
        {
            OnButtonLessonsClick?.Invoke();
        }
        private void ProcessButtonGameRoomClick()
        {
            OnButtonGameRoomClick?.Invoke();
        }
        private void ProcessButtonReturnClick()
        {
            OnButtonReturnClick?.Invoke();
        }

        public void SetBarnabusName(string name)
        {
            TextBarnabusName.text = name;
        }
        public SkeletonGraphic ChangeBarnabusSpine(int characterID)
        {
            var barnabusCard = NewGameManager.Instance.BarnabusCardManager.GetCard(characterID);
            SpineBarnabus.skeletonDataAsset = barnabusCard.skeletonData;
            SpineBarnabus.Initialize(true);

            return SpineBarnabus;
        }
    }
}
