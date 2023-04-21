namespace Barnabus.Network
{
    public class SendLogin : BaseSendPacket
    {
        public readonly string email;
        public readonly string password;

        public readonly AppInfo app;

        public SendLogin(string email, string password)
        {
            this.email = email;
            this.password = password;
            this.app = new AppInfo();
        }
    }
}
