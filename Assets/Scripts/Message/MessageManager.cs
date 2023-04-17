using System;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.Login
{
    public class MessageManager : BaseLoginManager
    {
        private GameObject messagePrefab = null;
        private AllErrorMessageInfo errorMessageInfos = null;

        public MessageManager(LoginSceneManager lsm) : base(lsm)
        {

        }

        public void Init(GameObject messagePrefab)
        {
            this.messagePrefab = messagePrefab;

            Init();
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

        public void SetAllErrorMessageInfo(AllErrorMessageInfo infos)
        {
            this.errorMessageInfos = infos;
        }

        public MessageUI DoShowErrorMessage(int errorCode)
        {
            var ui = GameObject.Instantiate(messagePrefab).GetComponent<MessageUI>();
            ui.SetCanvasCamera(loginSceneManager.SceneCamera);

            ui.Init();
            ui.DoPopUp();

            var info = errorMessageInfos.Find(info => info.ErrorCode == errorCode);
            ui.SetMessage(info.Title, info.Message);
            
            return ui;
        }
    }
}
