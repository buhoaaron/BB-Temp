using UnityEngine.Events;
using RenderHeads.Media.AVProVideo;
using DG.Tweening;

namespace Barnabus.UI
{
    /// <summary>
    /// 解鎖影片 UI
    /// </summary>
    public class UnlockVideoUI : BaseGameUI
    {
        public UnityAction OnMediaPlayerStarted = null;
        public UnityAction OnMediaPlayerFinished = null;

        public MediaPlayer MediaPlayer = null;
        public DisplayUGUI Display = null;

        public override void Init()
        {
            
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void Play()
        {
            MediaPlayer.Play();
            MediaPlayer.Events.AddListener(OnMediaPlayerEvent);
        }
        void OnMediaPlayerEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
        {
            switch (et)
            {
                case MediaPlayerEvent.EventType.Started:
                    ProcessMediaPlayerStarted(mp);
                    break;
                case MediaPlayerEvent.EventType.FinishedPlaying:
                    ProcessMediaPlayerFinished(mp);
                    break;
            }
        }

        private void ProcessMediaPlayerStarted(MediaPlayer mp)
        {
            OnMediaPlayerStarted?.Invoke();
        }

        private void ProcessMediaPlayerFinished(MediaPlayer mp)
        {
            //播完後會淡出並關閉
            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.7f);
            seq.Append(Display.DOFade(0, 0.5f));
            seq.AppendCallback(Hide);

            if (OnMediaPlayerFinished != null)
                seq.onComplete = OnMediaPlayerFinished.Invoke;
        }
    }
}
