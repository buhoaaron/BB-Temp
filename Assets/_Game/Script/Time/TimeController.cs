using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Time
{
    public class TimeController : MonoBehaviour
    {
        public PlayableDirector director;

        public void Pause() { director.Pause(); }
        public void Resume() {
            director.Resume();
            MessageLog();
        }
        void MessageLog(){
            MessageCenter.PostMessage<TutorialResumeMessage>(
            new TutorialResumeMessage());
        }
    }
}