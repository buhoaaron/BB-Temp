namespace Barnabus
{
    public class AvatarInfo
    {
        public readonly string skin_id;
        public readonly string color_id;
        public readonly string format_version;

        public AvatarInfo(string skin_id, string color_id, string format_version)
        {
            this.skin_id = skin_id;
            this.color_id = color_id;
            this.format_version = format_version;
        }
    }
}
