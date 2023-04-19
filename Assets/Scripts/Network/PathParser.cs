using System;
using System.Collections;

namespace Barnabus.Network
{
    public class PathParser : IBaseSystem
    {
        private NetworkManager networkManager = null;

        public PathParser(NetworkManager networkManager)
        {
            this.networkManager = networkManager;
        }

        #region BASE_API
        public void Init()
        {

        }
        public void SystemUpdate()
        {

        }
        public void Clear()
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
