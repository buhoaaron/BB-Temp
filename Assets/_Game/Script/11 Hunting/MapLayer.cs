using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.Hunting
{
    public class MapLayer : MonoBehaviour
    {
        [Range(0, 1)]
        public float followRatio;

        private RectTransform rectTransform;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void SetPosition(float x)
        {
            rectTransform.anchoredPosition = new Vector2(x * followRatio, rectTransform.anchoredPosition.y);
        }
    }
}