namespace Barnabus
{
    public class NetworkConfig
    {
        public readonly string domainDev = string.Empty;
        public readonly string domainBeta = string.Empty;

        public NetworkConfig(string domainDev, string domainBeta)
        {
            this.domainDev = domainDev;
            this.domainBeta = domainBeta;
        }
    }
}
