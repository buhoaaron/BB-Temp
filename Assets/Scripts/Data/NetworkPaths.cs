using System.Collections.Generic;

namespace Barnabus
{
    public class NetworkPaths : List<NetworkPathInfo>
    {
        public string GetPath(string code)
        {
            var info = Find(element => element.code == code);

            return info.path;
        }
    }
}
