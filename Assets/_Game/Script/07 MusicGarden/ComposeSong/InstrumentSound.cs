namespace Barnabus.MusicGarden.ComposeSong
{
    [System.Serializable]
    public class InstrumentSound
    {
        public int instrument;
        public float pitch;

        public InstrumentSound(int instrument, float pitch)
        {
            this.instrument = instrument;
            this.pitch = pitch;
        }
    }
}