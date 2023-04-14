using System;
using System.Collections.Generic;

namespace Barnabus.Login
{
    /// <summary>
    /// 控制頁面(LoginCommonUI)
    /// </summary>
    public class PageManager : IBaseSystem
    {
        private Dictionary<string, BaseLoginCommonUI> pages = null;

        private BaseLoginCommonUI currentPage = null;
        private BaseLoginCommonUI previousPage = null;

        #region BASE_API
        public void Init()
        {
            pages = new Dictionary<string, BaseLoginCommonUI>();
        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        {

        }
        #endregion

        public void AddPage(string pageKey, BaseLoginCommonUI page)
        {
            if (pages.ContainsKey(pageKey))
                return;

            pages.Add(pageKey, page);
        }

        public BaseLoginCommonUI GetPage(string pageKey)
        {
            if (pages.ContainsKey(pageKey))
                return pages[pageKey];

            return null;
        }

        public void SetCurrentPage(BaseLoginCommonUI page)
        {
            if (currentPage != null && currentPage != page)
            {
                previousPage = currentPage;
                //移出去
            }

            currentPage = page;
        }
    }
}
