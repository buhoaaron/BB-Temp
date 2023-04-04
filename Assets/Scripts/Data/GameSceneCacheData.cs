/// <summary>
/// 場景溝通資訊
/// </summary>
public class GameSceneCacheData : BaseData
{
    public MAIN_MENU MainMenuType = MAIN_MENU.MAIN;

    public GameSceneCacheData(MAIN_MENU mainMenuType)
    {
        MainMenuType = mainMenuType;
    }

    public void Reset()
    {
        MainMenuType = MAIN_MENU.MAIN;
    }
}
