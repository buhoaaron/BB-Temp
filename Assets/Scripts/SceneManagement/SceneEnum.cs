namespace Barnabus.SceneManagement
{
    public enum SCENE_STATE
    {
        //初始場景
        START = 0,
        //功能場景
        MAIN = 1,
        //遊戲場景
        FACE = 5,
        MUSIC = 6,
        DOT_TO_DOT = 7,
        HI_AND_BYE = 8,
        //讀取場景
        LOADING_MAIN = 101,
        LOADING_FACE = 105,
        LOADING_MUSIC = 106,
        LOADING_DOT_TO_DOT = 107,
        LOADING_HI_AND_BYE = 108,
    }
}
