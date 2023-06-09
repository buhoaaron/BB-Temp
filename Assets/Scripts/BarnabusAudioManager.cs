﻿using UnityEngine.UI;
using System.Collections.Generic;
using AudioSystem;

/// <summary>
/// 遊戲音樂音效管理
/// </summary>
public class BarnabusAudioManager : BaseBarnabusManager
{
    public bool IsMute
    {
        get
        {
            return audioSourceManager.IsMuteBGM || audioSourceManager.IsMuteSound;
        }
    }

    private AudioSourceManager audioSourceManager;
    private Dictionary<AUDIO_NAME, ButtonClickSubject> dictButtonClickSubjects;

    public BarnabusAudioManager(NewGameManager gm, AudioSourceManager asm) : base(gm)
    {
        audioSourceManager = asm;
    }

    public override void Init()
    {
        dictButtonClickSubjects = new Dictionary<AUDIO_NAME, ButtonClickSubject>();
    }

    public override void SystemUpdate()
    {

    }

    public override void Clear()
    {

    }

    public void PlaySound(AUDIO_NAME audioName, float delay = 0)
    {
        audioSourceManager.PlaySound(audioName.ToString(), delay);
    }

    public void SetMuteAll(bool isMute)
    {
        audioSourceManager.SetMuteBGM(isMute);
        audioSourceManager.SetMuteSound(isMute);

        GameManager.PlayerDataManager.SetMuteAll(isMute);
    }

    public void AddButton(AUDIO_NAME name, Button button)
    {
        //生成按鈕事件主題
        if (!dictButtonClickSubjects.ContainsKey(name))
        {
            var subject = new ButtonClickSubject(name.ToString());
            subject.OnPlaySound = audioSourceManager.PlaySound;
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
}

