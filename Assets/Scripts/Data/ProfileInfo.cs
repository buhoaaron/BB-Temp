namespace Barnabus
{
    /// <summary>
    /// Profile資訊(Server給的)
    /// </summary>
    public class ProfileInfo
    {
        public readonly int user_id;
        public readonly string user_firstname;
        public readonly string user_lastname;

        public ProfileInfo(int user_id, string user_firstname, string user_lastname)
        {
            this.user_id = user_id;
            this.user_firstname = user_firstname;
            this.user_lastname = user_lastname;
        }
    }
}
