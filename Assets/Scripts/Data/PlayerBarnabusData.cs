namespace Barnabus
{
    public class PlayerBarnabusData
    {
        public BarnabusBaseData BaseData = null;

        public bool IsUnlocked;

        public PlayerBarnabusData(BarnabusBaseData barnabusBaseData)
        {
            BaseData = barnabusBaseData;

            IsUnlocked = barnabusBaseData.AlreadyOwned;
        }
    }
}
