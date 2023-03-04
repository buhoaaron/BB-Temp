using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ShelfProp : MonoBehaviour
{
    public static ShelfProp Instance;
    public CardContent cardContent;
    public Image barnabusInLabImage;
    public PlayableDirector director;

    public ShelfBTNProp[] bTNProps;

    private void OnEnable()
    {
        if (Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;

        BarnabusList barnabusList = NewGameManager.Instance.BarnabusCardManager.BarnabusList;
        foreach (ShelfBTNProp btn in bTNProps)
        {
            BarnabusScanScriptable barnabusScanScriptable = barnabusList[btn.id];
            btn.GetComponent<Image>().sprite = barnabusScanScriptable.GetBarnabusImage;
        }
    }
    public void DoScan(ShelfBTNProp prop)
    {
        int id = prop.id;
        BarnabusList barnabusList = Resources.Load<BarnabusList>("BarnabusCard/BarnabusList");
        BarnabusScanScriptable barnabusScanScriptable = barnabusList[id];
        cardContent.ReadBarnabus(barnabusScanScriptable);
        barnabusInLabImage.sprite = barnabusScanScriptable.GetBarnabusImage;
        director.Play();
        ChangeProp(id, barnabusScanScriptable.barnabusName, Barnabus.DataManager.IsCharacterUnlocked(id));
    }

    public void ChangeProp(int id, string name, bool hatched)
    {
        var dict = new Dictionary<string, string>();

        dict.Add("buddy_id", id.ToString());
        dict.Add("hatched", hatched.ToString());
        dict.Add("character_name", name);
        MessageCenter.PostMessage<ChangePropMessage>(
            new ChangePropMessage()
            {
                props = dict
            }
        );
    }
}
