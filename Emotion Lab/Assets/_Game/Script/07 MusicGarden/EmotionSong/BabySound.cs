namespace Barnabus.MusicGarden.EmotionSong
{
    [System.Serializable]
    public class BabySound
    {
        public int buttonID;
        public int soundID;
        public float pitch;

        public BabySound(int buttonID, int soundID, float pitch)
        {
            this.buttonID = buttonID;
            this.soundID = soundID;
            this.pitch = pitch;
        }
    }
}