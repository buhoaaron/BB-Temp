using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfBTNProp : MonoBehaviour
{
    public int id = -1;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnClick()
    {
        ShelfProp.Instance.DoScan(this);
        SendMessage();
    }
    private void SendMessage(){
        
        MessageCenter.PostMessage<ChangePopupMessage>(
            new ChangePopupMessage(){
                popupID = "id_card",
                eventID = "hub_tapped"
            }
        );
    }
}
