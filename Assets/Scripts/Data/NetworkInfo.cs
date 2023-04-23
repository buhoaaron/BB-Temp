namespace Barnabus
{
    /// <summary>
    /// 玩家資訊(Server給的)
    /// </summary>
    public class NetworkInfo
    {
        public readonly int Meandmineid;
        public readonly string Token;

        public NetworkInfo(int meandmineid, string access_token)
        {
            Meandmineid = meandmineid;
            Token = access_token;
        }
    }
}
