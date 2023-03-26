﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

namespace Barnabus.UI
{
    public abstract class BaseGameUI : MonoBehaviour, IBaseUI
    {
        public List<Button> Buttons => buttons;
        protected List<Button> buttons = new List<Button>();
        [SerializeField]
        protected RectTransform root = null;
        [SerializeField]
        protected Image mask = null;

        public abstract void Init();
        public abstract void UpdateUI();
        public abstract void Clear();

        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void DoPopUp(TweenCallback onComplete = null)
        {
            Debug.AssertFormat(root != null, "not set to Root");
            Debug.AssertFormat(mask != null, "not set to Mask");

            ResetPopUp();

            Sequence seq = DOTween.Sequence();
            seq.Append(root.DOScale(1, 0.3f).SetEase(Ease.OutBack));
            seq.Append(mask.DOFade(0.78f, 0.2f));
            seq.onComplete = onComplete;
        }

        public void ResetPopUp()
        {
            root.localScale = Vector2.zero;
            UITool.SetAlpha(mask, 0);
        }
    }
}
