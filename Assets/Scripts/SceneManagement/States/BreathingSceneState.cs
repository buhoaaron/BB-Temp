using CustomAudio;

namespace Barnabus.SceneManagement
{
    public class BreathingSceneState : BaseSceneState
    {
        public BreathingSceneState(SceneStateController controller, string sceneName) : base(controller, sceneName)
        {}

        public override void Begin()
        { 
        
        }

        public override void StateUpdate()
        {

        }

        public override void End()
        {
            AudioManager.instance.StopAllSound();
            AudioManager.instance.StopBGM();
        }
    }
}
