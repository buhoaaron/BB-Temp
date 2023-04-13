namespace Barnabus.Login
{
    public class SignUpInfo
    {
        public readonly string EmailAddress;
        public readonly string Password;

        public SignUpInfo(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public override string ToString()
        {
            return string.Format("SignUpInfo, EmailAddress:{0}, Password:{1}", EmailAddress, Password);
        }
    }
}
