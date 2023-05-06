namespace Barnabus.Network
{
    public class ReceiveCreatePlayer : ReceiveSignUp
    {
        public readonly int player_id;
        public ReceiveCreatePlayer(int meandmineid, string access_token, int player_id) : base(meandmineid, access_token)
        {
            this.player_id = player_id;
        }
    }
}
