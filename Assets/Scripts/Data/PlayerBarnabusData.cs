namespace Barnabus
{
    public class PlayerBarnabusData
    {
        public int CharacterID => BaseData.CharacterID;
        public int PotionExchange => BaseData.PotionExchange;
        public string Element => BaseData.Element;
        public string Name => BaseData.Name;

        public BarnabusBaseData BaseData { get; private set; }
        /// <summary>
        /// 玩家是否已解鎖
        /// </summary>
        public bool IsUnlocked = false;
        /// <summary>
        /// 玩家是否已喚醒
        /// </summary>
        public bool IsWokenUp = false;

        public PlayerBarnabusData(BarnabusBaseData barnabusBaseData)
        {
            BaseData = barnabusBaseData;

            IsUnlocked = barnabusBaseData.AlreadyOwned;
        }
    }
}
