namespace Barnabus.EmotionMusic
{
    [System.Serializable]
    public class NotePointer
    {
        public int measureID;
        public int noteID;

        public NotePointer()
        {
            measureID = -1;
            noteID = -1;
        }

        public NotePointer(int measureID, int noteID)
        {
            this.measureID = measureID;
            this.noteID = noteID;
        }

        public int BeatID { get { return noteID % 4; } }

        public static bool IsNull(NotePointer pointer) { return pointer == null || pointer.IsNull(); }
        public bool IsNull() { return measureID == -1 || noteID == -1; }
        public bool Equal(NotePointer other) { return (measureID == other.measureID && noteID == other.noteID); }
        public new string ToString() { return "(" + measureID + ", " + noteID + ")"; }
    }
}