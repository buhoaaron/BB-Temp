using System;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.MusicGarden
{
    public class NoteButton : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Animator animator;

        private int measureID;
        private int id;
        private Action<int, int> clickedEvent;

        public void Initialize(int measureID, int noteID, Action<int, int> noteClickedEvent)
        {
            this.measureID = measureID;
            id = noteID;
            clickedEvent = noteClickedEvent;
        }

        public void OnClick() 
        { 
            clickedEvent?.Invoke(measureID, id); 
        }

        public void Appear()
        {
            animator.Play("Appear", 0, 0);
        }

        public void Disappear()
        {
            animator.Play("Disappear", 0, 0);
        }

        public void SetImage(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }
    }
}