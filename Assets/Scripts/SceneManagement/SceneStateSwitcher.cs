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
                    return new StartSceneState(sceneStateController, "");
                case SCENE_STATE.MAIN:
                    return new MainSceneState(sceneStateController, "MainScene");
                case SCENE_STATE.FACE:
                    return new FaceSceneState(sceneStateController, "EmotionFace");
                case SCENE_STATE.MUSIC:
                    return new MusicSceneState(sceneStateController, "EmotionMusic");
                case SCENE_STATE.HI_AND_BYE:
                    return new HiAndByeSceneState(sceneStateController, "GameHiAndByeScene");

                case SCENE_STATE.LOADING_MAIN:
                    return new LoadingMainSceneState(sceneStateController, "LoadingScene");
                case SCENE_STATE.LOADING_FACE:
                    return new LoadingFaceSceneState(sceneStateController, "LoadingScene");
                case SCENE_STATE.LOADING_MUSIC:
                    return new LoadingMusicSceneState(sceneStateController, "LoadingScene");
                case SCENE_STATE.LOADING_HI_AND_BYE:
                    return new LoadingHiAndByeSceneState(sceneStateController, "LoadingScene");
                default:
                    Debug.LogError(string.Format("No state found for {0}", sceneName));
                    return null;
            }
        }
    }
}
