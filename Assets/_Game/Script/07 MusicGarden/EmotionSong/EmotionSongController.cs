using CustomAudio;
using CustomAudio.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Barnabus.MusicGarden.Base;
using Game;
using System;

namespace Barnabus.MusicGarden.EmotionSong
{
    public class EmotionSongController : Base.SheetController<BabySound>
    {
        [Header("Dance")]
        [SerializeField]
        private DancePlayer dancePlayer;

        [Header("SoundButton")]
        [SerializeField]
        private List<SoundButton> selectedSoundButtons;

        [Header("ButtonSprite")]
        [SerializeField]
        private Sprite normalSprite;
        [SerializeField]
        private Sprite selectedSprite;

        [Header("SoundList")]
        [SerializeField]
        private GameObject soundList;
        [SerializeField]
        private SoundButton soundButtonPrefab;
        [SerializeField]
        private Transform soundButtonContainer;
        private List<SoundButton> soundButtonList = new List<SoundButton>();

        private int selectedSoundButtonID = 0;
        private int[] buttonSoundsID = new int[5] { 0, 1, 2, 3, 4}; //index 0 is parent barnabus
        private readonly float pitchScale = Mathf.Pow(2f, 1f / 12f);

        public override void Initialize()
        {
            base.Initialize();
            dancePlayer.SetSoundAssetList(soundAssets);
            dancePlayer.SetAutoSaveAction(AutoSave);

            if (GetActiveBarnabusCount() < buttonSoundsID.Length)
            {
                for (int i = 0; i < GetActiveBarnabusCount(); i++)
                {
                    selectedSoundButtons[i].backgroundImage.sprite = normalSprite;
                    selectedSoundButtons[i].soundImage.sprite = GetButtonSprite(buttonSoundsID[i]);
                }
                for (int i = GetActiveBarnabusCount(); i < buttonSoundsID.Length; i++)
                {
                    selectedSoundButtons[i].gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < selectedSoundButtons.Count; i++)
                {
                    selectedSoundButtons[i].backgroundImage.sprite = normalSprite;
                    selectedSoundButtons[i].soundImage.sprite = GetButtonSprite(buttonSoundsID[i]);
                }
            }

            selectedSoundButtonID = 1;
            selectedSoundButtons[1].backgroundImage.sprite = selectedSprite;
        }

        private int GetActiveBarnabusCount()
        {
            return soundAssets.Count;
        }

        public void OnClick_PlayDance()
        {
            StopSheetPlay();
            dancePlayer.PlayDance(sheet, buttonSoundsID);
        }

        //Data
        protected override void SetDataKey()
        {
            dataController.SetKey(GameSettings.EmotionSongDataPath);
        }

        public void AutoSave() { dataController.SaveData(sheet, "AutoSaveFile"); }

        protected override void LoadData()
        {
            Sheet<BabySound> tempSheet = dataController.LoadSelectedData<Sheet<BabySound>>();
            if (IsButtonIdOutOfRange(tempSheet))
            {
                StartCoroutine(WaitForFrame(() => Notifier.ShowAlert("Load failed.")));
                return;
            }

            StopSheetPlay();
            StopAllCoroutines();

            sheet = tempSheet;
            StartCoroutine(UpdateSheetView());
            SetButtonSoundBySheet();
        }

        protected override void ImportData()
        {
            Sheet<BabySound> tempSheet = dataController.ImportData<Sheet<BabySound>>();
            if (IsButtonIdOutOfRange(tempSheet))
            {
                StartCoroutine(WaitForFrame(() => Notifier.ShowAlert("Parse failed.")));
                return;
            }

            StopSheetPlay();
            StopAllCoroutines();

            sheet = tempSheet;
            StartCoroutine(UpdateSheetView());
            SetButtonSoundBySheet();
        }

        private IEnumerator WaitForFrame(Action action)
        {
            yield return null;
            action?.Invoke();
        }

