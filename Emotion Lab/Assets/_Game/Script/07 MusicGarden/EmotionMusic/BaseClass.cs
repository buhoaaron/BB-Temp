using System.Collections.Generic;

namespace Barnabus.EmotionMusic
{
    [System.Serializable]
    public class Sheet<T>
    {
        public int bpm;
        public List<Measure<T>> measures;

        public Sheet()
        {
            bpm = 120;
            measures = new List<Measure<T>>();
        }

        public void AddMeasure()
        {
            measures.Add(new Measure<T>());
        }

        public void RemoveLastMeasure()
        {
            measures.RemoveAt(measures.Count - 1);
        }

        public void AddNote(int measureID, int noteID, T noteInfo)
        {
            measures[measureID].beats[noteID % 4].AddNote(noteID, noteInfo);
        }

        public void RemoveNote(int measureID, int noteID)
        {
            measures[measureID].beats[noteID % 4].RemoveNote(noteID);
        }

        public void RemoveNote(NotePointer pointer)
        {
            measures[pointer.measureID].beats[pointer.BeatID].RemoveNote(pointer.noteID);
        }

        public Note<T> GetNote(NotePointer pointer)
        {
            return measures[pointer.measureID].beats[pointer.BeatID].notes.Find(x => x.id == pointer.noteID);
        }
    }

    [System.Serializable]
    public class Measure<T>
    {
        public Beat<T>[] beats;

        public Measure()
        {
            beats = new Beat<T>[4];
            for (int i = 0; i < 4; i++) beats[i] = new Beat<T>();
        }
    }

    [System.Serializable]
    public class Beat<T>
    {
        public List<Note<T>> notes;

        public Beat()
        {
            notes = new List<Note<T>>();
        }

        public void AddNote(int noteID, T noteInfo)
        {
            if (IsExisted(noteID)) return;
            notes.Add(new Note<T>(noteID, noteInfo));
        }

        public void RemoveNote(int noteID)
        {
            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i].id == noteID)
                {
                    notes.RemoveAt(i);
                    return;
                }
            }
        }

        private bool IsExisted(int noteID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].id == noteID)
                    return true;

            return false;
        }
    }

    [System.Serializable]
    public class Note<T>
    {
        public int id;
        public T info;

        public Note(int id, T info)
        {
            this.id = id;
            this.info = info;
        }
    }
}