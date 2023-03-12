namespace Barnabus
{
    public class BarnabusBaseData
    {
        public int CharacterID;
        public string Name;
        public string Vocab;
        public int Batch;
        public AUDIO_NAME SoundKey;
        public bool AlreadyOwned;

        public BarnabusBaseData Copy()
        {
            var newData = new BarnabusBaseData();
            newData.CharacterID = CharacterID;
            newData.Name = Name;
            newData.Vocab = Vocab;
            newData.Batch = Batch;
            newData.SoundKey = SoundKey;
            newData.AlreadyOwned = AlreadyOwned;
            return newData;
        }
    }
}
