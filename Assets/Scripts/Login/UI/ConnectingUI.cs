using UnityEngine;
using TMPro;
using Barnabus.Login;

namespace Barnabus.Network
{
    public class ConnectingUI : BaseLoginCommonUI
    {
        [Header("Set UI Components")]
        public TMP_Text TextMessage = null;

        public override void Init()
        {

        }
        public override void UpdateUI()
        {

        }
        public override void Clear()
        {
    
        }

        public void SetMessage(string message)
        {
            TextMessage.text = message;
        }
    }
}
