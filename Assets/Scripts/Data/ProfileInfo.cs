using UnityEngine;

namespace Barnabus
{
    /// <summary>
    /// Profile資訊(Server給的)
    /// </summary>
    public class ProfileInfo
    {
        public readonly int player_id;
        public readonly string first_name;
        public readonly string last_name;
        public readonly string family_nick_name;
        public readonly string color_id;
        public readonly string skin_id;
        public readonly string format_version;

        public ProfileInfo(int player_id, string first_name, string last_name, 
            string family_nick_name, string color_id, string skin_id, string format_version)
        {
            this.player_id = player_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.family_nick_name = family_nick_name;
            this.color_id = color_id;
            this.skin_id = skin_id;
            this.format_version = format_version;
        }
    }
}
