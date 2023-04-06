using UnityEngine;

namespace HiAndBye
{
    public class RankManager : HiAndByeBaseManager
    {
        private AllRankInfo allRankInfo = null;
        private RankInfo playerRankInfo = null;

        public RankManager(HiAndByeGameManager gm) : base(gm)
        {

        }

        public void Check(int correntNum, float countDownTime)
        {
            var rank1 = 0;
            var rank2 = Mathf.Floor(countDownTime / 2.0f);
            var rank3 = Mathf.Floor(countDownTime / 0.9f);

            if (correntNum >= rank3)
                playerRankInfo = allRankInfo.GetRankInfo(RANK.RANK_3);
            else if (correntNum >= rank2)
                playerRankInfo = allRankInfo.GetRankInfo(RANK.RANK_2);
            else if (correntNum >= rank1)
                playerRankInfo = allRankInfo.GetRankInfo(RANK.RANK_1);
        }

        public void SetAllRankInfo(AllRankInfo allRankInfo)
        {
            this.allRankInfo = allRankInfo;
        }

        public RankInfo GetRankInfo()
        {
            Debug.Log(string.Format("Rank: {0}, Potions: {1}", playerRankInfo.Rank, playerRankInfo.Potions));
            return playerRankInfo;
        }
    }
}
