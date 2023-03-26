using UnityEngine;
using UnityEngine.UI;
using Barnabus;
using Spine.Unity;

namespace Barnabus.Shelf
{
    public class HubController: MonoBehaviour, IBaseController
    {
        public int ID = 0;
        public BarnabusBaseData BarnabusData = null;
        public MainManager MainManager = null;
        public HUB_STATE State = HUB_STATE.UNLOCK;

        private Image imageHubBrandBg = null;
        private Image imageHubBrand = null;
        private Image imageChar = null;
        private Text textElement = null;
        private SkeletonGraphic skeletonGraphicEgg = null;

        private int fakePotions = 15;

        #region BASE_API
        public void Init(BarnabusBaseData data, MainManager mainManager)
        {
            MainManager = mainManager;
            BarnabusData = data;

            Init();
            InitState();
        }
        public void Init()
        {
            imageHubBrandBg = transform.Find("Image_Hub_Brand_Bg").GetComponent<Image>();
            imageHubBrand = transform.Find("Image_Hub_Brand").GetComponent<Image>();
            imageChar = transform.Find("Barnabus/Image_Char").GetComponent<Image>();
            textElement = transform.Find("Text_Element").GetComponent<Text>();
            skeletonGraphicEgg = transform.Find("Barnabus/SkeletonGraphic_Egg").GetComponent<SkeletonGraphic>();
        }

        /// <summary>
        /// 根據資料決定狀態
        /// </summary>
        private void InitState()
        {
            if (BarnabusData.AlreadyOwned)
            {
                State = HUB_STATE.SLEEP;
                return;
            }

            if (IsPotionExchange() && fakePotions >= BarnabusData.PotionExchange)
            {
                State = HUB_STATE.CAN_LOCK;
                return;
            }

            State = HUB_STATE.UNLOCK;
        }

        public void Refresh()
        {
            textElement.text = BarnabusData.Element;
            imageHubBrandBg.sprite = MainManager.GetHubBrandBg(BarnabusData.Color);
            imageHubBrand.sprite = MainManager.GetHubBrand(BarnabusData.Color);

            var barnabusSprite = MainManager.GetBarnabusSprite(BarnabusData.Name);
            imageChar.sprite = barnabusSprite;

            imageChar.enabled = CheckHatch();
            skeletonGraphicEgg.enabled = !CheckHatch();

            if (State == HUB_STATE.CAN_LOCK)
                skeletonGraphicEgg.AnimationState.SetAnimation(0, "idle_Green", true);
        }
        public void Clear()
        {

        }
        #endregion

        private bool CheckHatch()
        {
            return State != HUB_STATE.UNLOCK && State != HUB_STATE.CAN_LOCK;
        }

        private bool IsPotionExchange()
        {
            return BarnabusData.PotionExchange > 0;
        }
    }
}
