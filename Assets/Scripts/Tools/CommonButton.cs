using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    /// <summary>
    /// UGUI Button 客製: tintColor情況下也會對子物件有作用
    /// </summary>
    public class CommonButton : Button
    {
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (!gameObject.activeInHierarchy)
                return;

            Color tintColor;

            switch (state)
            {
                case SelectionState.Normal:
                    tintColor = colors.normalColor;
                    break;
                case SelectionState.Highlighted:
                    tintColor = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    tintColor = colors.pressedColor;
                    break;
                case SelectionState.Selected:
                    tintColor = colors.selectedColor;
                    break;
                case SelectionState.Disabled:
                    tintColor = colors.disabledColor;
                    break;
                default:
                    tintColor = Color.black;
                    break;
            }

            switch (transition)
            {
                case Transition.ColorTint:
                    StartColorTweenChild(tintColor * colors.colorMultiplier, instant);
                    break;
            }
        }

        private void StartColorTweenChild(Color targetColor, bool instant)
        {
            if (targetGraphic == null)
                return;

            var targetGraphicChilds = targetGraphic.GetComponentsInChildren<Graphic>();

            foreach(Graphic child in targetGraphicChilds)
                child.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
        }
    }
}
