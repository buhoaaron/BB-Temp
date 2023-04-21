namespace Barnabus.Login
{
    public class ErrorMessageInfo
    {
        public readonly int ErrorCode;
        public readonly string Title;
        public readonly string Message;

        public ErrorMessageInfo(int errorCode, string title, string message)
        {
            ErrorCode = errorCode;
            Title = title;
            Message = message;
        }
    }
}