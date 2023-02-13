using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomAudio.Resources;

namespace CustomAudio
{
    public class Sound
    {
        public ClipSource clipSource;
        public AudioSource source;

        public bool Playing { get { return source != null && source.isPlaying; } }

        public Sound(AudioClip clip, AudioSource source, bool isMute = false, float pitch = 1, bool loop = false)
        {
            clipSource = new ClipSource() { clipName = clip.name, clip = clip};
            this.source = source;
            this.source.clip = clipSource.clip;
            this.source.mute = isMute;
            this.source.pitch = pitch;
            this.source.loop = loop;

            this.source.Play();
        }

        public Sound(ClipSource clipSource, AudioSource source, bool isMute = false, float pitch = 1, bool loop = false)
        {
            this.clipSource = clipSource;
            this.source = source;
            this.source.clip = clipSource.clip;
            this.source.mute = isMute;
            this.source.pitch = pitch;
            this.source.loop = loop;

            this.source.Play();
        }

        public void Finish()
        {
            Object.Destroy(source);
            source = null;
        }

        public void SetPitch(float value)
        {
            source.pitch = value;
        }

        public void SetMute(bool isMute)
        {
            source.mute = isMute;
        }
    }
}