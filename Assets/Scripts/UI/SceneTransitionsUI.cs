using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Barnabus.UI
{
    public class SceneTransitionsUI : BaseGameUI
    {
        private float fadeDuration = 0.3f;
        private Image imageDarkScreen = null;

        public override void Init()
        {
            if (imageDarkScreen == null)
                imageDarkScreen = transform.Find("Image_DarkScreen").GetComponent<Image>();

            UITool.SetAlpha(imageDarkScreen, 0);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void DoFadeIn(TweenCallback onComplete)
        {
            Init();

            UITool.SetAlpha(imageDarkScreen, 0);

            var seq = DOTween.Sequence();
            seq.Append(imageDarkScreen.DOFade(1, fadeDuration));
            //seq.AppendInterval(0.05f);
            seq.onComplete = onComplete;
        }

        public void DoFadeOut(TweenCallback onComplete)
        {
            Init();

            UITool.SetAlpha(imageDarkScreen, 1);

            var seq = DOTween.Sequence();
            //seq.AppendInterval(0.05f);
            seq.Append(imageDarkScreen.DOFade(0, fadeDuration));
            seq.onComplete = onComplete;
        }
    }
}
