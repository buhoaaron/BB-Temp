using System.Collections.Generic;

namespace Barnabus.Network
{
    public class ReceiveLogin : ReceiveSignUp
    {
        public readonly List<ProfileInfo> players_list;
        public readonly int birth_year;

        public ReceiveLogin(int meandmine_id, string access_token, List<ProfileInfo> players_list, int birth_year) : base(meandmine_id, access_token)
        {
            this.players_list = players_list;
            this.birth_year = birth_year;
        }
    }
}
