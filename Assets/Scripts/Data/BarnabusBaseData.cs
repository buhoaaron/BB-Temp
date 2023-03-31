namespace Barnabus
{
    public class BarnabusBaseData
    {
        public readonly int CharacterID;
        public readonly string Name;
        public readonly string Vocab;
        public readonly int Batch;
        public readonly AUDIO_NAME SoundKey;
        public readonly bool AlreadyOwned;
        public readonly string Element;
        public readonly COLOR Color;
        public readonly int PotionExchange;

        public BarnabusBaseData(int characterID, string name, string vocab, int batch, AUDIO_NAME soundKey, bool alreadyOwned, string element, COLOR color, int potionExchange)
        {
            CharacterID = characterID;
            Name = name;
            Vocab = vocab;
            Batch = batch;
            SoundKey = soundKey;
            AlreadyOwned = alreadyOwned;
            Element = element;
            Color = color;
            PotionExchange = potionExchange;
        }
    }
}
