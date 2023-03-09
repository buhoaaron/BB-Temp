using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Barnabus.UI
{
    public class SceneLoadingUI : BaseGameUI
    {
        private Image imageProgressBar = null;

        public override void Init()
        {
            imageProgressBar = transform.Find("Image_ProgressBarBg/Image_ProgressBar").GetComponent<Image>();
            imageProgressBar.fillAmount = 0;
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void SetPrograss(float prograss)
        {
            imageProgressBar.fillAmount = prograss;
        }
    }
}
