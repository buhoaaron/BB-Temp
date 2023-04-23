namespace Barnabus.Login
{
    public class LoginInfo : SignUpInfo
    {
        public LoginInfo(string emailAddress, string password) : base(emailAddress, password)
        {
        }

        public override string ToString()
        {
            return string.Format("LoginInfo, EmailAddress:{0}, Password:{1}", EmailAddress, Password);
        }
    }
}
