namespace Barnabus.Network
{
    public class ReceiveErrorMessage : BaseReceivePacket
    {
        public readonly string error = "";

        public ReceiveErrorMessage(string error)
        {
            this.error = error;
        }

        public override string ToString()
        {
            var output = string.Format("ReceiveErrorMessage statusCode {0}, error: {1}", StatusCode, error);
            return output;
        }
    }
}
