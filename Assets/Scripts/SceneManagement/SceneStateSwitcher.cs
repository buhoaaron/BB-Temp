using UnityEngine;

namespace Barnabus.SceneManagement
{
    public class SceneStateSwitcher
    {
        private SceneStateController sceneStateController = null;

        public SceneStateSwitcher(SceneStateController controller)
        {
            sceneStateController = controller;
        }

        public BaseSceneState CaseSceneState(SCENE_STATE sceneName)
        {
            switch (sceneName)
            {
                case SCENE_STATE.START:
                    return new StartSceneState(sceneStateController, SceneLabels.NoScene);
                case SCENE_STATE.MAIN:
                    return new MainSceneState(sceneStateController, SceneLabels.Main);
                case SCENE_STATE.FACE:
                    return new FaceSceneState(sceneStateController, SceneLabels.EmotionFace);
                case SCENE_STATE.MUSIC:
                    return new MusicSceneState(sceneStateController, SceneLabels.EmotionMusic);
                case SCENE_STATE.DOT_TO_DOT:
                    return new DotToDotSceneState(sceneStateController, SceneLabels.DotToDot);
                case SCENE_STATE.HI_AND_BYE:
                    return new HiAndByeSceneState(sceneStateController, SceneLabels.GameHiAndBye);
                case SCENE_STATE.WAKE_UP:
                    return new WakeUpSceneState(sceneStateController, SceneLabels.WakeUp);
                case SCENE_STATE.WAKE_UP_WITH_UNLOCK:
                    return new WakeUpSceneUnlockState(sceneStateController, SceneLabels.WakeUp);
                case SCENE_STATE.BREATHING:
                    return new BreathingSceneState(sceneStateController, SceneLabels.Breathing);
                case SCENE_STATE.LOGIN:
                    return new LoginSceneState(sceneStateController, SceneLabels.Login);

                case SCENE_STATE.LOADING_MAIN:
                    return new LoadingMainSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_FACE:
                    return new LoadingFaceSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_MUSIC:
                    return new LoadingMusicSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_DOT_TO_DOT:
                    return new LoadingDotToDotSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_HI_AND_BYE:
                    return new LoadingHiAndByeSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_BREATHING:
                    return new LoadingBreathingSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_LOGIN:
                    return new LoadingLoginSceneState(sceneStateController, SceneLabels.Loading);
                case SCENE_STATE.LOADING_BASE:
                    return new LoadingBaseState(sceneStateController, SceneLabels.Loading);
                default:
                    Debug.LogError(string.Format("No state found for {0}", sceneName));
                    return null;
            }
        }
    }
}
