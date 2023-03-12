
using UnityEngine;

public class AudioSourceTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            AudioSourceManager.Instance.PauseBGM();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            AudioSourceManager.Instance.StopBGM();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            AudioSourceManager.Instance.PlaySound(AUDIO_NAME.BUTTON_CLICK);
        }
    }
}

