using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Timeline.Time
{
    [TrackColor(1, 1, 0.5f)]
    [TrackClipType(typeof(TimeClip))]
    [TrackBindingType(typeof(TimeController))]
    [DisplayName("Custom/Time Track")]
    public class TimeTrack : TrackAsset
    {
        /*
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<TextMixerBehaviour>.Create(graph, inputCount);
        }
        */
    }
}
