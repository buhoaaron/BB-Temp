using UnityEngine;

namespace Barnabus.Login
{
    public class LoginSceneManager : MonoBehaviour, IBaseSystem
    {
        [Header("Set IdentificationUI")]
        public IdentificationUI IdentificationUI = null;

        #region BASE_API
        public void Init()
        {
            IdentificationUI.Init();
        }
        public void SystemUpdate()
        {
 
        }
        public void Clear()
        {

        }
        #endregion
    }
}
