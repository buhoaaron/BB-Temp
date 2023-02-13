using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.EmotionMusic
{
    public class NoteLinker : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image image;
        [SerializeField] private Animator animator;

        public NotePointer BeginNote { get; set; }

        private Action<NoteLinker> onClick;

        public void SetPosition(Vector2 beginNotePosition, Vector2 endNotePosition)
        {
            rectTransform.position = (beginNotePosition + endNotePosition) / 2f;
        }

        public void SetSize(Vector2 beginNoteCanvasPosition, Vector2 endNoteCanvasPosition)
        {
            rectTransform.sizeDelta = new Vector2(endNoteCanvasPosition.x - beginNoteCanvasPosition.x, rectTransform.sizeDelta.y);
        }

        public void SetOnClickCallback(Action<NoteLinker> onclick) { this.onClick = onclick; }
        public void OnClick() { onClick?.Invoke(this); }

        public void SetImage(Sprite sprite) { image.sprite = sprite; }
        public void Disappear() { animator.Play("Disappear", 0, 0); }
        public void OnLongPlayed() { animator.Play("OnLongPlayed", 0, 0); }
    }
}