using Barnabus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class LabScene : MonoBehaviour
{
    public BarnabusList barnabusList;
    public Image barnabusInLab;
    public SkeletonGraphic skeletonGraphic;

    private void OnEnable()
    {
        DataManager.LoadCharacterData();

        BarnabusScanScriptable nextCharacter = GetNextLockedCharacter();
        if (nextCharacter != null)
        {
            // barnabusInLab.sprite = nextCharacter.GetBarnabusImage;
            ChangeBB(nextCharacter);
        }
        else barnabusInLab.gameObject.SetActive(false);
    }

    public void ChangeBB(BarnabusScanScriptable nextCharacter)
    {
        if (nextCharacter.skeletonData != null)
        {
            skeletonGraphic.skeletonDataAsset = nextCharacter.skeletonData;
            skeletonGraphic.Initialize(true);
            Material _mat = nextCharacter.skeletonMaterial;
            nextCharacter.SetSkeletonMaterial();
            skeletonGraphic.material = nextCharacter.skeletonMaterial;

            skeletonGraphic.AnimationState.SetAnimation(0, nextCharacter.GetAnimationName, true);
        }
    }

    private BarnabusScanScriptable GetNextLockedCharacter()
    {
        for (int i = 1; i <= barnabusList.Count; i++)
        {
            if (!DataManager.IsCharacterUnlocked(i))
            {
                if (barnabusList[i] != null) return barnabusList[i];
            }
        }
        return null;
    }
}
