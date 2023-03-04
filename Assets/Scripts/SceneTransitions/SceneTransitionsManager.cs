using UnityEngine;
using Barnabus.UI;
using UnityEngine.Events;

namespace Barnabus.SceneTransitions
{
    public class SceneTransitionsManager : MonoBehaviour, IBaseSystem
    {
        public UnityAction OnFadeInComplete = null;
        public UnityAction OnFadeOutComplete = null;

        [SerializeField]
        private SceneTransitionsUI sceneTransitUIPrefab = null;

        private SceneTransitionsUI sceneTransitionsUI = null;

        #region BASE_API
        public void Init()
        {
            sceneTransitionsUI = CreateSceneTransitUI();
            sceneTransitionsUI.Init();
        }
        public void SystemUpdate()
        {
            
        }
        public void Clear()
        {

        }
        #endregion

        private SceneTransitionsUI CreateSceneTransitUI()
        {
            var sceneTransit = Instantiate(sceneTransitUIPrefab);
            sceneTransit.transform.SetParent(transform);

            return sceneTransit;
        }

        public void FadeIn()
        {
            sceneTransitionsUI.DoFadeIn(ProcessFadeInComplete);
        }

        private void ProcessFadeInComplete()
        {
            OnFadeInComplete?.Invoke();
        }
        public void FadeOut()
        {
            sceneTransitionsUI.DoFadeOut(ProcessFadeOutComplete);
        }

        private void ProcessFadeOutComplete()
        {
            OnFadeOutComplete?.Invoke();
        }
    }
}
