using System;
using System.Collections;

namespace Barnabus.Network
{
    public class PathParser : BaseNetworkManager
    {
        public PathParser(NetworkManager networkManager) : base(networkManager)
        {

        }

        #region BASE_API
        public override void Init()
        {

        }
        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        public string CaseUrl(API_PATH targetPath)
        {
            var domain = networkManager.NetworkConfig.domainDev;
            var path = networkManager.NetworkPaths.GetPath(targetPath.ToString());

            var url = domain + path;

            return url;
        }
    }
}
