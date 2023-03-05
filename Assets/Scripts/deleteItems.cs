using Barnabus.EmotionFace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MakeAFaceItem"))
        {
            Debug.Log("item found");
            Destroy(collision.gameObject);
        }
    }
}
