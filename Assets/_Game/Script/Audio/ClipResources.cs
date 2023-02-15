using System.Collections.Generic;
using UnityEngine;

namespace CustomAudio.Resources
{
    [CreateAssetMenu(menuName = "Audio/ClipResources")]
    public class ClipResources : ScriptableObject
    {
        public AudioClip ButtonSFX;
        public AudioClip CameraSFX;
        public AudioClip SaveSFX;
        public AudioClip DeleteSFX;


       /* public List<ClipSource> clipResources;

        public ClipSource GetClipSource(int index)
        {
            return clipResources[index];
        }

        public ClipSource GetClipSource(string clipName)
        {
            for (int i = 0; i < clipResources.Count; i++)
                if (clipResources[i].clipName == clipName)
                    return clipResources[i];

            return null;
        }*/
    }
}