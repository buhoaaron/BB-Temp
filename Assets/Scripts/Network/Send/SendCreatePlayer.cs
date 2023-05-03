namespace Barnabus.Network
{
    public class SendCreatePlayer : BaseSendPacket
    {
        public readonly int user_id;
        public readonly bool is_parent_owned;
        public readonly string first_name;
        public readonly string last_name;
        public readonly string grade;
        public readonly string country_code;
        public readonly string state;
        public readonly AvatarInfo avatar;
        public SendCreatePlayer(int user_id, bool is_parent_owned, string first_name, string last_name, string grade, string country_code, string state, AvatarInfo avatar)
        {
            this.user_id = user_id;
            this.is_parent_owned = is_parent_owned;
            this.first_name = first_name;
            this.last_name = last_name;
            this.grade = grade;
            this.country_code = country_code;
            this.state = state;
            this.avatar = avatar;
        }
    }
}
