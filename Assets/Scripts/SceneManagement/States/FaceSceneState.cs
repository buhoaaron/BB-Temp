using CustomAudio;

namespace Barnabus.SceneManagement
{
    public class FaceSceneState : BaseSceneState
    {
        public FaceSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        { 
        
        }
        public override void StateUpdate()
        {

        }
        public override void End()
        {
            try
            {
                AudioManager.instance.StopAllSound();
                AudioManager.instance.StopBGM();
            }
            catch
            {

            }
        }
    }
}
