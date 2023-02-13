namespace Barnabus
{
    [System.Serializable]
    public class CharacterData
    {
        public int id;
        public bool isUnlocked;
        public string hatchTimestampUTC;
        public int timeSinceLastHatchSec;

        public CharacterData()
        {
            id = -1;
            isUnlocked = false;
            hatchTimestampUTC = "null";
            timeSinceLastHatchSec = -1;
        }

        public CharacterData(int id, bool isUnlocked, string hatchTimestampUTC, int timeSinceLastHatchSec)
        {
            this.id = id;
            this.isUnlocked = isUnlocked;
            this.hatchTimestampUTC = hatchTimestampUTC;
            this.timeSinceLastHatchSec = timeSinceLastHatchSec;
        }

        public CharacterData(CharacterData data)
        {
            id = data.id;
            isUnlocked = data.isUnlocked;
            hatchTimestampUTC = data.hatchTimestampUTC;
            timeSinceLastHatchSec = data.timeSinceLastHatchSec;
        }
    }
}