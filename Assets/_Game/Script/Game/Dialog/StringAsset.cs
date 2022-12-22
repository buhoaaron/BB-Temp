using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/StringAsset")]
public class StringAsset : ScriptableObject
{
    [Header("Emotion Face")]
    public string emotionFaceStartDialog;
    public string emotionFaceEndDialog;

    [Header("Emotion Music")]
    public string emotionMusicStartDialog;
    public string emotionMusicEndDialog;

    [Header("Dot to Dot")]
    public string dotToDotStartDialog;
    public string dotToDotEndDialog;

    [Header("Breathing")]
    public string angerbreathingStartDialog;
    public string angerbreathingEndDialog;

    public string propbreathingStartDialog;
    public string propbreathingEndDialog;
}