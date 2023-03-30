using System.Collections.Generic;

namespace Barnabus
{
    public class AllPlayerBarnabusData : List<PlayerBarnabusData>
    {
        public PlayerBarnabusData GetBarnabusData(int id)
        {
            return Find(data => data.BaseData.CharacterID == id);
        }
    }
}
