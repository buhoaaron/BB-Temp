using System.Collections.Generic;

namespace HiAndBye
{
    public class AllRankInfo : List<RankInfo>
    {
        public RankInfo GetRankInfo(RANK rank)
        {
            return Find(info => info.Rank == rank);
        }
    }
}
