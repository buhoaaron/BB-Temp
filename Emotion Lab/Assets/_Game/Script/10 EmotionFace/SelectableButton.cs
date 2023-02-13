using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.EmotionFace
{
    public class SelectableButton : MonoBehaviour
    {
        public Image backgroundImage;
        public Image buttonIcon;

        [HideInInspector]
        public string parameter;
        [HideInInspector]
        public Action<SelectableButton> onClick;

        public void OnClick()
        {
            onClick?.Invoke(this);
        }
    }
}