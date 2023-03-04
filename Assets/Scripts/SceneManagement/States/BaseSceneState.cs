using StateMachine;
using UnityEngine.SceneManagement;

namespace Barnabus.SceneManagement
{
    public abstract class BaseSceneState : BaseState
    {
        public string SceneName = string.Empty;

        new protected SceneStateController controller = null;

        public BaseSceneState(SceneStateController controller, string sceneName) : base(controller)
        {
            this.controller = controller;

            SceneName = sceneName;
        }
        public virtual void LoadingSceneUpdate(float prograss)
        { }
    }
}
