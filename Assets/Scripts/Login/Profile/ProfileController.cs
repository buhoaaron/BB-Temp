using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Barnabus.UI;
using TMPro;

namespace Barnabus.Login
{
    public class ProfileController : MonoBehaviour, IBaseController
    {
        public UnityAction OnButtonClick = null;
        public PROFILE_STATE State { private set; get; } = PROFILE_STATE.NORMAL;
        public ProfileInfo Info { private set; get; } = null;

        public Button ButtonPlayer = null;
        public TMP_Text TextName = null;

        private UIButtonChangeSprite changeButtonSprite = null;

        #region BASE_API
        public void Init()
        {
            changeButtonSprite = ButtonPlayer.GetComponent<UIButtonChangeSprite>();

            ButtonPlayer.onClick.AddListener(ProcessButtonClick);
        }
        public void Refresh()
        {
            switch (State)
            {
                case PROFILE_STATE.NORMAL:
                    LayoutNormal();
                    break;
                case PROFILE_STATE.ADD:
                    LayoutAdd();
                    break;
            }
        }
        public void Clear()
        {

        }
        #endregion

        public void SetState(PROFILE_STATE state, ProfileInfo info = null)
        {
            State = state;
            Info = info;

            Refresh();
        }

        private void LayoutNormal()
        {
            Debug.Assert(Info != null, "In normal, ProfileInfo can't be null.");

            changeButtonSprite.Change(0);
            //family's profile only show firstname
            TextName.text = Info.user_firstname;
        }

        private void LayoutAdd()
        {
            changeButtonSprite.Change(1);

            TextName.text = LoginText.AddChild;
        }

        private void ProcessButtonClick()
        {
            OnButtonClick?.Invoke();
        }
    }
}
