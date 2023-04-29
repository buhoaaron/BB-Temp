using System.Collections.Generic;

namespace Barnabus
{
    /// <summary>
    /// 玩家網路資訊(Server給的)
    /// </summary>
    public class NetworkInfo
    {
        public readonly int Meandmineid;
        public readonly string Token;
        public readonly List<ProfileInfo> Profiles;

        public NetworkInfo(int meandmineid, string access_token, List<ProfileInfo> profileInfos = null)
        {
            Meandmineid = meandmineid;
            Token = access_token;
            Profiles = profileInfos;
        }

        public int GetProfileCount()
        {
            if (Profiles == null)
                return 0;

            return Profiles.Count;
        }
    }
}
