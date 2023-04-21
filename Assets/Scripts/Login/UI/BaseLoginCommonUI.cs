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
        public virtual void Init(Camera camera)
        {
            SetCanvasCamera(camera);

            Init();
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

        public override void DoPopUp(TweenCallback onComplete = null)
        {
            Debug.AssertFormat(root != null, "not set to Root");
            Debug.AssertFormat(mask != null, "not set to Mask");

            ResetPopUp();

            Sequence seq = DOTween.Sequence();
            seq.Append(root.DOScale(1, 0.3f).SetEase(Ease.OutBack));
            seq.Join(mask.DOFade(0.4f, 0.2f));
            seq.onComplete = onComplete;
        }
    }
}
