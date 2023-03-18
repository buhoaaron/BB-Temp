using UnityEngine;
using System.Collections;

namespace AudioSystem
{
    /// <summary>
    /// 音樂音效管理器
    /// </summary>
    public class AudioSourceManager : MonoBehaviour
    {
        public static AudioSourceManager Instance => instance;
        private static AudioSourceManager instance = null;

        private AudioSource currentPlayBGM = null;
        private AudioClipResources audioClipResources = null;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            //自動取得場上的AudioSource
            audioClipResources = GetComponent<AudioClipResources>();
            currentPlayBGM = transform.Find("AudioSourceBGM").GetComponent<AudioSource>();
        }

        public void PlayBGM(int audioClipIndex)
        {
            PlayBGM(GetAudioClip(audioClipIndex).Clip);
        }

        public void PlayBGM(string audioClipName)
        {
            PlayBGM(GetAudioClip(audioClipName).Clip);
        }
        private void PlayBGM(AudioClip audioClip)
        {
            //若和當前BGM不同則先停掉當前BGM
            if (audioClip != currentPlayBGM.clip)
                StopBGM();

            currentPlayBGM.clip = audioClip;
            currentPlayBGM.loop = true;
            currentPlayBGM.Play();
        }
        /// <summary>
        /// 暫停BGM
        /// </summary>
        public void PauseBGM()
        {
            if (currentPlayBGM != null)
                currentPlayBGM.Pause();
        }
        /// <summary>
        /// 停止BGM
        /// </summary>
        public void StopBGM()
        {
            if (currentPlayBGM != null)
                currentPlayBGM.Stop();
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioName">音源列舉</param>
        public void PlaySound(string audioClipName)
        {
            var audioClipPair = GetAudioClip(audioClipName);
            StartCoroutine(IPlaySound(audioClipPair));
        }
        public void PlaySound(int audioClipIndex)
        {
            var audioClipPair = GetAudioClip(audioClipIndex);
            StartCoroutine(IPlaySound(audioClipPair));
        }
        private IEnumerator IPlaySound(AudioClipPair audioClipPair)
        {
            var source = CreateAudioSource(audioClipPair.Name, audioClipPair.Clip);
            source.loop = false;
            source.Play();

            yield return new WaitForSeconds(audioClipPair.Clip.length);

            Destroy(source.gameObject);
        }

        private AudioClipPair GetAudioClip(int audioClipIndex)
        {
            return audioClipResources.GetAudioClip(audioClipIndex);
        }

        private AudioClipPair GetAudioClip(string audioClipName)
        {
            return audioClipResources.GetAudioClip(audioClipName);
        }

        private AudioSource CreateAudioSource(string audioName, AudioClip clip)
        {
            var audioSourceObj = new GameObject(audioName.ToString());
            audioSourceObj.transform.SetParent(transform);
            //加入AudioSource組件
            var audioSource = audioSourceObj.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.playOnAwake = false;

            return audioSource;
        }
    }
}

