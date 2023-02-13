using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.MusicGarden
{
    public class MeasureView : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField]
        private NoteButton noteButton;

        private List<NoteButton> noteButtons;

        public void Initialize(int id, int noteAmount, Action<int, int> noteClickedEvent)
        {
            noteButtons = new List<NoteButton>();

            NoteButton newButton;
            for (int i = 0; i < noteAmount; i++)
            {
                newButton = Instantiate(noteButton, this.transform);
                newButton.Initialize(id, i, noteClickedEvent);
                noteButtons.Add(newButton);
            }
        }

        public NoteButton GetNoteButton(int noteID)
        {
            return noteButtons[noteID];
        }
    }
}