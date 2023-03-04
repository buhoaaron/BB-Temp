using System.Collections;
using UnityEngine;

namespace Barnabus.Card
{
    public class BarnabusCardManager : IBaseSystem
    {
        public BarnabusList BarnabusList => barnabusCardList;

        private BarnabusList barnabusCardList = null;

        #region BASE_API
        public void Init()
        {

        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        {

        }
        #endregion

        public IEnumerator LoadBarnabusListAsync()
        {
            var request = Resources.LoadAsync<BarnabusList>("BarnabusCard/BarnabusList");
            yield return request;
            barnabusCardList = request.asset as BarnabusList;
        }
    }
}
