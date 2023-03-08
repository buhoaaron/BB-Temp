using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class BreathingEnd : MonoBehaviour
{
    public PlayableDirector director;
    [Header("Pause Contents")]
    public Image pauseImage;
    public Sprite pauseSprite, resumeSprite;
    public GameObject pausePanel;

    bool isPaused;
    public void DoPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            director.Pause();
            pauseImage.sprite = pauseSprite;
            pausePanel.SetActive(true);
        }
        else
        {
            director.Resume();
            pauseImage.sprite = resumeSprite;
            pausePanel.SetActive(false);
        }
    }
    public void ShowEndDialog()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "AngerBreathing":
                DialogController.ShowDialog(DialogController.StringAsset.angerbreathingEndDialog, () => ShowAward());
                break;
            case "PropBreathing":
                DialogController.ShowDialog(DialogController.StringAsset.propbreathingEndDialog, () => ShowAward());
                break;
        }
    }

    private void ShowAward()
    {
        AwardController.SetPotionSprite(Game.GameSettings.BreathingPotionType);
        AwardController.ShowAward(3, 3,
                () => SceneTransit.LoadSceneAsync("MainScene"),
                () => SceneTransit.LoadSceneAsync("AngerBreathing"),
                () => SceneTransit.LoadSceneAsync("AngerBreathing")
        );

        Barnabus.DataManager.LoadPotions();
        Barnabus.DataManager.Potions.AddPotion(Game.GameSettings.BreathingPotionType, 3);
        Barnabus.DataManager.SavePotions();
    }
}
