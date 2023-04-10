using UnityEngine;

namespace Barnabus.UI
{
    public class UIDebugObject : MonoBehaviour
    {
#if !DEBUG_MODE
        private void Awake()
        {
            gameObject.SetActive(false);
        }
#endif
    }
}
