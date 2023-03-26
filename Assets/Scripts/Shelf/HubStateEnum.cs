namespace Barnabus.Shelf
{
    public enum HUB_STATE
    {
        /// <summary>
        /// 不能解鎖
        /// </summary>
        UNLOCK = 0,
        /// <summary>
        /// 可以解鎖
        /// </summary>
        CAN_LOCK,
        /// <summary>
        /// 睡眠
        /// </summary>
        SLEEP,
        /// <summary>
        /// 喚醒後閒置
        /// </summary>
        NORMAL_IDLE,
    }
}
