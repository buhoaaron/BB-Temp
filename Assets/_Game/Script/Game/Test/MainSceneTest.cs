using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Barnabus;
using System.IO;

public class MainSceneTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DataManager.LoadCharacterData();
            Debug.Log(DataManager.Characters.ToJson());
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            FindObjectOfType<TutorialDetect>().director.Stop();
        }
    }

    public void OnClick_AddRed()
    {
        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(PotionType.Red, 2);
        DataManager.SavePotions();
    }
    public void OnClick_AddYellow()
    {
        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(PotionType.Yellow, 2);
        DataManager.SavePotions();
    }
    public void OnClick_AddGreen()
    {
        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(PotionType.Green, 2);
        DataManager.SavePotions();
    }
    public void OnClick_AddBlue()
    {
        DataManager.LoadPotions();
        DataManager.Potions.AddPotion(PotionType.Blue, 2);
        DataManager.SavePotions();
    }
    public void OnClick_ResetPotions()
    {
        PlayerPrefs.DeleteKey("Potions");
        DataManager.LoadPotions();
    }
    public void OnClick_ResetCharacters()
    {
        PlayerPrefs.DeleteKey("Characters");
        DataManager.LoadCharacterData();
        DataManager.Characters.SetCharacter(new CharacterData(1, true));
        DataManager.SaveCharacterData();
    }
    public void OnClick_ResetGame()
    {
        PlayerPrefs.DeleteAll();
        DataManager.LoadPotions();
        DataManager.LoadCharacterData();
        DataManager.LoadGamePassedTime();
        PlayerPrefs.SetInt("IsTutorialPlayed", 0);
        DataManager.SoftTutorialState = 0;
        if (Directory.Exists(Application.persistentDataPath + "/System/EmotionFace"))
            Directory.Delete(Application.persistentDataPath + "/System/EmotionFace", true);
        DataManager.SetPlayerID();
        SceneTransit.LoadSceneAsync("MainScene");
    }
}