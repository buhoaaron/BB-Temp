using System;
using UnityEngine;

namespace Barnabus.Login
{
    /// <summary>
    /// 驗證年齡
    /// </summary>
    public class VerifyAgeManager : BaseLoginManager
    {
        public readonly int AdultAge = 18;

        private int userBirthYear = 0;

        public VerifyAgeManager(LoginSceneManager lsm) : base(lsm)
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
            userBirthYear = 0;
        }
        #endregion

        public bool CheckAdultAge(int birthyear)
        {
            userBirthYear = birthyear;
            //取得年份(本機)
            var result = DateTimeOffset.Now.Year - userBirthYear >= AdultAge;

            return result;
        }
    }
}
