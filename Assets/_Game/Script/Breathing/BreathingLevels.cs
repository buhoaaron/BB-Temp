using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreathingLevels : MonoBehaviour
{
    public int id;
    public TMP_Text levelTxt;
    public Image outline;
    public Image background;
    public Button btn;

    [HideInInspector]
    public System.Action<BreathingLevels> onClick;

    public void OnClick()
    {
        onClick?.Invoke(this);
    }
}
