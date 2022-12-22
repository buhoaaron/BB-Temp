using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBTN : MonoBehaviour
{
    public void BackToHome()
    {
        BackToMainScene.Instance.GoHome();
    }
}
