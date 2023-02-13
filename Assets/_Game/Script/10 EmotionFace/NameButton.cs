using UnityEngine;
using UnityEngine.UI;
using System;

namespace Barnabus.EmotionFace
{
    public class NameButton : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI text;
        public Image backgroundImage;
        public Button button;
        public int id;

        private Action<NameButton> onClick;

        public void OnClick()
        {
            onClick?.Invoke(this);
        }

        private void Update()
        {
            if (backgroundImage.enabled == false)
            {
                backgroundImage.enabled = true;
            }
        }

        public void SetOnClick(Action<NameButton> callback)
        {
            onClick = callback;
        }

        public void SetText(string content)
        {
            text.text = content;
        }

        public void SetEnable(bool enabled) { button.enabled = enabled; }
    }
}