using UnityEngine;
using Barnabus.UI;

namespace Barnabus.Login
{
    /// <summary>
    /// Login流程UI的共用基底
    /// </summary>
    public abstract class BaseLoginCommonUI : BaseGameUI
    {
        public virtual void Init(Camera camera)
        {
            SetCanvasCamera(camera);

            Init();
        }

        public void SetCanvasCamera(Camera camera)
        {
            GetComponent<Canvas>().worldCamera = camera;
        }
    }
}
