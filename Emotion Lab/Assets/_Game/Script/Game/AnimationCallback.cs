using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationCallback : MonoBehaviour
{
    [NonReorderable]
    public List<UnityEvent> callbacks;

    public void Do(int callbackIndex) { callbacks[callbackIndex]?.Invoke(); }
}