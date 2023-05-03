namespace Barnabus.Network
{
    public class ReceiveSignUp : BaseReceivePacket
    {
        public readonly int meandmine_id = 0;
        public readonly string access_token = "";

        public ReceiveSignUp(int meandmine_id, string access_token)
        {
            this.meandmine_id = meandmine_id;
            this.access_token = access_token;
        }

        public override string ToString()
        {
            return string.Format("meandmine_id: {0}, access_token: {1}", meandmine_id, access_token);
        }
    }
}
