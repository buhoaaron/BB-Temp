using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionMusic
{
    public class MeasureView : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField]
        private NoteButton noteButton;

        private List<NoteButton> noteButtons = new List<NoteButton>();

        public void Initialize(int id, int noteAmount, Action<NoteButton> noteClickedEvent
                                                     , Action<NoteButton> onNotePointerEnter
                                                     , Action<NoteButton> onNotePointerExit
                                                     , Action<NoteButton> onNotePointerDown
                                                     , Action<NoteButton> onNotePointerUp)
        {
            for (int i = 0; i < noteButtons.Count; i++) Destroy(noteButtons[i].gameObject);
            noteButtons.Clear();

            NoteButton newButton;
            for (int i = 0; i < noteAmount; i++)
            {
                newButton = Instantiate(noteButton, this.transform);
                newButton.Initialize(id, i, noteClickedEvent, onNotePointerEnter, onNotePointerExit, onNotePointerDown, onNotePointerUp);
                noteButtons.Add(newButton);
            }
        }

        public NoteButton GetNoteButton(int noteID)
        {
            return noteButtons[noteID];
        }
    }
}