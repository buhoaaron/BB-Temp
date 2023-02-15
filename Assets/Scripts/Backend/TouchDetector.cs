using CustomAudio;
using CustomAudio.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour
{
    public string ButtonTag;
    public ClipResources UIAudios;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 touchpos = TouchPoint();
        if(IsTouchScreen(touchpos))
        {
            ButtonTag = GetCurrentButtonTag();
            
            Debug.Log(ButtonTag);

            if(ButtonTag=="Button")
            {
                AudioManager.instance.PlaySound(UIAudios.ButtonSFX);
            }
            if(ButtonTag=="Delete")
            {
                AudioManager.instance.PlaySound(UIAudios.DeleteSFX);
            }
            if(ButtonTag=="Camera")
            {
                AudioManager.instance.PlaySound(UIAudios.CameraSFX);
            }
        }

    }

    bool IsTouchScreen(Vector3 touchpos)
    {
        return touchpos != Vector3.zero;
    }

    string GetCurrentButtonTag()
    {
        GameObject selectObj = EventSystem.current.currentSelectedGameObject;
        if (selectObj != null)
        {
            if (selectObj.CompareTag("Button"))
            {
                return selectObj.tag;
            }

            else
            
            return selectObj.tag;


        }
        else return "";

        
    }
    Vector3 TouchPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return Input.mousePosition;
        }
        else return Vector3.zero;
    }
}
