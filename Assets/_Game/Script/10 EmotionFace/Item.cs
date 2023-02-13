using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Barnabus.EmotionFace
{
    public class Item : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Vector2 scaleRange = new Vector2(0.25f, 1f);
        public RectTransform rectTransform;
        public Image image;

        [HideInInspector]
        public ItemInfo info = new ItemInfo();

        private Action<Item> onPointerDown;
        private Action<Item> onPointerUp;

        private Rect rect;
        private Rect pictureRect;
        private Vector2 rotatedSize;

        public void Initialize(ItemInfo _info)
        {
            info = _info;
            rectTransform.localScale = new Vector3(info.scale, info.scale, 1);
            rectTransform.localEulerAngles = new Vector3(0, 0, info.rotation);
            SetPosition(info.positionRatio);
        }

        public void SetPictureRect(RectTransform pictureRectTransform) 
        {
            pictureRect = new Rect(-pictureRectTransform.sizeDelta / 2, pictureRectTransform.sizeDelta);
        }

        public void SetCallbacks(Action<Item> onPointerDown, Action<Item> onPointerUp)
        {
            this.onPointerDown = onPointerDown;
            this.onPointerUp = onPointerUp;
        }

        public void SetPosition(Vector2 positionRatio)
        {
            //rectTransform.anchoredPosition = new Vector2(pictureRect.center.x + (pictureRect.width / 2f) * positionRatio.x, pictureRect.center.y + (pictureRect.height / 2f) * positionRatio.y);
            rectTransform.anchoredPosition = (pictureRect.size / 2f) * positionRatio;

            OutOfRectRangeDetect();
            info.positionRatio = positionRatio;
        }

        public void SetScale(float value)
        {
            rectTransform.localScale = new Vector3(value, value, 0);

            if (rectTransform.localScale.x <= scaleRange.x) rectTransform.localScale = new Vector3(scaleRange.x, scaleRange.x, 1);
            else if (rectTransform.localScale.x >= scaleRange.y) rectTransform.localScale = new Vector3(scaleRange.y, scaleRange.y, 1);

            info.scale = rectTransform.localScale.x;
            OutOfRectRangeDetect();
        }

        public void SetRotation(float value)
        {
            rectTransform.localEulerAngles = new Vector3(0, 0, value);
            info.rotation = rectTransform.localEulerAngles.z;
            OutOfRectRangeDetect();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) onPointerDown?.Invoke(this);
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began)
                onPointerDown?.Invoke(this);
#endif
        }

        public void OnPointerUp(PointerEventData eventData)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonUp(0)) onPointerUp?.Invoke(this);
#elif UNITY_ANDROID || UNITY_IOS
            if (Input.touches[0].phase == TouchPhase.Ended)
                onPointerUp?.Invoke(this);
#endif
        }

        private void OutOfRectRangeDetect()
        {
            rotatedSize = rectTransform.sizeDelta * (Mathf.Cos((info.rotation % 90) * Mathf.Deg2Rad) + Mathf.Sin((info.rotation % 90) * Mathf.Deg2Rad));

            rect.xMin = rectTransform.anchoredPosition.x - rotatedSize.x * info.scale / 2f;
            rect.xMax = rectTransform.anchoredPosition.x + rotatedSize.x * info.scale / 2f;
            rect.yMin = rectTransform.anchoredPosition.y - rotatedSize.y * info.scale / 2f;
            rect.yMax = rectTransform.anchoredPosition.y + rotatedSize.y * info.scale / 2f;

            if (rect.xMin < pictureRect.xMin)
                rectTransform.anchoredPosition += new Vector2(pictureRect.xMin - rect.xMin, 0);
            else if (rect.xMax > pictureRect.xMax)
                rectTransform.anchoredPosition -= new Vector2(rect.xMax - pictureRect.xMax, 0);
            if (rect.yMin < pictureRect.yMin)
                rectTransform.anchoredPosition += new Vector2(0, pictureRect.yMin - rect.yMin);
            else if (rect.yMax > pictureRect.yMax)
                rectTransform.anchoredPosition -= new Vector2(0, rect.yMax - pictureRect.yMax);
        }
    }
}