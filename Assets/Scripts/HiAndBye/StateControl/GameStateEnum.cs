namespace HiAndBye.StateControl
{
    public enum GAME_STATE
    {
        /// <summary>
        /// 遊戲初始化
        /// </summary>
        GAME_INIT = 0,
        /// <summary>
        /// 遊戲開始
        /// </summary>
        GAME_START,
        /// <summary>
        /// 設定題目
        /// </summary>
        SET_QUESTION,
        /// <summary>
        /// 答題
        /// </summary>
        ANSWER_QUESTION,
        /// <summary>
        /// 檢查答案
        /// </summary>
        CHECK_ANSWER,
        /// <summary>
        /// 結算
        /// </summary>
        RESULT,
        /// <summary>
        /// 暫停
        /// </summary>
        PAUSE,
        /// <summary>
        /// 獲得藥水
        /// </summary>
        POTION_REWARD,
    }
}
