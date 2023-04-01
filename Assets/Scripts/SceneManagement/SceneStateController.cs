using StateMachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Barnabus.SceneTransitions;

namespace Barnabus.SceneManagement
{
    /// <summary>
    /// 場景狀態機
    /// </summary>
    public class SceneStateController : StateController, IBaseSystem
    {
        public NewGameManager GameManager = null;

        private bool isLoadingScene = false;
        private AsyncOperation loadingSceneAsync = null;
        private UnityAction<Scene, LoadSceneMode> onSceneLoaded = null;

        private SceneStateSwitcher stateSwitcher = null;
        private SceneTransitionsManager transitionsManager = null;

        public SceneStateController(NewGameManager gm, SceneTransitionsManager manager)
        {
            GameManager = gm;
            transitionsManager = manager;
        }

        #region BASE_API
        public void Init()
        {
            stateSwitcher = new SceneStateSwitcher(this);
        }
        public void SystemUpdate()
        {

        }
        public void Clear()
        {
            SceneManager.sceneLoaded -= onSceneLoaded;
            loadingSceneAsync = null;
            isLoadingScene = false;
        }
        #endregion

        public void SetState(SCENE_STATE sceneName)
        {
            if (isLoadingScene)
                return;

            BaseSceneState state = stateSwitcher.CaseSceneState(sceneName);

            //判斷是否不需要切場景
            if (IsNotChangeScene(state.SceneName))
            {
                base.SetState(state);
            }
            else
            {
                isLoadingScene = true;
                //淡入效果完成後才讀取場景
                transitionsManager.OnFadeInComplete = () =>
                {
                    //場景讀取完後才切換狀態及淡出
                    onSceneLoaded = (scene, mode) =>
                    {
                        base.SetState(state);

                        transitionsManager.FadeOut();

                        Clear();
                    };

                    LoadSceneAsync(state.SceneName, onSceneLoaded);
                };

                transitionsManager.FadeIn();
            }   
        }
        private void LoadSceneAsync(string sceneName, UnityAction<Scene, LoadSceneMode> onLoaded = null)
        {
            SceneManager.sceneLoaded += onLoaded;
            loadingSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        }

        public override void StateUpdate()
        {
            //觸發LoadingSceneUpdate
            if (loadingSceneAsync?.isDone == false)
            {
                var sceneState = currentState as BaseSceneState;
                sceneState.LoadingSceneUpdate(loadingSceneAsync.progress);
                return;
            }

            base.StateUpdate();
        }

        /// <summary>
        /// 是否符合不轉場的情況
        /// </summary>
        private bool IsNotChangeScene(string targetSceneName)
        {
            var isNoScene = targetSceneName == SceneLabels.NoScene;

            return isNoScene;
        }
    }
}
