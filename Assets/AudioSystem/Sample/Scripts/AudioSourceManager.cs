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
    /// <summary>
    /// 當前播放的BGM
    /// </summary>
    private AudioSource currentBGM = null;

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

        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// 播放背景音樂
    /// </summary>
    /// <param name="audioName">音源列舉</param>
    public void PlayBGM(AUDIO_NAME audioName)
    {
        int audioIndex = (int)audioName;
        var audioSource = listAudioSources[audioIndex];
        //若不同則停掉前一個BGM
        if (audioSource != currentBGM)
            StopBGM();
        
        //相同則繼續播
        currentBGM = audioSource;
        currentBGM.loop = true;

        currentBGM.Play();
    }
    /// <summary>
    /// 暫停BGM
    /// </summary>
    public void PauseBGM()
    {
        if (currentBGM != null)
            currentBGM.Pause();
    }
    /// <summary>
    /// 停止BGM
    /// </summary>
    public void StopBGM()
    {
        if (currentBGM != null)
            currentBGM.Stop();
    }
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName">音源列舉</param>
    public void PlaySound(AUDIO_NAME audioName)
    {
        int audioIndex = (int)audioName;
        var audioSource = listAudioSources[audioIndex];
        audioSource.loop = false;

        audioSource.Play();
    }
}

