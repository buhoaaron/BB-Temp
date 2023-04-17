using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public partial class LoginSceneStateController
    {
        public BaseLoginSceneState CaseState(LOGIN_SCENE_STATE stateName)
        {
            switch (stateName)
            {
                case LOGIN_SCENE_STATE.IDENTIFICATION:
                    return new IdentificationState(this);
                case LOGIN_SCENE_STATE.SIGN_UP_ANDROID:
                    return new SignUpAndroidState(this);
                case LOGIN_SCENE_STATE.SIGN_UP_IOS:
                    return new SignUpiOSState(this);
                case LOGIN_SCENE_STATE.VERIFY_AGE:
                    return new VerifyAgeState(this);
                case LOGIN_SCENE_STATE.ACCOUNT:
                    return new AccountState(this);
                default:
                    Debug.LogError(string.Format("No state found for {0}", stateName));
                    return null;
            }
        }
    }
}
