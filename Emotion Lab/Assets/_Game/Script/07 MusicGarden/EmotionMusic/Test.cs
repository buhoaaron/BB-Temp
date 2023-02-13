using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public AudioSource audioSource;
    public float time;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Stop();
            audioSource.time = time;
            audioSource.Play();
        }
        Debug.Log(audioSource.time);
    }
}
