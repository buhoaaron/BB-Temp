using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

namespace Barnabus
{
    /// <summary>
    /// 敬請期待提示物件
    /// </summary>
    public class ComingSoonMask : MonoBehaviour
    {
        public Button ButtonComingSoon = null;
        public TMPro.TMP_Text TextTip = null;

        private Sequence seq = null;

        private void Start()
        {
            TextTip.DOFade(0, 0f);

            ButtonComingSoon.onClick.AddListener(DoShowTip);
        }

        private void Clear()
        {
            seq = null;
        }

        private void DoShowTip()
        {
            if (seq != null)
                return;

            seq = DOTween.Sequence();
            seq.Append(TextTip.DOFade(1, 0f));
            seq.AppendInterval(1.0f);
            seq.Append(TextTip.DOFade(0, 0.3f));
            seq.onComplete = Clear;
        }
    }
}
