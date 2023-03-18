using System.Collections.Generic;
using UnityEngine;

namespace AudioSystem
{
    [System.Serializable]
    public struct AudioClipPair
    {
        public string Name;
        public AudioClip Clip;
    }
    public class AudioClipResources : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClipPair> listAudioClips;

        public void AddAudioClip(AudioClipPair audioClipPair)
        {
            listAudioClips.Add(audioClipPair);
        }

        public AudioClipPair GetAudioClip(int index)
        {
            try
            {
                return listAudioClips[index];
            }
            catch
            {
                throw;
            }
        }

        public AudioClipPair GetAudioClip(string name)
        {
            return listAudioClips.Find(x => x.Name == name);
        }

        public void Clear()
        {
            listAudioClips = new List<AudioClipPair>();
        }
    }
}
