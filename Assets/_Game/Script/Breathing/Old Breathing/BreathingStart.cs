using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Timeline.Time;

public class BreathingStart : MonoBehaviour
{
    public TimeController timeController;

    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "AngerBreathing":
                DialogController.ShowDialog(DialogController.StringAsset.angerbreathingStartDialog, () => PlayBreathing());
                break;
            case "PropBreathing":
                DialogController.ShowDialog(DialogController.StringAsset.propbreathingStartDialog, () => PlayBreathing());
                break;
        }
    }

    private void PlayBreathing()
    {
        timeController.Resume();
    }
}
