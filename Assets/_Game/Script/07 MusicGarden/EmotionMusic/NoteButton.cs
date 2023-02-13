using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.EmotionMusic
{
    public class NoteButton : MonoBehaviour
    {
        public RectTransform rectTransform;
        [SerializeField]
        private Image dotImage;
        [SerializeField]
        private Animator animator;

        public int MeasureID { get; private set; }
        public int NoteID { get; private set; }
        private Action<NoteButton> clickedEvent;
        private Action<NoteButton> onPointerEnter;
        private Action<NoteButton> onPointerExit;
        private Action<NoteButton> onPointerDown;
        private Action<NoteButton> onPointerUp;

        public void Initialize(int measureID, int noteID, Action<NoteButton> noteClickedEvent
                                                        , Action<NoteButton> onPointerEnter
                                                        , Action<NoteButton> onPointerExit
                                                        , Action<NoteButton> onPointerDown
                                                        , Action<NoteButton> onPointerUp)
        {
            MeasureID = measureID;
            NoteID = noteID;
            clickedEvent = noteClickedEvent;
            this.onPointerEnter = onPointerEnter;
            this.onPointerExit = onPointerExit;
            this.onPointerDown = onPointerDown;
            this.onPointerUp = onPointerUp;
        }

        public void OnClick() { clickedEvent?.Invoke(this); }

        public void Show() { animator.Play("Show", 0, 0); }
        public void Hide() { animator.Play("Hide", 0, 0); }
        public void Appear() { animator.Play("Appear", 0, 0); }
        public void Disappear() { animator.Play("Disappear", 0, 0); }
        public void OnPlayed() { animator.Play("OnPlayed", 0, 0); }
        public void OnLongPlayed() { animator.Play("OnLongPlayed", 0, 0); }

        public void SetColor(Color color) { dotImage.color = color; }
        public void SetSprite(Sprite sprite) { dotImage.sprite = sprite; }

        public void OnPointerEnter() { onPointerEnter?.Invoke(this); }
        public void OnPointerExit() { onPointerExit?.Invoke(this); }
        public void OnPointerDown() { onPointerDown?.Invoke(this); }
        public void OnPointerUp() { onPointerUp?.Invoke(this); }

        public NotePointer ToPointer() { return new NotePointer(MeasureID, NoteID); }
    }
}