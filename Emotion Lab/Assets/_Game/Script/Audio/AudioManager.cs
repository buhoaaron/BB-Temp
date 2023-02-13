using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CustomAudio.Resources;

namespace CustomAudio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioSource bgmSource;
        public ClipResources soundResources;

        private List<Sound> soundList = new List<Sound>();
        private bool isBgmMute, isSoundMute;

        public float maxVolume;

        void Awake()
        {
            if (instance) Destroy(gameObject);
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                StartCoroutine(FinishDetect());
            }
        }

        public void LoadMuteSetting()
        {
            //Load
            isBgmMute = Barnabus.DataManager.IsMuteBGM == 1;
            isSoundMute = Barnabus.DataManager.IsMuteSFX == 1;
            UpdateAudioMute();
        }

        public void UpdateAudioMute()
        {
            bgmSource.mute = isBgmMute;
            for (int i = 0; i < soundList.Count; i++)
                soundList[i].SetMute(isSoundMute);
        }

        #region Sound
        public void SetSoundResources(ClipResources resources)
        {
            soundResources = resources;
        }

        public void PlaySound(AudioClip clip, float pitch = 1, bool loop = false)
        {
            if (clip != null)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                soundList.Add(new Sound(clip, source, isSoundMute, pitch, loop));
            }
        }

        public void PlaySound(ClipSource clip, float pitch = 1, bool loop = false)
        {
            if (clip != null)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                soundList.Add(new Sound(clip, source, isSoundMute, pitch, loop));
            }
        }

        public void PlaySound(string clipName, float pitch = 1, bool loop = false)
        {
            ClipSource clip = soundResources.GetClipSource(clipName);
            if (clip != null)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();

                soundList.Add(new Sound(clip, source, isSoundMute, pitch, loop));
            }
        }

        public AudioSource FindAudioSource(AudioClip clip)
        {
            return soundList.Find(x => x.source.clip == clip).source;
        }

        public void StopSound(string clipName)
        {
            int index = FindSoundIndex(clipName);
            if (index != -1) RemoveSound(index);
        }

        public void StopSound(AudioClip clip)
        {
            int index = FindSoundIndex(clip);
            if (index != -1) RemoveSound(index);
        }

        public void StopAllSound()
        {
            for (int i = 0; i < soundList.Count; i++)
                soundList[i].Finish();
            soundList.Clear();
        }

        public bool IsSoundExist(AudioClip clip) { return FindSoundIndex(clip) != -1; }

        public void SetSoundPitch(string clipName, float pitch)
        {
            int index = FindSoundIndex(clipName);
            if (index != -1) soundList[index].SetPitch(pitch);
        }

        private int FindSoundIndex(string clipName)
        {
            FinishSoundDetect();
            for (int i = 0; i < soundList.Count; i++)
                if (soundList[i].clipSource.clipName == clipName)
                    return i;
            return -1;
        }

        private int FindSoundIndex(AudioClip clip)
        {
            if (clip == null) return -1;

            FinishSoundDetect();
            for (int i = 0; i < soundList.Count; i++)
                if (soundList[i].clipSource.clip == clip)
                    return i;
            return -1;
        }

        private void RemoveSound(int index)
        {
            soundList[index].Finish();
            soundList.RemoveAt(index);
        }

        private void FinishSoundDetect()
        {
            for (int i = 0; i < soundList.Count; i++)
                if (!soundList[i].Playing)
                    RemoveSound(i);
        }

        IEnumerator FinishDetect()
        {
            WaitForSeconds seconds = new WaitForSeconds(1);
            while (true)
            {
                yield return seconds;
                FinishSoundDetect();
            }
        }
        #endregion

        #region BGM
        public void PlayBGM(AudioClip clip, bool loop = true, float beginTime = 0)
        {
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.time = beginTime;
            bgmSource.Play();
            bgmSource.mute = isBgmMute;
            bgmSource.volume = maxVolume;
        }

        public void PauseBGM()
        {
            if (bgmSource.isPlaying)
                bgmSource.Pause();
        }

        public void ResumeBGM()
        {
            if (!bgmSource.isPlaying)
            {
                bgmSource.UnPause();
                BgmFadeIn();
            }
        }

        public void StopBGM()
        {
            bgmSource.Stop();
        }

        private void BgmFadeIn(float duration = 1)
        {
            StartCoroutine(WaitForBgmFadeIn(duration));
        }

        public void BgmFadeOut(float duration = 1)
        {
            StartCoroutine(WaitForBgmFadeOut(duration));
        }

        public float GetBgmCurrentTime() { return bgmSource.time; }
        public float GetBgmProgress() { return bgmSource.time / bgmSource.clip.length; }

        IEnumerator WaitForBgmFadeIn(float duration)
        {
            bgmSource.volume = 0;
            while (bgmSource.volume < maxVolume)
            {
                bgmSource.volume += 0.05f;
                yield return new WaitForSeconds(duration / 20f);
            }
            bgmSource.volume = maxVolume;
        }

        IEnumerator WaitForBgmFadeOut(float duration)
        {
            bgmSource.volume = maxVolume;
            while (bgmSource.volume > 0)
            {
                bgmSource.volume -= 0.05f;
                yield return new WaitForSeconds(duration / 20f);
            }
            bgmSource.volume = 0;
        }
        #endregion
    }
}