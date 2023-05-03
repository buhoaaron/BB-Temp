using System;
using System.Collections.Generic;

namespace Barnabus.Login
{
    /// <summary>
    /// 控制頁面(LoginCommonUI)
    /// </summary>
    public class PageManager : IBaseSystem
    {
        private Dictionary<PAGE, BaseLoginCommonUI> pages = null;

        private BaseLoginCommonUI currentPage = null;
        private BaseLoginCommonUI previousPage = null;

        #region BASE_API
        public void Init()
        {
            pages = new Dictionary<PAGE, BaseLoginCommonUI>();
        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        {

        }
        #endregion

        public void AddPage(PAGE name, BaseLoginCommonUI page)
        {
            if (pages.ContainsKey(name))
                return;

            pages.Add(name, page);
        }

        public BaseLoginCommonUI GetPage(PAGE name)
        {
            if (pages.ContainsKey(name))
                return pages[name];

            return null;
        }

        public void SetCurrentPage(BaseLoginCommonUI page)
        {
            if (currentPage != null && currentPage != page)
            {
                previousPage = currentPage;
            }

            currentPage = page;
        }

        public void ResetPages()
        {
            foreach(var page in pages.Values)
                page.Hide();
        }
    }
}
