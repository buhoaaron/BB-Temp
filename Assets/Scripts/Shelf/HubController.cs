using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;

namespace Barnabus.Shelf
{
    public class HubController: MonoBehaviour, IBaseController
    {
        public int ID = 0;
        public BarnabusBaseData BarnabusData = null;
        public MainManager MainManager = null;
        public HUB_STATE State => strategy.State;
        public Image ImageChar => imageChar;
        public SkeletonGraphic SkeletonGraphicEgg => skeletonGraphicEgg;

        private BaseHubStrategy strategy = null;
        private Image imageHubBrandBg = null;
        private Button buttonHubBrand = null;
        private Image imageChar = null;
        private TMP_Text textElement = null;
        private SkeletonGraphic skeletonGraphicEgg = null;

        #region BASE_API
        public void Init(BarnabusBaseData data, MainManager mainManager)
        {
            MainManager = mainManager;
            BarnabusData = data;

            Init();
            InitStrategy();

            buttonHubBrand.onClick.AddListener(ProcessHubClick);
        }
        public void Init()
        {
            imageHubBrandBg = transform.Find("Image_Hub_Brand_Bg").GetComponent<Image>();
            buttonHubBrand = transform.Find("Button_Hub_Brand").GetComponent<Button>();
            imageChar = transform.Find("Barnabus/Image_Char").GetComponent<Image>();
            textElement = transform.Find("TMPText_Element").GetComponent<TMP_Text>();
            skeletonGraphicEgg = transform.Find("Barnabus/SkeletonGraphic_Egg").GetComponent<SkeletonGraphic>();
        }

        /// <summary>
        /// 根據資料決定狀態
        /// </summary>
        private void InitStrategy()
        {
            if (BarnabusData.AlreadyOwned)
            {
                strategy = new HubStrategy_Sleep(this);
                return;
            }

            if (IsPotionExchange() && MainManager.GetPotionAmount() >= BarnabusData.PotionExchange)
            {
                strategy = new HubStrategy_Unlock(this);
                return;
            }

            strategy = new HubStrategy_NotUnlock(this);
        }

        public void Refresh()
        {
            textElement.text = BarnabusData.Element;
            imageHubBrandBg.sprite = MainManager.GetHubBrandBg(BarnabusData.Color);
            buttonHubBrand.image.sprite = MainManager.GetHubBrand(BarnabusData.Color);

            var barnabusSprite = MainManager.GetBarnabusSprite(BarnabusData.Name);
            imageChar.sprite = barnabusSprite;

            strategy.Refresh();
        }
        public void Clear()
        {
            buttonHubBrand.onClick.RemoveListener(ProcessHubClick);
        }
        #endregion

        private void ProcessHubClick()
        {
            strategy.ProcessHubClick();
        }

        private bool IsPotionExchange()
        {
            return BarnabusData.PotionExchange > 0;
        }
    }
}
