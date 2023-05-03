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
        /// <summary>
        /// 登入
        /// </summary>
        LOGIN,
        /// <summary>
        /// 選擇Profile
        /// </summary>
        CHOOSE_PROFILE,
        /// <summary>
        /// 建立Profile
        /// </summary>
        CREATE_PROFILE,
        /// <summary>
        /// 家長入職
        /// </summary>
        PARENTS_ONBOARDING,
    }
}
