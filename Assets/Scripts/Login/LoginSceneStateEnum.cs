namespace Barnabus.Login.StateControl
{
    public enum LOGIN_SCENE_STATE
    {
        /// <summary>
        /// 身份辨認
        /// </summary>
        IDENTIFICATION,
        /// <summary>
        /// 註冊(Android)
        /// </summary>
        SIGN_UP_ANDROID,
        /// <summary>
        /// 註冊(iOS)
        /// </summary>
        SIGN_UP_IOS,
        /// <summary>
        /// 驗證年齡
        /// </summary>
        VERIFY_AGE,
        /// <summary>
        /// 連結小孩帳號
        /// </summary>
        ACCOUNT,
    }
}
