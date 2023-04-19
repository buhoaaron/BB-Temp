namespace Barnabus.Network
{
    public class SendSignUp : BaseSendPacket
    {
        public readonly string email;
        public readonly string password;
        public readonly int birth_year;

        public readonly AppInfo app;

        public SendSignUp(string email, string password, int birth_year)
        {
            this.email = email;
            this.password = password;
            this.birth_year = birth_year;
            this.app = new AppInfo();
        }
    }
}
