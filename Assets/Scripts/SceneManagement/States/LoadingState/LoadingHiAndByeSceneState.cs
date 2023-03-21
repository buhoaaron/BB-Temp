using UnityEngine;
using HiAndBye;

namespace Barnabus.SceneManagement
{
    public class LoadingHiAndByeSceneState : BaseLoadingSceneState
    {
        private bool isSpineLoaded = false;

        public LoadingHiAndByeSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {

        }
        public override void Begin()
        {
            base.Begin();

            LoadHiAndByeSpine();
        }
        public override void StateUpdate()
        {
            if (isSpineLoaded)
                controller.SetState(SCENE_STATE.HI_AND_BYE);
        }

        private void LoadHiAndByeSpine()
        {
            if (!controller.GameManager.gameObject.TryGetComponent<HiAndByeSpineAssets>(out var spineAssets))
                spineAssets = controller.GameManager.gameObject.AddComponent<HiAndByeSpineAssets>();

            spineAssets.Init();
            spineAssets.OnLoadCompleted = LoadSpineAssetsCompleted;
            spineAssets.LoadSpineAssets();
        }

        private void LoadSpineAssetsCompleted()
        {
            isSpineLoaded = true;
        }
    }
}
