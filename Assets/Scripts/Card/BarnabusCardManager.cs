using System.Collections;
using UnityEngine;

namespace Barnabus.Card
{
    public class BarnabusCardManager : BaseBarnabusManager
    {
        public BarnabusList BarnabusList => barnabusCardList;

        private BarnabusList barnabusCardList = null;

        private AllBarnabusBaseData allBarnabusBaseData = null;

        public BarnabusCardManager(NewGameManager gm) : base(gm)
        {

        }

        #region BASE_API
        public override void Init()
        {

        }
        public override void SystemUpdate()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        public IEnumerator LoadBarnabusListAsync()
        {
            var request = Resources.LoadAsync<BarnabusList>("BarnabusCard/BarnabusList");
            yield return request;
            barnabusCardList = request.asset as BarnabusList;
        }

        public BarnabusScanScriptable GetCard(int id)
        {
            try
            {
                return barnabusCardList[id];
            }
            catch(System.Exception ex)
            {
                Debug.LogError("GetCard Fail: " + ex);
                return null;
            }
        }

        public BarnabusScanScriptable GetCard(string name)
        {
            var card = barnabusCardList.cards.Find(card => card.barnabusName.Contains(name));

            if (card == null)
                Debug.LogError("GetCard Fail: " + name);

            return card;
        }

        public void LoadJson()
        {
            allBarnabusBaseData = GameManager.JsonManager.DeserializeObject<AllBarnabusBaseData>(JsonText.BarnabusBaseData);
        }

        public AllBarnabusBaseData GetAllBarnabusBaseData()
        {
            return allBarnabusBaseData;
        }
    }
}
