namespace HiAndBye
{
    public abstract class HiAndByeBaseManager : IBaseSystem
    {
        protected HiAndByeGameManager gameManager;
        public HiAndByeBaseManager(HiAndByeGameManager gm)
        {
            gameManager = gm;
        }
        public virtual void Init()
        {

        }
        public virtual void SystemUpdate()
        {

        }
        public virtual void Clear()
        {

        }
    }
}
