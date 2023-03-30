/*FIXED: 2023.3.30 Remove unused property*/

namespace Barnabus
{
    [System.Serializable]
    public class CharacterData
    {
        public int id;
        public bool isUnlocked;

        public CharacterData()
        {
            id = -1;
            isUnlocked = false;
        }

        public CharacterData(int id, bool isUnlocked)
        {
            this.id = id;
            this.isUnlocked = isUnlocked;
        }

        public CharacterData(CharacterData data)
        {
            id = data.id;
            isUnlocked = data.isUnlocked;
        }
    }
}