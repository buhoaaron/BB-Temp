using UnityEngine;
using Spine.Unity;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AddressableAssets;

public class SpineTest : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic;
    public List<SkeletonDataAsset> skeletonDataAsset;

    // Start is called before the first frame update

    void Start()
    {
        //AssetLabelReference label = new AssetLabelReference();
        //label.labelString = "HiAndByeSpineAssets";

        StartCoroutine(LoadSpineAsset());

        //Addressables.Release(handle);
    }

    private IEnumerator LoadSpineAsset()
    {
        var handle = Addressables.LoadAssetsAsync<SkeletonDataAsset>("HiAndByeSpine", skeletonDataAsset.Add);

        yield return handle;

        print("Done");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            skeletonGraphic.skeletonDataAsset = skeletonDataAsset.Find(x=>x.name=="Anger");
            skeletonGraphic.Initialize(true);
            //skeletonGraphic.AnimationState.SetAnimation(0, "idle", true);
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            skeletonGraphic.skeletonDataAsset = skeletonDataAsset.Find(x => x.name == "Joy");
            skeletonGraphic.Initialize(true);
            //skeletonGraphic.AnimationState.SetAnimation(0, "idle", true);
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            skeletonGraphic.skeletonDataAsset = skeletonDataAsset.Find(x => x.name == "Sadness");
            skeletonGraphic.Initialize(true);
            //skeletonGraphic.AnimationState.SetAnimation(0, "idle", true);
        }
    }
}
