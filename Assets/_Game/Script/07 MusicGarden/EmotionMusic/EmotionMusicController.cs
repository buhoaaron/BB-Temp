using Barnabus.SceneManagement;
using Barnabus.UI;
using CustomAudio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Barnabus.EmotionMusic
{
    public enum MusicGameState { SelectMain, SelectSupport, SelectFinalCheck, EditMusic, Dance }

    public class EmotionMusicController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform canvas;
        [SerializeField]
        private EmotionMusicAsset asset;
        public EmotionMusicAsset Asset { get { return asset; } }

        [Header("Character Selector")]
        [SerializeField]
        private GameObject characterSelector;
        [SerializeField]
        private Transform characterButtonContainer;
        [SerializeField]
        private CharacterButton characterButtonPrefab;
        [SerializeField]
        private GameObject selectorConfirmButton;

        [Header("Prefab")]
        [SerializeField]
        private PotionRewardUI potionRewardUIPrefab;

        [Header("Select Character")]
        [SerializeField]
        private GameObject selectCharacterCanvas;
        [SerializeField]
        private List<SpineUIAnimator> stageCharacters;
        [SerializeField]
        private List<GameObject> spotlights;
        [SerializeField]
        private List<GameObject> stageButtons;
        [SerializeField]
        private GameObject selectBackButton;
        [SerializeField]
        private GameObject selectConfirmButton;
        [SerializeField]
        private GameObject leadTitle;
        [SerializeField]
        private GameObject groupTitle;

        [Header("Edit Music")]
        [SerializeField]
        private EM_SheetController sheetController;
        [SerializeField]
        private EM_SheetPlayer sheetPlayer;
        [SerializeField]
        private GameObject editMusicCanvas;

        [Header("Dance")]
        [SerializeField]
        private GameObject danceCanvas;
        [SerializeField]
        private EM_DancePlayer dancePlayer;

        private List<CharacterButton> characterButtons = new List<CharacterButton>();
        private int[] selectedCharactersID = new int[5] { -1, -1, -1, -1, -1 };
        private int currentSelectedCharacterID = -1;
        private int selectedStageIndex = -1;
        private MusicGameState gameState;

        private void Start()
        {
            DataManager.LoadCharacterData();

            //selectCharacterCanvas.SetActive(true);
            editMusicCanvas.SetActive(false);
            danceCanvas.SetActive(false);
            ChangeGameState(MusicGameState.SelectMain);
            GenerateCharacterButtons();
            //OnClick_ShowCharacterSelector(0);
            // DialogController.ShowDialog(DialogController.StringAsset.emotionMusicStartDialog, null);


        }

        void Update()
        {
            
           
            
        }

        public void ChangeGameState(MusicGameState state)
        {
            gameState = state;
            switch (gameState)
            {
                case MusicGameState.SelectMain:
                    for (int i = 0; i < selectedCharactersID.Length; i++)
                    {
                        spotlights[i].SetActive(i == 0);
                        stageButtons[i].SetActive(i == 0);
                    }
                    selectBackButton.SetActive(false);
                    selectConfirmButton.SetActive(selectedCharactersID[0] != -1);
                    break;
                case MusicGameState.SelectSupport:
                    selectCharacterCanvas.SetActive(true);
                    for (int i = 0; i < selectedCharactersID.Length; i++)
                    {
                        spotlights[i].SetActive(i != 0);
                        stageButtons[i].SetActive(i != 0);
                    }
                    selectBackButton.SetActive(true);
                    selectConfirmButton.SetActive(true);
                    break;
                case MusicGameState.SelectFinalCheck:
                    for (int i = 0; i < selectedCharactersID.Length; i++)
                    {
                        spotlights[i].SetActive(true);
                        stageButtons[i].SetActive(false);
                    }
                    selectBackButton.SetActive(true);
                    selectConfirmButton.SetActive(true);
                    break;
                case MusicGameState.EditMusic:
                    selectCharacterCanvas.SetActive(false);
                    editMusicCanvas.SetActive(true);
                    danceCanvas.SetActive(false);
                    break;
                case MusicGameState.Dance:
                    editMusicCanvas.SetActive(false);
                    danceCanvas.SetActive(true);
                    break;
            }
        }

        public void OnClick_SelectStageBack()
        {
            switch (gameState)
            {
                case MusicGameState.SelectMain:
                    break;
                case MusicGameState.SelectSupport:
                    for (int i = 1; i < selectedCharactersID.Length; i++)
                    {
                        selectedCharactersID[i] = -1;
                        stageCharacters[i].gameObject.SetActive(false);
                    }
                    selectedStageIndex = 0;
                    ChangeGameState(MusicGameState.SelectMain);
                    //RefreshCharacterButtonStates();
                    GenerateCharacterButtons();
                    characterSelector.SetActive(false);
                    selectConfirmButton.SetActive(false);
                    break;
                case MusicGameState.SelectFinalCheck:
                    ChangeGameState(MusicGameState.SelectSupport);
                    characterSelector.SetActive(true);
                    break;
                case MusicGameState.EditMusic:
                    sheetController.OnClick_StopPlaySheet();
                    characterSelector.SetActive(false);
                    editMusicCanvas.SetActive(false);
                    ChangeGameState(MusicGameState.SelectSupport);
                    for (int i = 0; i < selectedCharactersID.Length; i++)
                    {
                        SetStageCharacter(i, asset.GetCharacterAssetByID(selectedCharactersID[i]));
                    }
                    RefreshCharacterButtonStates();
                    break;
                case MusicGameState.Dance:
                    sheetPlayer.EraseSlider();
                    sheetPlayer.Invoke("RefreshSlider",.2f);
                    dancePlayer.Stop();
                    sheetController.OnClick_StopPlaySheet();
                    ChangeGameState(MusicGameState.EditMusic);
                    break;
            }
        }

        public void OnClick_SelectStageConfirm()
        {

            switch (gameState)
            {
                case MusicGameState.SelectMain:
                    ChangeGameState(MusicGameState.EditMusic);
                    //selectedStageIndex++;
                    break;
                case MusicGameState.SelectSupport:
                    ChangeGameState(MusicGameState.EditMusic);
                    sheetController.Initialize(selectedCharactersID);
                    OnClick_CloseCharacterSelector();
                    break;
                case MusicGameState.SelectFinalCheck:
                    sheetController.Initialize(selectedCharactersID);
                    ChangeGameState(MusicGameState.EditMusic);
                    break;
                case MusicGameState.EditMusic:
                    break;
            }

            
         
        }

        private void SetStageCharacter(int index, Character character)
        {
            if (character == null) stageCharacters[index].gameObject.SetActive(false);
            else
            {
                stageCharacters[index].gameObject.SetActive(true);
                stageCharacters[index].SetAsset(character.spineAsset);
                stageCharacters[index].Play(character.idleAnimationName);
            }
        }

        public void OnDataLoad(EM_Data data)
        {
            for (int i = 0; i < selectedCharactersID.Length; i++) selectedCharactersID[i] = data.charactersID[i];
            ChangeGameState(MusicGameState.EditMusic);
        }

        public void ShowEndDialog()
        {
            ShowAward();
            // DialogController.ShowDialog(DialogController.StringAsset.emotionMusicEndDialog, () => ShowAward());
        }

        public void ShowAward()
        {
            sheetController.StopAllCoroutines();
            dancePlayer.Stop();
            sheetPlayer.Stop();

            //FIXED: Unified potion reward UI
            var potionRewardUI = Instantiate(potionRewardUIPrefab, canvas);
            potionRewardUI.Init(POTION_REWARD_MODE.TWO_BUTTONS);
            potionRewardUI.DoPopUp();
            potionRewardUI.SetPotionValue(3);

            potionRewardUI.OnButtonBackMainClick = () =>
            {
                //Back MainRoom
                NewGameManager.Instance.AudioManager.PlaySound(AUDIO_NAME.BUTTON_CLICK);
                NewGameManager.Instance.SetSceneState(SCENE_STATE.LOADING_MAIN);
            };
            potionRewardUI.OnButtonReplayClick = () =>
            {
                //ReEnter Music
                NewGameManager.Instance.AudioManager.PlaySound(AUDIO_NAME.BUTTON_CLICK);
                NewGameManager.Instance.SetSceneState(SCENE_STATE.LOADING_MUSIC);
            };

            DataManager.LoadPotions();
            DataManager.Potions.AddPotion(Game.GameSettings.EmotionMusicPotionType, 3);
            DataManager.SavePotions();
        }

        private void ConfirmAutoSave(Action callback)
        {
            Notifier.ShowConfirmView("Warning", "Do you want to save data before leaving?",
                () =>
                {
                    sheetController.AutoSave();
                    callback.Invoke();
                },
                () => callback.Invoke());
        }

        #region Character Selector
        private void GenerateCharacterButtons()
        {
            for (int i = 0; i < characterButtons.Count; i++) Destroy(characterButtons[i].gameObject);
            characterButtons.Clear();

            CharacterButton newButton;
            for (int i = 0; i < asset.characterList.Count; i++)
            {
                newButton = Instantiate(characterButtonPrefab, characterButtonContainer);
                newButton.SetCharacterID(asset.characterList[i].id);
                newButton.SetOnClickCallback(OnClick_CharacterButton);
                newButton.SetEnable(true);
                newButton.SetFrameVisable(false);
                newButton.SetCharacterSprite(asset.characterList[i].icon);
                characterButtons.Add(newButton);
            }
        }

        public void OnClick_ShowCharacterSelector(int stageIndex)
        {
           
            if (selectedCharactersID[stageIndex] == -1)
            {
                //selectedStageIndex = GetEmptyCharacterIndex();
                if (selectedStageIndex == -1) selectedStageIndex = stageIndex;
            }
            else selectedStageIndex = stageIndex;

            leadTitle.SetActive(selectedStageIndex == 0);
            groupTitle.SetActive(selectedStageIndex != 0);

            currentSelectedCharacterID = selectedCharactersID[selectedStageIndex];
            if (selectedStageIndex == 0 && currentSelectedCharacterID == -1) selectorConfirmButton.SetActive(false);
            else selectorConfirmButton.SetActive(true);
            RefreshCharacterButtonStates();

            characterSelector.SetActive(true);
            
        }

        private int GetEmptyCharacterIndex()
        {
            for (int i = 0; i < selectedCharactersID.Length; i++)
            {
                if (selectedCharactersID[i] == -1) return i;
            }

            return -1;
        }

        public void OnClick_CloseCharacterSelector()
        {
            selectedStageIndex = -1;
            currentSelectedCharacterID = -1;
            characterSelector.SetActive(false);
        }
/*
        public void OnClick_SelectConfirm()
        {
            selectedCharactersID[selectedStageIndex] = currentSelectedCharacterID;

            if (gameState != MusicGameState.EditMusic)
            {
                FillUpEmptyCharacter();
                for (int i = 0; i < selectedCharactersID.Length; i++)
                    SetStageCharacter(i, asset.GetCharacterAssetByID(selectedCharactersID[i]));

                //if (gameState == MusicGameState.SelectMain) selectConfirmButton.SetActive(currentSelectedCharacterID != -1);
            }
            else
            {
                sheetController.SelectCharacter(selectedStageIndex, asset.GetCharacterAssetByID(currentSelectedCharacterID));
                sheetController.FillUpEmptyCharacter();
                FillUpEmptyCharacter();
            }
            //OnClick_CloseCharacterSelector();
        }*/

       /* private void FillUpEmptyCharacter()
        {
            int index = 999;
            for (int i = 0; i < selectedCharactersID.Length; i++)
            {
                if (selectedCharactersID[i] == -1)
                {
                    index = i;
                    break;
                }
            }
            if (index != 999)
            {
                for (int i = index; i < selectedCharactersID.Length - 1; i++)
                {
                    selectedCharactersID[i] = selectedCharactersID[i + 1];
                }
                selectedCharactersID[selectedCharactersID.Length - 1] = -1;
            }
        }*/

        private void OnClick_CharacterButton(CharacterButton button)
        {

            if (currentSelectedCharacterID == button.characterID && selectedStageIndex != 0)
            {
                currentSelectedCharacterID = -1;
                button.SetFrameVisable(false);
                
            }
            else
            {
                if (currentSelectedCharacterID != -1)
                {
                    GetCharacterButtonByID(currentSelectedCharacterID).SetFrameVisable(false);
                    if (currentSelectedCharacterID != button.characterID)
                        AudioManager.instance.StopSound(asset.GetCharacterAssetByID(currentSelectedCharacterID).sound);
                }
                currentSelectedCharacterID = button.characterID;
                button.SetFrameVisable(true);
                button.SetFrameSprite(asset.selectedFrames[selectedStageIndex]);
                
                AudioClip clip = asset.GetCharacterAssetByID(button.characterID).sound;
                if (!AudioManager.instance.IsSoundExist(clip)) AudioManager.instance.PlaySound(clip);

                selectedCharactersID[selectedStageIndex] = currentSelectedCharacterID;

 
            }

            for (int i = 0; i < selectedCharactersID.Length; i++)
            SetStageCharacter(i, asset.GetCharacterAssetByID(selectedCharactersID[i]));

            selectorConfirmButton.SetActive(true);

            OnClick_CloseCharacterSelector();
            RefreshCharacterButtonStates();

       
            ChangeGameState(MusicGameState.SelectSupport);
            
           /* if (selectedStageIndex != 0&&currentSelectedCharacterID!=-1)
            {
                selectedStageIndex++;

            }

            
            if (selectedStageIndex > 4)
            {
                selectedStageIndex = 4;

            }
            */

        }

        private CharacterButton GetCharacterButtonByID(int characterID)
        {
            for (int i = 0; i < characterButtons.Count; i++)
            {
                if (characterButtons[i].characterID == characterID) return characterButtons[i];
            }
            return null;
        }

        private void RefreshCharacterButtonStates()
        {

            int frameID;
            for (int i = 0; i < characterButtons.Count; i++)
            {
                if (GetCharacterSelectable(characterButtons[i].characterID))
                {
                    characterButtons[i].SetEnable(true);
                    characterButtons[i].SetCharacterSprite(asset.GetCharacterAssetByID(characterButtons[i].characterID).icon);

                }
                else
                {
                    characterButtons[i].SetEnable(false);
                    characterButtons[i].SetCharacterSprite(asset.GetCharacterAssetByID(characterButtons[i].characterID).lockedIcon);
                }

                frameID = GetCharacterFrameID(characterButtons[i].characterID);

                if (frameID != -1)
                {
                    characterButtons[i].SetFrameVisable(true);
                    characterButtons[i].SetFrameSprite(asset.selectedFrames[frameID]);
                    characterButtons[i].SetEnable(false);
                    characterButtons[i].SetCharacterSprite(asset.GetCharacterAssetByID(characterButtons[i].characterID).lockedIcon);
                }
                else characterButtons[i].SetFrameVisable(false);
            }
        }

        private bool GetCharacterSelectable(int characterID)
        {

            int stageIndex = GetSelectedCharacterStageIndex(characterID);

            //if (!IsCharacterUnlock(characterID)) return false; //�Ө��⥼���� => ���i���
            if (stageIndex != -1 && stageIndex != selectedStageIndex) return false; //�Ө���w��b��L�R�x��m => ���i���
            else return true;


            //return true;
        }

        private int GetCharacterFrameID(int characterID)
        {
            if (currentSelectedCharacterID == characterID) return selectedStageIndex;
            else return GetSelectedCharacterStageIndex(characterID);
        }

        private bool IsCharacterUnlock(int characterID)
        {
            return DataManager.IsCharacterUnlocked(characterID);
        }

        private int GetSelectedCharacterStageIndex(int characterID)
        {
            for (int i = 0; i < selectedCharactersID.Length; i++)
            {
                if (selectedCharactersID[i] == characterID) return i;
            }
            return -1;
        }
        #endregion
    }
}