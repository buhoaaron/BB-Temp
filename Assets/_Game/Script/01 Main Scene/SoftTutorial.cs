using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Barnabus;

public class SoftTutorial : MonoBehaviour
{
    // [SerializeField] private PlayableDirector softTimeline;

    [SerializeField] private GameObject container;
    [SerializeField] private PlayableDirector director;
    [SerializeField] private List<PlayableAsset> clips;

    private void OnEnable()
    {
        if (DataManager.SoftTutorialState > 3)
            return;
        SoftTutorialControl();
    }

    public void SoftTutorialControl()
    {
        int id = DataManager.SoftTutorialState;

        DataManager.LoadPotions();

        int counts = 0;
        counts = (DataManager.Potions[PotionType.Red] > 0 ? 1 : 0) +
                 (DataManager.Potions[PotionType.Blue] > 0 ? 1 : 0) +
                 (DataManager.Potions[PotionType.Green] > 0 ? 1 : 0) +
                 (DataManager.Potions[PotionType.Yellow] > 0 ? 1 : 0);

        if (counts > id)
        {
            container.SetActive(true);
            director.playableAsset = clips[id];
            director.Play();
            director.stopped += SoftFinish;
        }
    }

    public void SoftFinish(PlayableDirector aDirector)
    {
        container.SetActive(false);
        aDirector.playableAsset = null;
        director.stopped -= SoftFinish;
        DataManager.SoftTutorialState += 1;
    }
}
