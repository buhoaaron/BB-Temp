using System;

namespace Barnabus.Network
{
    public class NetworkCallbacks 
    {
        public Action<ReceiveErrorMessage> OnFail  = null;
    }
}