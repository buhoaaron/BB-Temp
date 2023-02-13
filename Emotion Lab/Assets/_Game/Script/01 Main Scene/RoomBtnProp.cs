using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RoomBtnProp : MonoBehaviour
{
    public VideoClip clip;
    public string title;
    public string dialogue;
    public string gameName;
    public Image potion;

    public void OnClick(RoomProp prop)
    {
        prop.Init(clip, title, dialogue, gameName, potion.sprite);
        prop.gameObject.SetActive(true);
    }
}
