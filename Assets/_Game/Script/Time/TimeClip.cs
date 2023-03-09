using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine;

namespace Timeline.Time
{
    public class TimeClip : PlayableAsset, ITimelineClipAsset
    {
        private TimeBehavour template = new TimeBehavour();

        public ClipCaps clipCaps => ClipCaps.None;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<TimeBehavour>.Create(graph, template);
        }
    }
}