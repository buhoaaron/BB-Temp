using UnityEngine;

namespace Barnabus.Login.StateControl
{
    public partial class LoginSceneStateController
    {
        public BaseLoginSceneState CaseState(LOGIN_SCENE_STATE stateName)
        {
            switch (stateName)
            {
                case LOGIN_SCENE_STATE.IDENTIFICATION: return new IdentificationState(this);
                case LOGIN_SCENE_STATE.SIGN_UP_ANDROID: return new SignUpAndroidState(this);
                case LOGIN_SCENE_STATE.SIGN_UP_IOS: return new SignUpiOSState(this);
                case LOGIN_SCENE_STATE.VERIFY_AGE: return new VerifyAgeState(this);
                case LOGIN_SCENE_STATE.ACCOUNT: return new AccountState(this);
                case LOGIN_SCENE_STATE.LOGIN: return new LoginWithEmailState(this);
                case LOGIN_SCENE_STATE.CHOOSE_PROFILE: return new ChooseProfileState(this);
                case LOGIN_SCENE_STATE.CREATE_PROFILE: return new NewAccountSetUpState(this);
                case LOGIN_SCENE_STATE.PARENTS_ONBOARDING: return new ParentsOnboardingState(this);
                default:
                    Debug.LogError(string.Format("No state found for {0}", stateName));
                    return null;
            }
        }
    }
}
