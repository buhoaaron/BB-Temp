using UnityEngine;
using Barnabus.UI;
using DG.Tweening;

namespace Barnabus.Login
{
    /// <summary>
    /// Login流程UI的共用基底
    /// </summary>
    public abstract class BaseLoginCommonUI : BaseGameUI
    {
        protected CanvasGroup canvasGroup = null;

        private Sequence seqShow = null;
        private void Awake()
        {
            //add用來做淡入淡出的元件
            if (!TryGetComponent<CanvasGroup>(out canvasGroup))
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        public void SetCanvasCamera(Camera camera)
        {
            GetComponent<Canvas>().worldCamera = camera;
        }

        public void DoShift(bool isForward, TweenCallback onComplete = null)
        {
            var tweener = root.DOLocalMoveX(isForward ? -1920 : 0, 0.3f).SetEase(Ease.Linear);
            tweener.onComplete = onComplete;
        }

        public void DoPopUp(TweenCallback onComplete = null, float delay = 0)
        {
            Debug.AssertFormat(root != null, "not set to Root");
            Debug.AssertFormat(mask != null, "not set to Mask");

            ResetPopUp();

            seqShow = DOTween.Sequence();
            seqShow.AppendInterval(delay);
            seqShow.Append(root.DOScale(1, 0.3f).SetEase(Ease.OutBack));
            seqShow.Join(mask.DOFade(0.4f, 0.2f));
            seqShow.onComplete = onComplete;
        }

        public void DoFade(float endValue, float duration, TweenCallback onComplete = null)
        {
            Tweener t = canvasGroup.DOFade(endValue, duration);
            t.onComplete = onComplete;
        }

        public override void Destroy()
        {
            if (seqShow != null)
                seqShow.Kill();

            Destroy(gameObject);
        }
    }
}
