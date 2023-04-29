using Barnabus.SceneManagement;
/// <summary>
/// 場景溝通資訊
/// </summary>
public class GameSceneCacheData : BaseData
{
    public MAIN_MENU MainMenuType = MAIN_MENU.MAIN;
    /// <summary>
    /// GamePreview要跳轉的場景狀態
    /// </summary>
    public SCENE_STATE GamePreviewJumpState = SCENE_STATE.FACE;

    public GameSceneCacheData(MAIN_MENU mainMenuType)
    {
        MainMenuType = mainMenuType;
    }

    public GameSceneCacheData(SCENE_STATE jumpState)
    {
        GamePreviewJumpState = jumpState;
    }

    public void Reset()
    {
        MainMenuType = MAIN_MENU.MAIN;
        GamePreviewJumpState = SCENE_STATE.FACE;
    }
}
