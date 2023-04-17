
namespace Barnabus.Login
{
    public abstract class BaseLoginManager : IBaseSystem
    {
        protected LoginSceneManager loginSceneManager = null;

        public BaseLoginManager(LoginSceneManager lsm)
        {
            loginSceneManager = lsm;
        }

        #region BASE_API
        public abstract void Init();
        public abstract void SystemUpdate();
        public abstract void Clear();
        #endregion
    }
}
