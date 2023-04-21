using System.Net.Mail;

namespace Barnabus
{
    public static class EmailChecker
    {
        /// <summary>
        /// 檢查是否符合email格式
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool CheckValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
