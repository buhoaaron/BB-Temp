using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

[RequireComponent(typeof(SkeletonGraphic))]
public class SpineUIAnimator : MonoBehaviour
{
    private SkeletonGraphic spine;

    private void Awake()
    {
        spine = GetComponent<SkeletonGraphic>();
    }

    public void SetAsset(SkeletonDataAsset asset) { spine.skeletonDataAsset = asset; }

    public void Play(string animationName, bool loop = true, Action onPlayCompletedCallback = null)
    {
        spine.startingAnimation = animationName;
        spine.startingLoop = loop;
        spine.Initialize(true);

        StopAllCoroutines();
        if (onPlayCompletedCallback != null) StartCoroutine(WaitForPlay(GetAnimationDuration(animationName), onPlayCompletedCallback));
    }

    public void Stop()
    {
        spine.startingAnimation = "";
        spine.startingLoop = false;
        spine.Initialize(true);
    }

    public float GetAnimationDuration(string animationName)
    {
        return spine.skeletonDataAsset.GetSkeletonData(true).FindAnimation(animationName).Duration;
    }

    private IEnumerator WaitForPlay(float delayTime, Action onPlayCompletedCallback)
    {
        yield return new WaitForSeconds(delayTime);

        onPlayCompletedCallback.Invoke();
    }
}
