using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using Barnabus.UI;
using TMPro;

namespace Barnabus.Login
{
    public class MessageUI : BaseLoginCommonUI
    {
        public UnityAction OnButtonConfirmClick = null;

        [Header("Set UI Components")]
        public Button ButtonConfirm = null;
        public TMP_Text TextTitle = null;
        public TMP_Text TextMessage = null;

        public override void Init()
        {
            ButtonConfirm.onClick.AddListener(ProcessButtonConfirmClick);
        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {

        }

        public void SetMessage(string title, string message)
        {
            TextTitle.text = title;
            TextMessage.text = message;
        }
        
        private void ProcessButtonConfirmClick()
        {
            OnButtonConfirmClick?.Invoke();

            Destroy();
        }
    }
}
