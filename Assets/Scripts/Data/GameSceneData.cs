/// <summary>
/// 暫存用遊戲場景資訊
/// </summary>
public class GameSceneData : BaseData
{
    public MAIN_MENU MainMenuType = MAIN_MENU.MAIN;

    public GameSceneData(MAIN_MENU mainMenuType)
    {
        MainMenuType = mainMenuType;
    }
}
