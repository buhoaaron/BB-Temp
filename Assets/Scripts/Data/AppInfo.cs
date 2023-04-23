using UnityEngine;

namespace Barnabus
{
    public class AppInfo
    {
        public readonly string id;
        public readonly string version;

        public AppInfo(string id, string version)
        {
            this.id = id;
            this.version = version;
        }

        public AppInfo()
        {
            this.id = Application.identifier; 
            this.version = Application.version; 
        }
    }
}
