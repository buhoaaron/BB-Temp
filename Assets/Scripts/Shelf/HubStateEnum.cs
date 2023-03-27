namespace Barnabus.Shelf
{
    public enum HUB_STATE
    {
        /// <summary>
        /// 不能解鎖
        /// </summary>
        NOT_UNLOCK = 0,
        /// <summary>
        /// 可以解鎖
        /// </summary>
        UNLOCK,
        /// <summary>
        /// 睡眠
        /// </summary>
        SLEEP,
        /// <summary>
        /// 閒置
        /// </summary>
        IDLE,
    }
}
