using System;

namespace Barnabus.Network
{
    public class NetworkCallbacks 
    {
        public Action<string> OnSuccess  = null;
        public Action<ReceiveErrorMessage> OnFail  = null;
    }
}