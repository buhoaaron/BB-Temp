using System.Collections.Generic;

namespace Barnabus.EmotionMusic
{
    [System.Serializable]
    public class EM_Data
    {
        public List<int> charactersID;
        public int songID;
        public Sheet<CharacterSound> sheet;

        public EM_Data()
        {
            charactersID = new();
            sheet = new();
        }
    }
}