namespace Barnabus.EmotionMusic
{
    [System.Serializable]
    public class CharacterSound
    {
        public int characterID;
        public float pitch;
        public NotePointer linkNote;
        public bool isLongSound;

        public CharacterSound(int characterID, float pitch, bool isLongSound = false, NotePointer linkNote = null)
        {
            this.characterID = characterID;
            this.pitch = pitch;
            this.isLongSound = isLongSound;
            this.linkNote = linkNote;
        }
    }
}