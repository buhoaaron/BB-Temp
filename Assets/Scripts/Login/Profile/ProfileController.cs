using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
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

        private CanvasGroup group = null;

        #region BASE_API
        public void Init()
        {
            group = GetComponent<CanvasGroup>();

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

            //family's profile only show firstname
            TextName.text = Info.family_nick_name;

            ButtonPlayer.image.sprite = Info.SpriteIcon;
        }

        private void LayoutAdd()
        {
            TextName.text = LoginText.AddChild;
        }

        private void ProcessButtonClick()
        {
            OnButtonClick?.Invoke();
        }

        public void DoShow(float delay)
        {
            group.alpha = 0;

            var originPos = ButtonPlayer.transform.localPosition;
            ButtonPlayer.transform.localPosition = new Vector2(originPos.x, originPos.y + 20);

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(delay);
            seq.Append(ButtonPlayer.transform.DOLocalMoveY(originPos.y, 0.5f));
            seq.Join(group.DOFade(1, 0.5f));
        }
    }
}
