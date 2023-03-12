using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioSystem;
using System.Collections;

/// <summary>
/// 音樂音效管理器
/// </summary>


public class AudioSourceManager : MonoBehaviour
{
    public static AudioSourceManager Instance => instance;
    private static AudioSourceManager instance = null;

    [SerializeField]
    private List<AudioSource> listAudioSources;
    private Dictionary<AUDIO_NAME, ButtonClickSubject> dictButtonClickSubjects;
    private AudioSource currentPlayBGM = null;

    private AudioClipResources audioClipResources = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        //自動取得場上的AudioSource
        /*var audioSourcesObj = GameObject.Find(Config.AudioSourcesDefaultName);
        if (audioSourcesObj != null)
        {
            listAudioSources = new List<AudioSource>(audioSourcesObj.GetComponentsInChildren<AudioSource>());
        }*/
        audioClipResources = GameObject.Find("AudioResources").GetComponent<AudioClipResources>();

        dictButtonClickSubjects = new Dictionary<AUDIO_NAME, ButtonClickSubject>();
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
        StartCoroutine(IPlaySound(audioName));
    }
    private IEnumerator IPlaySound(AUDIO_NAME audioName)
    {
        //var audioSource = GetAudioSource(audioName);
        var audioClip = GetAudioClip(audioName);

        var source = CreateAudioSource(audioName, audioClip);
        source.loop = false;
        source.Play();

        yield return new WaitForSeconds(audioClip.length);

        Destroy(source.gameObject);
    }

    public void AddButton(AUDIO_NAME name, Button button)
    {
        //生成按鈕事件主題
        if (!dictButtonClickSubjects.ContainsKey(name))
        {
            var subject = new ButtonClickSubject(name);
            subject.OnPlaySound = PlaySound;
            dictButtonClickSubjects.Add(name, subject);
        }

        dictButtonClickSubjects[name].AddButton(button);
    }

    public void AddButton(AUDIO_NAME name, List<Button> buttons)
    {
        foreach (var button in buttons)
            AddButton(name, button);
    }

    public void RemoveButton(AUDIO_NAME name, Button button)
    {
        if (dictButtonClickSubjects.ContainsKey(name))
            dictButtonClickSubjects[name].RemoveButton(button);
    }

    public void RemoveButton(AUDIO_NAME name, List<Button> buttons)
    {
        foreach (var button in buttons)
            RemoveButton(name, button);
    }

    private AudioSource GetAudioSource(AUDIO_NAME audioName)
    {
        int audioIndex = (int)audioName;
        return listAudioSources[audioIndex];
    }

    private AudioClip GetAudioClip(AUDIO_NAME audioName)
    {
        int audioIndex = (int)audioName;
        return audioClipResources.listSounds[audioIndex];
    }

    private AudioSource CreateAudioSource(AUDIO_NAME audioName, AudioClip clip)
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

