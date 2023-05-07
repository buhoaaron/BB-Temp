using System.Collections.Generic;

namespace Barnabus.Login
{
    /// <summary>
    /// Profile相關資料管理
    /// </summary>
    public class ProfileManager : BaseLoginManager
    {
        protected internal class StateCurriculum : List<string> { }
        protected internal class Grade : List<string> { }

        protected StateCurriculum stateCurriculum = null;
        protected Grade grade = null;

        public ProfileManager(LoginSceneManager lsm) : base(lsm) 
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

        public void LoadStateCurriculum()
        {
            stateCurriculum = loginSceneManager.JsonManager.DeserializeObject<StateCurriculum>(JsonText.StateCurriculum);
        }

        public void LoadGrade()
        {
            grade = loginSceneManager.JsonManager.DeserializeObject<Grade>(JsonText.Grade);
        }

        public List<string> GetStateCurriculumList()
        {
            return stateCurriculum;
        }

        public List<string> GetGradeList()
        {
            return grade;
        }
    }
}