        private bool IsButtonIdOutOfRange(Sheet<BabySound> tempSheet)
        {
            Beat<BabySound> beat;
            Note<BabySound> note;
            for (int measureIndex = 0; measureIndex < tempSheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = tempSheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        if (note.info.buttonID >= buttonSoundsID.Length) return true;
                    }
                }
            }

            return false;
        }

        // Measure
        protected override int GetNoteAmountPerMeasure()
        {
            return 36;
        }

        //Sound
        public void SelectSound(int buttonID)
        {
            if (selectedSoundButtonID == buttonID) 
            {
                if (GetActiveBarnabusCount() > buttonSoundsID.Length) ShowSoundList();
            }
            else
            {
                selectedSoundButtons[selectedSoundButtonID].backgroundImage.sprite = normalSprite;
                selectedSoundButtonID = buttonID;
                selectedSoundButtons[selectedSoundButtonID].backgroundImage.sprite = selectedSprite;
            }
        }

        public void ChangeButtonSound(int soundID)
        {
            ReplaceNoteSound(buttonSoundsID[selectedSoundButtonID], soundID);
            buttonSoundsID[selectedSoundButtonID] = soundID;
            selectedSoundButtons[selectedSoundButtonID].soundImage.sprite = GetButtonSprite(soundID);
            HideSoundList();
        }

        private void ShowSoundList()
        {
            soundList.SetActive(true);

            if (soundButtonList.Count == 0)
            {
                for(int i = 0; i < soundAssets.Count; i++)
                {
                    if (!IsSoundSelected(i))
                    {
                        soundButtonList.Add(Instantiate(soundButtonPrefab, soundButtonContainer));
                        soundButtonList[^1].SetSoundID(i);
                        soundButtonList[^1].SetOnClickCallback(ChangeButtonSound);
                        soundButtonList[^1].soundImage.sprite = GetButtonSprite(i);
                    }
                }
            }
            else
            {
                int index = 0;
                int soundID = 0;
                for (int i = 0; i < soundAssets.Count; i++)
                {
                    if (!IsSoundSelected(soundID))
                    {
                        soundButtonList[index].SetSoundID(soundID);
                        soundButtonList[index].soundImage.sprite = GetButtonSprite(soundID);
                        index++;
                    }
                    soundID++;
                }
            }
        }

        public void HideSoundList()
        {
            soundList.SetActive(false);
        }

        private bool IsSoundSelected(int soundID)
        {
            for(int i = 0; i < buttonSoundsID.Length; i++)
                if(buttonSoundsID[i] == soundID) 
                    return true;

            return false;
        }

        private void SetButtonSoundBySheet()
        {
            for (int i = 0; i < buttonSoundsID.Length; i++) buttonSoundsID[i] = -1;
            
            Beat<BabySound> beat;
            Note<BabySound> note;
            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        buttonSoundsID[note.info.buttonID] = note.info.soundID;
                    }
                }
            }

            int soundID = 0;

            for(int i = 0; i < buttonSoundsID.Length; i++)
            {
                if(buttonSoundsID[i] == -1)
                {
                    while(IsSoundInButtonSounds(soundID)) soundID++;
                    buttonSoundsID[i] = soundID;
                }
                selectedSoundButtons[i].soundImage.sprite = GetButtonSprite(buttonSoundsID[i]);
            }
        }

        private bool IsSoundInButtonSounds(int soundID)
        {
            for (int i = 0; i < buttonSoundsID.Length; i++)
                if (buttonSoundsID[i] == soundID) 
                    return true;

            return false;
        }

        private Sprite GetButtonSprite(int soundID)
        {
            return soundAssets[soundID].buttonSprite;
        }

        private Sprite GetNoteSprite(int soundID)
        {
            return soundAssets[soundID].noteSprite;
        }

        //Note
        public override void ClickedNote(int measureID, int noteID)
        {
            if (selectedSoundButtonID == 0 && noteID > 3) return;
            else if (selectedSoundButtonID != 0 && noteID <= 3) return;

            AddNote(measureID, noteID, out int removedNoteID);

            if (removedNoteID != -1) GetNoteButton(measureID, removedNoteID).Disappear();
            if (noteID != removedNoteID)
            {
                GetNoteButton(measureID, noteID).Appear();
                GetNoteButton(measureID, noteID).SetImage(GetNoteSprite(buttonSoundsID[selectedSoundButtonID]));

                if (!sheetPlayer.IsPlaying)
                    AudioManager.instance.PlaySound(soundAssets[buttonSoundsID[selectedSoundButtonID]].audioClip, GetPitchByNoteID(noteID));
            }
        }

        protected override void AddNote(int measureID, int noteID, out int removedNoteID)
        {
            List<Note<BabySound>> notes = sheet.measures[measureID].beats[noteID % 4].notes;

            removedNoteID = -1;
            int selectedSoundID = buttonSoundsID[selectedSoundButtonID];
            int sameIdIndex = FindNoteById(notes, noteID);
            int sameSoundIndex = FindNoteBySoundID(notes, selectedSoundID);

            if (sameIdIndex == -1) //點擊的格子是空的
            {
                if (sameSoundIndex == -1) //該beat尚未有這個樂器 => 新增
                {
                    sheet.AddNote(measureID, noteID, new BabySound(selectedSoundButtonID, selectedSoundID, GetPitchByNoteID(noteID)));
                }
                else //該beat其他格子已有這個樂器 => 刪掉該格，新增這格
                {
                    removedNoteID = notes[sameSoundIndex].id;
                    sheet.RemoveNote(measureID, removedNoteID);
                    sheet.AddNote(measureID, noteID, new BabySound(selectedSoundButtonID, selectedSoundID, GetPitchByNoteID(noteID)));
                }
            }
            else //點擊的格子已有樂器
            {
                if (notes[sameIdIndex].info.soundID == selectedSoundID) //與該格樂器相同 => 刪除
                {
                    removedNoteID = notes[sameIdIndex].id;
                    sheet.RemoveNote(measureID, removedNoteID);
                }
                else //與該格樂器不同 => 替換該格樂器
                {
                    notes[sameIdIndex].info.soundID = selectedSoundID;

                    if (sameSoundIndex != -1) //該beat其他格子有相同樂器 => 刪除該格
                    {
                        removedNoteID = notes[sameSoundIndex].id;
                        sheet.RemoveNote(measureID, removedNoteID);
                    }
                }
            }
        }

        private int FindNoteById(List<Note<BabySound>> notes, int noteID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].id == noteID)
                    return i;

            return -1;
        }

        private int FindNoteBySoundID(List<Note<BabySound>> notes, int soundID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].info.soundID == soundID)
                    return i;

            return -1;
        }

        private NoteButton GetNoteButton(int measureID, int noteID)
        {
            return measureViews[measureID].GetNoteButton(noteID);
        }

        private float GetPitchByNoteID(int id)
        {
            return (id / 4) switch
            {
                2 => Mathf.Pow(pitchScale, 2), // D
                3 => Mathf.Pow(pitchScale, 4), // E
                4 => Mathf.Pow(pitchScale, 5), // F
                5 => Mathf.Pow(pitchScale, 7), // G
                6 => Mathf.Pow(pitchScale, 9), // A
                7 => Mathf.Pow(pitchScale, 11), // B
                8 => 2, // High C
                _ => 1, //Parent Barnabus or C
            };
        }

        private void ReplaceNoteSound(int oldSoundID, int newSoundID)
        {
            Beat<BabySound> beat;
            Note<BabySound> note;
            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        if (note.info.soundID == oldSoundID)
                        {
                            note.info.soundID = newSoundID;
                            GetNoteButton(measureIndex, note.id).SetImage(GetNoteSprite(newSoundID));
                        }
                    }
                }
            }
        }

        protected override void SetNoteButton(int measureID, Note<BabySound> note)
        {
            GetNoteButton(measureID, note.id).SetImage(GetNoteSprite(note.info.soundID));
        }
    }
}