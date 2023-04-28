using System.Collections.Generic;

namespace Barnabus.Network
{
    public class ReceiveLogin : ReceiveSignUp
    {
        public readonly List<ProfileInfo> players_list;

        public ReceiveLogin(int meandmineid, string access_token, List<ProfileInfo> players_list) : base(meandmineid, access_token)
        {
            this.players_list = players_list;
        }
    }
}
