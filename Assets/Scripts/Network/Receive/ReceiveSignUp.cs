namespace Barnabus.Network
{
    public class ReceiveSignUp : BaseReceivePacket
    {
        public readonly int meandmineid = 0;
        public readonly string access_token = "";

        public ReceiveSignUp(int meandmineid, string access_token)
        {
            this.meandmineid = meandmineid;
            this.access_token = access_token;
        }

        public override string ToString()
        {
            return string.Format("meandmineid: {0}, access_token: {1}", meandmineid, access_token);
        }
    }
}
