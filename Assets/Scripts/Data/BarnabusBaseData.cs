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
        public string Element;
        public COLOR Color;
        public int PotionExchange;
        public BarnabusBaseData Copy()
        {
            var newData = new BarnabusBaseData();
            newData.CharacterID = CharacterID;
            newData.Name = Name;
            newData.Vocab = Vocab;
            newData.Batch = Batch;
            newData.SoundKey = SoundKey;
            newData.AlreadyOwned = AlreadyOwned;
            newData.Element = Element;
            newData.Color = Color;
            newData.PotionExchange = PotionExchange;
            return newData;
        }
    }
}
