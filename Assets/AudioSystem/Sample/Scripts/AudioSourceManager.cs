using System.Collections.Generic;
using UnityEngine;

using AudioSystem;

/// <summary>
/// 音樂音效管理器
/// </summary>


public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager Instance => instance;
    private static AudioSourceManager instance = null;

    [SerializeField]
    private List<AudioSource> listAudioSources;

    private AudioSource currentPlayBGM = null;

    private void Awake()
    {
        if (instance == null)
            instance = this; 
        //自動取得場上的AudioSource
        var audioSourcesObj = GameObject.Find(Config.AudioSourcesDefaultName);
        if (audioSourcesObj != null)
        {
            listAudioSources = new List<AudioSource>(audioSourcesObj.GetComponentsInChildren<AudioSource>());
        }
    }

    /// <summary>
    /// 播放背景音樂
    /// </summary>
    /// <param name="audioName">音源列舉</param>
    public void PlayBGM(AUDIO_NAME audioName)
    {
        var audioSource = GetAudioSource(audioName);
        //若和當前BGM不同則先停掉當前BGM
        if (audioSource != currentPlayBGM)
            StopBGM();

        currentPlayBGM = audioSource;
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
    public void PlaySound(AUDIO_NAME audioName)
    {
        var audioSource = GetAudioSource(audioName);
        audioSource.loop = false;

        audioSource.Play();
    }

    private AudioSource GetAudioSource(AUDIO_NAME audioName)
    {
        int audioIndex = (int)audioName;
        return listAudioSources[audioIndex];
    }
}

