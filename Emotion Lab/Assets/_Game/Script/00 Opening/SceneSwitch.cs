using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    public void ReplayIntro()
    {
        SceneTransit.LoadScene("opening");
    }

}
