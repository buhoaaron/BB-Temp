using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Barnabus;
using Spine.Unity;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BarnabusScan", order = 1)]
public class BarnabusScanScriptable : ScriptableObject
{
    public int id;
    public bool isLocked = true;
    [Header("Potion_Related")]
    public PotionRequirement potionRequirement;

    public bool IsUnlocked() { return DataManager.IsCharacterUnlocked(id); }

    public Sprite GetCardImage
    {
        get
        {
            if (DataManager.IsCharacterUnlocked(id)) return cardImage;
            return lockedCardImage;
        }
    }

    public Sprite GetBarnabusImage
    {
        get
        {
            if (DataManager.IsCharacterUnlocked(id)) return barnabusImage;
            return lockedBarnabusImage;
        }
    }

    public string GetAnimationName
    {
        get
        {
            if (DataManager.IsCharacterUnlocked(id)) return idleAnimation;
            return idleSleepAnimation;
        }
    }

    public void SetSkeletonMaterial()
    {
        if (skeletonMaterial == null)
            return;

        if (DataManager.IsCharacterUnlocked(id))
        {
            skeletonMaterial.SetFloat("_GrayPhase", 0);
        }
        else
        {
            skeletonMaterial.SetFloat("_GrayPhase", 1);
        }
    }

    public Sprite CharacterImage { get { return barnabusImage; } }

    [Header("ID_Card")]
    [SerializeField] Sprite cardImage;
    [SerializeField] Sprite barnabusImage;

    [SerializeField] Sprite lockedCardImage;
    [SerializeField] Sprite lockedBarnabusImage;

    public string barnabusNo, element, elementColor, barnabusName;
    public string eyerbow, face, mouth, body, soundDescribe;
    public AudioClip barnabusSound, huntingFoundSound;

    [Header("Spine")]
    public SkeletonDataAsset skeletonData;

    public Material skeletonMaterial;
    [SpineAnimation(dataField: "skeletonData")]

    [SerializeField] string idleAnimation = "idle", idleSleepAnimation = "idle_sleep";

    [Header("Story")]
    public AudioClip storySound;

}

[System.Serializable]
public class PotionRequirement
{
    public int red;
    public int green;
    public int yellow;
    public int blue;
}
