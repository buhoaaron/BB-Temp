using UnityEngine;

namespace CustomAudio
{
    public enum AudioType { BGM, Sound }

    public class AudioPlayer : MonoBehaviour
    {
        public AudioType type;
        public AudioClip clip;
        public bool playOnStart;

        private void Start()
        {
            if (playOnStart)
                Play();
        }

        public void Play()
        {
            if (clip == null) return;

            switch (type)
            {
                case AudioType.BGM:
                    AudioManager.instance.PlayBGM(clip);
                    break;
                case AudioType.Sound:
                    AudioManager.instance.PlaySound(clip);
                    break;
            }
        }

        public void PauseBGM() { AudioManager.instance.PauseBGM(); }
        public void ResumeBGM() { AudioManager.instance.ResumeBGM(); }

        public void StopAllSound() { AudioManager.instance.StopAllSound(); }
    }
}