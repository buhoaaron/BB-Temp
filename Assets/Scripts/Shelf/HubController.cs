using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using TMPro;

namespace Barnabus.Shelf
{
    public class HubController: MonoBehaviour, IBaseController
    {
        public int ID = 0;
        public PlayerBarnabusData BarnabusData = null;
        public MainManager MainManager = null;
        public HUB_STATE State => strategy.State;
        public SkeletonGraphic SkeletonGraphicEgg => skeletonGraphicEgg;
        public SkeletonGraphic SkeletonGraphicBarnabus => skeletonGraphicBarnabus;

        private BaseHubStrategy strategy = null;
        private Image imageHubBrandBg = null;
        private Button buttonHubBrand = null;
        private TMP_Text textElement = null;
        private SkeletonGraphic skeletonGraphicEgg = null;
        private SkeletonGraphic skeletonGraphicBarnabus = null;

        #region BASE_API
        public void Init(PlayerBarnabusData data, MainManager mainManager)
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
            textElement = transform.Find("TMPText_Element").GetComponent<TMP_Text>();
            skeletonGraphicBarnabus = transform.Find("Barnabus/SkeletonGraphic_Barnabus").GetComponent<SkeletonGraphic>();
            skeletonGraphicEgg = transform.Find("Barnabus/SkeletonGraphic_Egg").GetComponent<SkeletonGraphic>();
        }

        /// <summary>
        /// 根據資料決定狀態
        /// </summary>
        private void InitStrategy()
        {
            if (!IsOpen())
            {
                //TOFIX: 之後要新增未開放的策略
                strategy = new HubStrategy_NotUnlock(this);
                return;
            }

            if (BarnabusData.IsUnlocked)
            {
                strategy = BarnabusData.IsWokenUp ? new HubStrategy_Idle(this) : new HubStrategy_Sleep(this);
                return;
            }

            if (IsPotionExchange() && MainManager.GetPotionAmount() >= BarnabusData.BaseData.PotionExchange)
            {
                strategy = new HubStrategy_Unlock(this);
                return;
            }

            strategy = new HubStrategy_NotUnlock(this);
        }

        public void Refresh()
        {
            textElement.text = BarnabusData.BaseData.Element;
            imageHubBrandBg.sprite = MainManager.GetHubBrandBg(BarnabusData.BaseData.Color);
            buttonHubBrand.image.sprite = MainManager.GetHubBrand(BarnabusData.BaseData.Color);

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

        public SkeletonGraphic ChangeBarnabusSpine(int characterID, string initAnimationName = "")
        {
            var barnabusCard = NewGameManager.Instance.BarnabusCardManager.GetCard(characterID);
            SkeletonGraphicBarnabus.skeletonDataAsset = barnabusCard.skeletonData;
            SkeletonGraphicBarnabus.Initialize(true);

            if (!string.IsNullOrEmpty(initAnimationName))
                SkeletonGraphicBarnabus.AnimationState.SetAnimation(0, initAnimationName, true);

            return SkeletonGraphicBarnabus;
        }
        /// <summary>
        /// 是否是以藥水解鎖
        /// </summary>
        /// <returns></returns>
        private bool IsPotionExchange()
        {
            return BarnabusData.BaseData.PotionExchange > 0;
        }

        /// <summary>
        /// 是否已開放
        /// </summary>
        /// <returns></returns>
        private bool IsOpen()
        {
            return BarnabusData.BaseData.IsOpen;
        }
    }
}
