using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadCSV : MonoBehaviour
{
    public TextAsset textData = null;

    public List<string> playerList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadCSV());
    }

    private IEnumerator LoadCSV()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://script.google.com/macros/s/AKfycbx7fRPFtsoFasrIimvWY3czCO4NSIEY9A3MnFwvS_LpwfViYbMgyFl0CEZuJXVIJVfS/exec"))

        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("Up load Complete");
            }
        }
    }
}
