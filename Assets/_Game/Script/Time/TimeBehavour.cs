using UnityEngine.Playables;

namespace Timeline.Time
{
    public class TimeBehavour : PlayableBehaviour
    {
        private bool isFirstFrame = true;

        private TimeController timeController;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if(isFirstFrame)
            {
                timeController = playerData as TimeController;
                timeController.Pause();
                isFirstFrame = false;
            }
        }
    }
}