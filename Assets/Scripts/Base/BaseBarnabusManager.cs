public abstract class BaseBarnabusManager : IBaseSystem
{
    public NewGameManager GameManager = null;
    public BaseBarnabusManager(NewGameManager gm)
    {
        GameManager = gm;
    }

    #region BASE_API
    public abstract void Init();
    public abstract void SystemUpdate();
    public abstract void Clear();
    #endregion

    public virtual void Save()
    {

    }

    public virtual void Load()
    {

    }
}

