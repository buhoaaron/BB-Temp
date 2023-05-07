using System.Collections.Generic;

namespace Barnabus
{
    /// <summary>
    /// 家庭帳號資訊
    /// </summary>
    public class FamiliesAccountInfo
    {
        #region FROM_SERVER
        public readonly int Meandmineid;
        public readonly string Token;
        public readonly int BirthYear;
        public readonly List<ProfileInfo> Profiles = new List<ProfileInfo>();
        #endregion

        public FamiliesAccountInfo(int meandmineid, string access_token, int birth_year = 0, List<ProfileInfo> profileInfos = null)
        {
            Meandmineid = meandmineid;
            Token = access_token;
            Profiles = profileInfos;
            BirthYear = birth_year;
        }

        public ProfileInfo GetProfileInfo(int id)
        {
            var info = Profiles.Find(x => x.player_id == id);

            return info;
        }

        public void AddProfile(ProfileInfo newInfo)
        {
            var isContain = IsProfileContain(newInfo.player_id);

            if (isContain)
                return;

            Profiles.Add(newInfo);
        }

        private bool IsProfileContain(int id)
        {
            var listPlayerId = Profiles.ConvertAll<int>((info) => info.player_id);

            return listPlayerId.Contains(id);
        }

        public int GetProfileCount()
        {
            if (Profiles == null)
                return 0;

            return Profiles.Count;
        }
    }
}
