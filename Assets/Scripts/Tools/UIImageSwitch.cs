﻿using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    /// <summary>
    /// 圖片開關
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class UIImageSwitch : MonoBehaviour
    {
        public Sprite SpriteOn;
        public Sprite SpriteOff;

        private Image image = null;

        private void Awake()
        {
            if (!TryGetComponent<Image>(out image))
                Debug.LogError("Image component is required.");
        }
    }
}
