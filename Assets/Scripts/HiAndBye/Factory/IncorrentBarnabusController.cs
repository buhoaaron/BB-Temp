using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace HiAndBye
{
    public class IncorrentBarnabusController : BaseGameObjectController
    {
        public UnityAction OnButtonBarnabusClick = null;

        private Button buttonBarnabus = null;
        private Text textName = null;

        public IncorrentBarnabusController(GameObject target) : base(target)
        {}

        #region BASE_API
        public override void Init()
        {
            buttonBarnabus = Transform.Find("Mode/Button_Barnabus").GetComponent<Button>();
            textName = Transform.Find("Mode/Text_Name").GetComponent<Text>();

            buttonBarnabus.onClick.AddListener(ProcessButtonBarnabusClick);
        }

        public override void Refresh()
        {

        }
        public override void Clear()
        {

        }
        #endregion

        public void SetBarnabus(Sprite sp)
        {
            buttonBarnabus.image.sprite = sp;
        }

        public void SetName(string name)
        {
            textName.text = name;
        }

        private void ProcessButtonBarnabusClick()
        {
            OnButtonBarnabusClick?.Invoke();
        }
    }
}
 

