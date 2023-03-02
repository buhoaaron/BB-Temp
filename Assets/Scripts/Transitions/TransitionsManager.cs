using UnityEngine;

namespace Barnabus.Transitions
{
    public class TransitionsManager : MonoBehaviour, IBaseSystem
    {
        private SceneTransit sceneTransitPrefab = null;

        #region BASE_API
        public void Init()
        {
            sceneTransitPrefab = Resources.Load<SceneTransit>(ResourcesConfig.SceneTransitPrefabPath);

            if (sceneTransitPrefab == null)
                Debug.LogError("Can't find SceneTransitPrefabPath.");
        }
        public void SystemUpdate()
        {
            
        }
        public void Clear()
        {

        }
        #endregion

        private SceneTransit CreateSceneTransit()
        {
            var sceneTransit = Instantiate(sceneTransitPrefab);
            sceneTransit.transform.SetParent(transform);

            return sceneTransit;
        }
    }
}
