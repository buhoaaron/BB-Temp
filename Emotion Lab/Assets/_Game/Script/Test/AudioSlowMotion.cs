using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSlowMotion : MonoBehaviour
{
    public AudioSource audioSource;
    public float targetLength;
    
    public void PlayClip()
    {
        Debug.Log("Audio Length: " + audioSource.clip.length);
        if (audioSource.clip.length >= targetLength) audioSource.Play();
        else
        {
            audioSource.pitch = audioSource.clip.length / targetLength;
            audioSource.Play();
        }
    }
}