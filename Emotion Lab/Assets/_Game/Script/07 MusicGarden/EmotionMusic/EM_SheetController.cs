using CustomAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using System;

namespace Barnabus.EmotionMusic
{
    public class EM_SheetController : MonoBehaviour
    {
        public EmotionMusicAsset Asset { get { return emotionMusicController.Asset; } }
        public bool IsPreviewingSong { get; set; }

        [SerializeField]
        private Image background;

        [Space(10)]
        [SerializeField]
        private EmotionMusicController emotionMusicController;
        [SerializeField]
        private EM_DancePlayer dancePlayer;
        [SerializeField]
        private DataController dataController;

        [Header("Sound Button")]
        [SerializeField]
        private List<SoundButton> soundButtons;

        [Header("Song Selector")]
        [SerializeField]
        private GameObject songSelector;
        [SerializeField]
        private SongButton songButtonPrefab;
        [SerializeField]
        private Transform songButtonContainer;

        [Header("Song Info")]
        [SerializeField]
        private Image songImage;
        [SerializeField]
        private TMPro.TextMeshProUGUI songName;
        [SerializeField]
        private Animator songCdAnimator;

        [Header("Measure")]
        [SerializeField]
        private MeasureView measurePrefab;
        [SerializeField]
        private GameObject measureSplitLine;
        [SerializeField]
        private Transform measureContainer;

        [Header("Sheet Play")]
        [SerializeField]
        private EM_SheetPlayer sheetPlayer;
        [SerializeField]
        private GameObject startPlaySheetButton;
        [SerializeField]
        private GameObject stopPlaySheetButton;

        [Header("Note Position Convert")]
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private RectTransform sheetCanvas;

        [Header("NoteLinker")]
        [SerializeField]
        private int maxNoteLinkCount = 4;
        [SerializeField]
        private ScrollRect sheetScrollRect;
        [SerializeField]
        private NoteLinker noteLinkerPrefab;
        [SerializeField]
        private Transform noteLinkerContainer;
        [SerializeField]
        private RectTransform sheetRectTransform;
        [SerializeField]
        private RectTransform noteLinkersRectTransform;

        private int selectedSoundButtonIndex = 0;
        private Character MainCharacter { get { return Asset.GetCharacterAssetByID(soundButtons[0].characterID); } }
        private Character SelectedCharacter { get { return Asset.GetCharacterAssetByID(SelectedCharacterID); } }
        private int SelectedCharacterID { get { return soundButtons[selectedSoundButtonIndex].characterID; } }

        private int selectedSongID = 0;
        private int currentSongID = 0;
        private Song CurrentSong { get { return MainCharacter.songList[currentSongID]; } }
        private bool isSongPlayingBeforeSelectSong;

        private List<NoteButton> tempNoteButtonLinkList = new List<NoteButton>();
        private List<NoteLinker> noteLinkerList = new List<NoteLinker>();

        public void Initialize(int[] charactersID)
        {
            IsPreviewingSong = false;

            selectedSoundButtonIndex = 0;
            SetSoundButtons(charactersID);
            background.sprite = MainCharacter.musicBackground;

            currentSongID = 0;
            songButtonContainer.GetComponent<RectTransform>().anchoredPosition *= new Vector2(0, 1);
            GenerateSongButtons(MainCharacter);
            SetSongInfo(CurrentSong);

            GenerateEmptySheet(CurrentSong);
            sheetPlayer.InitializeBySong(CurrentSong);

            //OnClick_StopPlaySheet();
            StartCoroutine(WaitForOneFrame(OnClick_PlaySheet));
        }

        private void Start()
        {
            DataControllerInitialize();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                GenerateEmptySheet(CurrentSong);
                NoteButton note;
                for (int i = 0; i < sheet.measures.Count; i++)
                {
                    if(i % 2 == 0)
                    {
                        note = GetNoteButton(i, 4);
                        note.Appear();
                        note.SetSprite(SelectedCharacter.noteIcon);
                        //note.SetColor(SelectedCharacter.noteColor);
                        AddNote(i, 4, out int none);
                    }
                    else
                    {
                        note = GetNoteButton(i, 0);
                        note.Appear();
                        note.SetSprite(SelectedCharacter.noteIcon);
                        //note.SetColor(SelectedCharacter.noteColor);
                        AddNote(i, 0, out int none);
                    }
                    note = GetNoteButton(i, 2);
                    note.Appear();
                    note.SetSprite(SelectedCharacter.noteIcon);
                    //note.SetColor(SelectedCharacter.noteColor);
                    AddNote(i, 2, out int removedNoteID);
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteKey(GameSettings.EmotionMusicDataPath);
            }

            noteLinkersRectTransform.anchoredPosition = sheetRectTransform.anchoredPosition;
        }

        private IEnumerator WaitForOneFrame(Action action)
        {
            yield return null;
            action?.Invoke();
        }

        #region Sound Button
        public void SetSoundButtons(int[] charactersID)
        {
            for(int i = 0; i < charactersID.Length; i++)
            {
                SetSoundButton(i, emotionMusicController.Asset.GetCharacterAssetByID(charactersID[i]));
                soundButtons[i].SetFrameVisable(soundButtons[i].characterID == selectedSoundButtonIndex);
            }
        }

        private void SetSoundButton(int index, Character character)
        {
            if(character == null)
            {
                soundButtons[index].SetEmpty(true);
                soundButtons[index].SetCharacterID(-1);
                soundButtons[index].SetBackgroundSprite(Asset.defaultSoundButtonBackground);
            }
            else
            {
                soundButtons[index].SetEmpty(false);
                soundButtons[index].SetCharacterID(character.id);
                soundButtons[index].SetBackgroundSprite(character.buttonBackground);
                //soundButtons[index].SetBackgroundColor(character.noteColor);
                soundButtons[index].SetCharacterSprite(character.icon);
            }
        }

        public void OnClick_SoundButton(int index)
        {
            if(soundButtons[index].characterID == -1 || (selectedSoundButtonIndex == index && index != 0))
            {
                emotionMusicController.OnClick_ShowCharacterSelector(index);
            }
            else
            {
                soundButtons[selectedSoundButtonIndex].SetFrameVisable(false);
                selectedSoundButtonIndex = index;
                soundButtons[selectedSoundButtonIndex].SetFrameVisable(true);
            }
        }

        public void SelectCharacter(int index, Character character)
        {
            soundButtons[selectedSoundButtonIndex].SetFrameVisable(false);
            if (character == null && selectedSoundButtonIndex == index)
            {
                RemoveNotesByCharacterID(soundButtons[selectedSoundButtonIndex].characterID);
                selectedSoundButtonIndex = 0;
            }
            else if(character != null)
            {
                if(selectedSoundButtonIndex == index)
                {
                    ReplaceNoteCharacterID(soundButtons[selectedSoundButtonIndex].characterID, character.id);
                }
                selectedSoundButtonIndex = index;
            }
            soundButtons[selectedSoundButtonIndex].SetFrameVisable(true);

            SetSoundButton(index, character);
        }

        public void FillUpEmptyCharacter()
        {
            int index = 999;
            for (int i = 0; i < soundButtons.Count; i++)
            {
                if (soundButtons[i].characterID == -1)
                {
                    index = i;
                    break;
                }
            }
            if (index != 999)
            {
                for (int i = index; i < soundButtons.Count - 1; i++)
                {
                    SetSoundButton(i, Asset.GetCharacterAssetByID(soundButtons[i + 1].characterID));
                }
                SetSoundButton(soundButtons.Count - 1, null);
            }
        }
        #endregion

        #region Song Selector
        private List<SongButton> songButtons = new List<SongButton>();

        private void GenerateSongButtons(Character character)
        {
            for (int i = 0; i < songButtons.Count; i++) Destroy(songButtons[i].gameObject);
            songButtons.Clear();

            SongButton newButton;
            for (int i = 0; i < character.songList.Count; i++)
            {
                newButton = Instantiate(songButtonPrefab, songButtonContainer);
                newButton.SetID(i);
                newButton.SetOnClickCallback(OnClick_SongButton);
                newButton.SetSongSprite(character.songList[i].sprite);
                newButton.SetSongName(character.songList[i].name);
                newButton.SetFrameVisable(i == currentSongID);
                songButtons.Add(newButton);
            }
        }

        private void OnClick_SongButton(SongButton button)
        {
            //if (selectedSongID == button.id) return;

            songButtons[selectedSongID].SetFrameVisable(false);
            songButtons[selectedSongID].Stop();

            selectedSongID = button.id;
            button.SetFrameVisable(true);
            button.Play();
            AudioManager.instance.PlayBGM(MainCharacter.songList[selectedSongID].audioClip);
            IsPreviewingSong = true;
        }

        public void OnClick_ConfirmSongSelect()
        {
            currentSongID = selectedSongID;
            SetSongInfo(CurrentSong);
            GenerateEmptySheet(CurrentSong);
            sheetPlayer.InitializeBySong(CurrentSong);
            OnClick_CloseSongSelector();
        }

        public void OnClick_ShowSongSelector()
        {
            isSongPlayingBeforeSelectSong = sheetPlayer.IsPlaying;
            if (sheetPlayer.IsPlaying) OnClick_StopPlaySheet();

            songSelector.SetActive(true);
            OnClick_SongButton(songButtons[currentSongID]);
        }

        public void OnClick_CloseSongSelector()
        {
            songSelector.SetActive(false);
            AudioManager.instance.StopBGM();
            IsPreviewingSong = false;

            if (isSongPlayingBeforeSelectSong) OnClick_PlaySheet();
        }

        private void SetSongInfo(Song song)
        {
            songImage.sprite = song.sprite;
            songName.text = song.name;
        }
        #endregion

        #region Sheet
        private Sheet<CharacterSound> sheet = new Sheet<CharacterSound>();
        private List<MeasureView> measureViews = new List<MeasureView>();
        private List<GameObject> measureLines = new List<GameObject>();

        private readonly float pitchScale = Mathf.Pow(2f, 1f / 12f);

        private void GenerateEmptySheet(Song song)
        {
            for (int i = 0; i < measureViews.Count; i++) Destroy(measureViews[i].gameObject);
            measureViews.Clear();
            for (int i = 0; i < measureLines.Count; i++) Destroy(measureLines[i]);
            measureLines.Clear();
            for (int i = 0; i < noteLinkerList.Count; i++) Destroy(noteLinkerList[i].gameObject);
            noteLinkerList.Clear();

            sheet = new Sheet<CharacterSound>();
            sheet.bpm = song.bpm;

            MeasureView newMeasureView;
            int measureCount = GetMeasureCount(song);
            for (int i = 0; i < measureCount; i++)
            {
                if(i != 0) measureLines.Add(Instantiate(measureSplitLine, measureContainer));
                newMeasureView = Instantiate(measurePrefab, measureContainer);
                newMeasureView.Initialize(measureViews.Count, 28, OnClick_Note, OnNotePointerEnter, OnNotePointerExit, OnNotePointerDown, OnNotePointerUp);
                measureViews.Add(newMeasureView);
                sheet.AddMeasure();
            }
        }

        private IEnumerator SetSheet(Song song, Sheet<CharacterSound> sheetData)
        {
            GenerateEmptySheet(song);

            sheet = sheetData;
            sheet.bpm = song.bpm;

            yield return null;

            Beat<CharacterSound> beat;
            Note<CharacterSound> note;
            NoteLinker noteLinker;
            NotePointer pointer, lastPointer;
            NoteButton beginNoteButton, lastNoteButton;

            for (int measureIndex = 0; measureIndex < measureViews.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        pointer = new NotePointer(measureIndex, note.id);
                        GetNoteButton(measureIndex, note.id).Show();
                        SetNoteButton(measureIndex, note);

                        if (note.info.isLongSound && !IsInLinkerList(pointer))
                        {
                            lastPointer = GetLinkLastPointer(pointer);
                            beginNoteButton = GetNoteButton(pointer.measureID, pointer.noteID);
                            lastNoteButton = GetNoteButton(lastPointer.measureID, lastPointer.noteID);

                            noteLinker = Instantiate(noteLinkerPrefab, noteLinkerContainer);
                            noteLinker.SetImage(Asset.GetCharacterAssetByID(note.info.characterID).longNoteIcon);
                            noteLinker.BeginNote = pointer;
                            noteLinker.SetOnClickCallback(OnClick_NoteLinker);
                            noteLinker.SetPosition(beginNoteButton.rectTransform.position, lastNoteButton.rectTransform.position);
                            noteLinker.SetSize(GetNoteCanvasPosition(beginNoteButton.rectTransform.position), GetNoteCanvasPosition(lastNoteButton.rectTransform.position));
                            noteLinkerList.Add(noteLinker);
                        }
                    }
                }
            }
        }

        private bool IsInLinkerList(NotePointer pointer)
        {
            NotePointer tempPointer;
            for (int i = 0; i < noteLinkerList.Count; i++)
            {
                tempPointer = noteLinkerList[i].BeginNote;
                while (!NotePointer.IsNull(tempPointer))
                {
                    if (tempPointer.Equal(pointer)) return true;
                    tempPointer = sheet.GetNote(tempPointer).info.linkNote;
                }
            }
            return false;
        }

        private NotePointer GetLinkLastPointer(NotePointer beginPointer)
        {
            Note<CharacterSound> note = sheet.GetNote(beginPointer);
            NotePointer lastPointer = beginPointer;
            while (!NotePointer.IsNull(note.info.linkNote))
            {
                lastPointer = note.info.linkNote;
                note = sheet.GetNote(note.info.linkNote);
            }

            return lastPointer;
        }

        private void SetNoteButton(int measureID, Note<CharacterSound> note)
        {
            GetNoteButton(measureID, note.id).SetSprite(Asset.GetCharacterAssetByID(note.info.characterID).noteIcon);
            //GetNoteButton(measureID, note.id).SetColor(Asset.GetCharacterAssetByID(note.info.characterID).noteColor);
        }

        private int GetMeasureCount(Song song)
        {
            //"時長*(bpm/60)"計算出來的是4分音符的數量
            //return (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f) / 4f); //四分音符(每小節有4個4分音符=4個4分音符)
            return (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f) / 2f); //八分音符(每小節有4個8分音符=2個4分音符)
            //return (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f)); //十六分音符(每小節有4個16分音符=1個4分音符)
        }

        private NoteLinker GetNotLinkerByPointer(NotePointer beginPointer)
        {
            for (int i = 0; i < noteLinkerList.Count; i++)
            {
                if (noteLinkerList[i].BeginNote.Equal(beginPointer))
                {
                    return noteLinkerList[i];
                }
            }
            return null;
        }

        private void OnNotePointerEnter(NoteButton note)
        {
            if(tempNoteButtonLinkList.Count > 0 && tempNoteButtonLinkList.Find(x => x == note) == null)
            {
                if (tempNoteButtonLinkList.Count >= maxNoteLinkCount) return; //最多4拍
                if (note.NoteID / 4 != tempNoteButtonLinkList[0].NoteID / 4) return; //不在同一列
                if (note.MeasureID < tempNoteButtonLinkList[0].MeasureID) return; //不能往左滑
                if (note.MeasureID == tempNoteButtonLinkList[0].MeasureID && note.NoteID < tempNoteButtonLinkList[0].NoteID) return; //不能往左滑
                if ((note.MeasureID - 1) > tempNoteButtonLinkList[^1].MeasureID) return; //跨太多了，沒連起來
                if (note.MeasureID == tempNoteButtonLinkList[^1].MeasureID && (note.NoteID - 1) != tempNoteButtonLinkList[^1].NoteID) return; //跨太多了，沒連起來
                if (note.MeasureID > tempNoteButtonLinkList[^1].MeasureID && (note.NoteID + 3) != tempNoteButtonLinkList[^1].NoteID) return; //跨太多了，沒連起來
                if (IsNoteExistSound(note)) return; //已經有Note了

                int sameSoundIndex = FindNoteByCharacterID(sheet.measures[note.MeasureID].beats[note.NoteID % 4].notes, SelectedCharacterID);
                if (sameSoundIndex != -1) return; //同一行已經有相同角色了

                tempNoteButtonLinkList.Add(note);
                NoteLinker noteLinker = GetNotLinkerByPointer(tempNoteButtonLinkList[0].ToPointer());
                if (noteLinker == null)
                {
                    noteLinker = Instantiate(noteLinkerPrefab, noteLinkerContainer);
                    noteLinker.SetImage(SelectedCharacter.longNoteIcon);
                    noteLinker.BeginNote = new NotePointer(tempNoteButtonLinkList[0].MeasureID, tempNoteButtonLinkList[0].NoteID);
                    noteLinker.SetOnClickCallback(OnClick_NoteLinker);
                    noteLinkerList.Add(noteLinker);

                    AddLongNote(tempNoteButtonLinkList[0].ToPointer());
                    tempNoteButtonLinkList[0].Show();
                    tempNoteButtonLinkList[0].SetSprite(SelectedCharacter.noteIcon);

                    if(!sheetPlayer.IsPlaying)
                        AudioManager.instance.PlaySound(SelectedCharacter.longSound, GetPitchByNoteID(note.NoteID));
                }

                noteLinker.SetPosition(tempNoteButtonLinkList[0].rectTransform.position, tempNoteButtonLinkList[^1].rectTransform.position);
                noteLinker.SetSize(GetNoteCanvasPosition(tempNoteButtonLinkList[0].rectTransform.position), GetNoteCanvasPosition(tempNoteButtonLinkList[^1].rectTransform.position));

                AddLongNote(tempNoteButtonLinkList[0].ToPointer(), tempNoteButtonLinkList[^1].ToPointer());
                note.Show();
                note.SetSprite(SelectedCharacter.noteIcon);
            }
        }

        private void OnNotePointerExit(NoteButton note)
        {

        }

        private void OnNotePointerDown(NoteButton note)
        {
            //sheetScrollRect.enabled = IsNoteExistSound(note);
            sheetScrollRect.enabled = false;
            if(!IsNoteExistSound(note))
            {
                int sameSoundIndex = FindNoteByCharacterID(sheet.measures[note.MeasureID].beats[note.NoteID % 4].notes, SelectedCharacterID);
                if (sameSoundIndex != -1) return; //同一行已經有相同角色了

                tempNoteButtonLinkList.Clear();
                tempNoteButtonLinkList.Add(note);
            }
        }

        private void OnNotePointerUp(NoteButton note)
        {
            sheetScrollRect.enabled = true;
            tempNoteButtonLinkList.Clear();

            if (!sheetPlayer.IsPlaying)
                AudioManager.instance.StopSound(SelectedCharacter.longSound);
        }

        private void OnClick_Note(NoteButton note)
        {
            if (IsNoteExistSound(note) && sheet.GetNote(note.ToPointer()).info.isLongSound) return;

            AddNote(note.MeasureID, note.NoteID, out int removedNoteID);

            if (removedNoteID != -2)
            {
                if (removedNoteID != -1) GetNoteButton(note.MeasureID, removedNoteID).Disappear();
                if (note.NoteID != removedNoteID)
                {
                    note.Appear();
                    note.SetSprite(SelectedCharacter.noteIcon);
                    //note.SetColor(SelectedCharacter.noteColor);

                    //if (!sheetPlayer.IsPlaying)
                    //AudioManager.instance.PlaySound(soundAssets[buttonSoundsID[selectedSoundButtonID]].audioClip, GetPitchByNoteID(noteID));
                    AudioManager.instance.PlaySound(SelectedCharacter.sound, GetPitchByNoteID(note.NoteID));
                }
            }
        }

        private Vector2 GetNoteCanvasPosition(Vector2 noteWorldPosition)
        {
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(noteWorldPosition);
            viewportPoint = viewportPoint * 2f - new Vector3(1f, 1f, 0);
            return viewportPoint * sheetCanvas.sizeDelta / 2f;
        }

        private bool IsNoteExistSound(NoteButton note)
        {
            List<Note<CharacterSound>> notes = sheet.measures[note.MeasureID].beats[note.NoteID % 4].notes;
            return FindNoteById(notes, note.NoteID) != -1;
        }

        public void OnClick_NoteLinker(NoteLinker noteLinker)
        {
            DeleteNoteLinker(noteLinker.BeginNote);
        }    

        private void AddLongNote(NotePointer beginNote, NotePointer newNote = null)
        {
            if (NotePointer.IsNull(newNote))
                sheet.AddNote(beginNote.measureID, beginNote.noteID, new CharacterSound(SelectedCharacterID, GetPitchByNoteID(beginNote.noteID), true));
            else
            {
                Note<CharacterSound> note = sheet.GetNote(beginNote);
                while (!NotePointer.IsNull(note.info.linkNote)) note = sheet.GetNote(note.info.linkNote);

                note.info.linkNote = new NotePointer(newNote.measureID, newNote.noteID);
                sheet.AddNote(newNote.measureID, newNote.noteID, new CharacterSound(SelectedCharacterID, GetPitchByNoteID(newNote.noteID), true));
            }
        }

        private void DeleteNoteLinker(NotePointer beginNote)
        {
            NotePointer currentNote = beginNote;
            NotePointer nextNote = sheet.GetNote(currentNote).info.linkNote;

            while(!NotePointer.IsNull(nextNote))
            {
                GetNoteButton(currentNote.measureID, currentNote.noteID).Hide();
                sheet.RemoveNote(currentNote);

                currentNote = nextNote; 
                nextNote = sheet.GetNote(currentNote).info.linkNote;
            }
            GetNoteButton(currentNote.measureID, currentNote.noteID).Hide();
            sheet.RemoveNote(currentNote);

            for (int i = 0; i < noteLinkerList.Count; i++)
            {
                if (noteLinkerList[i].BeginNote.Equal(beginNote))
                {
                    Destroy(noteLinkerList[i].gameObject);
                    noteLinkerList.RemoveAt(i);
                    break;
                }
            }
        }

        private void AddNote(int measureID, int noteID, out int removedNoteID)
        {
            List<Note<CharacterSound>> notes = sheet.measures[measureID].beats[noteID % 4].notes;

            removedNoteID = -1;
            int sameIdIndex = FindNoteById(notes, noteID);
            int sameSoundIndex = FindNoteByCharacterID(notes, SelectedCharacterID);

            if (sameIdIndex == -1) //點擊的格子是空的
            {
                if (sameSoundIndex == -1) //該beat尚未有這個樂器 => 新增
                {
                    sheet.AddNote(measureID, noteID, new CharacterSound(SelectedCharacterID, GetPitchByNoteID(noteID)));
                }
                else //該beat其他格子已有這個樂器 => 刪掉該格，新增這格
                {
                    if (!notes[sameSoundIndex].info.isLongSound)
                    {
                        removedNoteID = notes[sameSoundIndex].id;
                        sheet.RemoveNote(measureID, removedNoteID);
                        sheet.AddNote(measureID, noteID, new CharacterSound(SelectedCharacterID, GetPitchByNoteID(noteID)));
                    }
                    else removedNoteID = -2;
                }
            }
            else //點擊的格子已有樂器
            {
                if (notes[sameIdIndex].info.characterID == SelectedCharacterID) //與該格樂器相同 => 刪除
                {
                    removedNoteID = notes[sameIdIndex].id;
                    sheet.RemoveNote(measureID, removedNoteID);
                }
                else //與該格樂器不同 => 替換該格樂器
                {
                    if(sameSoundIndex != -1) //該beat其他格子有相同樂器
                    {
                        if (!notes[sameSoundIndex].info.isLongSound)
                        {
                            notes[sameIdIndex].info.characterID = SelectedCharacterID;

                            removedNoteID = notes[sameSoundIndex].id;
                            sheet.RemoveNote(measureID, removedNoteID);
                        }
                        else removedNoteID = -2;
                    }
                    else notes[sameIdIndex].info.characterID = SelectedCharacterID;
                }
            }
        }

        private NoteButton GetNoteButton(int measureID, int noteID)
        {
            return measureViews[measureID].GetNoteButton(noteID);
        }

        private void ReplaceNoteCharacterID(int oldCharacterID, int newCharacterID)
        {
            Beat<CharacterSound> beat;
            Note<CharacterSound> note;
            NoteLinker linker;
            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        if (note.info.characterID == oldCharacterID)
                        {
                            note.info.characterID = newCharacterID;
                            GetNoteButton(measureIndex, note.id).SetSprite(Asset.GetCharacterAssetByID(newCharacterID).noteIcon);

                            if(note.info.isLongSound)
                            {
                                linker = GetNotLinkerByPointer(new NotePointer(measureIndex, note.id));
                                if (linker != null)
                                {
                                    linker.SetImage(Asset.GetCharacterAssetByID(newCharacterID).longNoteIcon);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RemoveNotesByCharacterID(int characterID)
        {
            Beat<CharacterSound> beat;
            Note<CharacterSound> note;
            NoteLinker linker;
            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = beat.notes.Count - 1; noteIndex >= 0; noteIndex--)
                    {
                        note = beat.notes[noteIndex];
                        if (note.info.characterID == characterID)
                        {
                            GetNoteButton(measureIndex, note.id).Hide();

                            if (note.info.isLongSound)
                            {
                                linker = GetNotLinkerByPointer(new NotePointer(measureIndex, note.id));
                                if (linker != null)
                                {
                                    int index = noteLinkerList.FindIndex(x => x.BeginNote == linker.BeginNote);
                                    Destroy(linker.gameObject);
                                    noteLinkerList.RemoveAt(index);
                                }
                            }

                            beat.RemoveNote(note.id);
                        }
                    }
                }
            }
        }

        private int FindNoteById(List<Note<CharacterSound>> notes, int noteID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].id == noteID)
                    return i;

            return -1;
        }

        private int FindNoteByCharacterID(List<Note<CharacterSound>> notes, int characterID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].info.characterID == characterID)
                    return i;

            return -1;
        }

        private float GetPitchByNoteID(int id)
        {
            return (id / 4) switch
            {
                0 => 1, //C
                1 => Mathf.Pow(pitchScale, 2), // D
                2 => Mathf.Pow(pitchScale, 4), // E
                3 => Mathf.Pow(pitchScale, 5), // F
                4 => Mathf.Pow(pitchScale, 7), // G
                5 => Mathf.Pow(pitchScale, 9), // A
                6 => Mathf.Pow(pitchScale, 11), // B
                _ => 1
            };
        }
        #endregion

        #region Sheet Play
        public void OnClick_PlaySheet()
        {
            sheetPlayer.Play(sheet);
            songCdAnimator.Play("Playing", 0, 0);
            startPlaySheetButton.SetActive(false);
            stopPlaySheetButton.SetActive(true);
        }

        public void OnClick_StopPlaySheet()
        {
            sheetPlayer.Stop();
            songCdAnimator.Play("Idle", 0, 0);
            startPlaySheetButton.SetActive(true);
            stopPlaySheetButton.SetActive(false);
        }

        public void OnPlayedNoteButton(int measureID, int noteID)
        {            
            GetNoteButton(measureID, noteID).OnPlayed();
        }

        public void OnPlayedNoteLinker(NotePointer beginNote)
        {
            NoteLinker longNote = GetNotLinkerByPointer(beginNote);
            if (longNote != null)
            {
                longNote.OnLongPlayed();
                Note<CharacterSound> note = sheet.GetNote(beginNote);
                GetNoteButton(beginNote.measureID, beginNote.noteID).OnLongPlayed();
                while (!NotePointer.IsNull(note.info.linkNote))
                {
                    GetNoteButton(note.info.linkNote.measureID, note.info.linkNote.noteID).OnLongPlayed();
                    note = sheet.GetNote(note.info.linkNote);
                }
            }
        }

        public void OnPlayedSoundButton(int characterID)
        {
            GetSoundButtonByCharacterID(characterID).OnPlayed();
        }

        private SoundButton GetSoundButtonByCharacterID(int charatcerID)
        {
            for(int i = 0; i < soundButtons.Count; i++)
            {
                if (soundButtons[i].characterID == charatcerID) return soundButtons[i];
            }
            return null;
        }
        #endregion

        #region Data
        private EM_Data Data
        {
            get
            {
                EM_Data data = new();
                for (int i = 0; i < soundButtons.Count; i++) data.charactersID.Add(soundButtons[i].characterID);
                data.songID = currentSongID;
                data.sheet = sheet;
                return data;
            }
        }

        private void SetDataKey() { dataController.SetKey(GameSettings.EmotionMusicDataPath); }

        private void DataControllerInitialize()
        {
            SetDataKey();
            dataController.SetActions(SaveData, LoadData, DeleteData, RenameData, ExportData, ImportData);
        }

        public void SaveByRandomName() 
        {
            string fileName = "";
            fileName = "File" + DateTime.Now.ToString("yyyyMMdd");
            for (int i = 0; i < 4; i++) fileName += (char)UnityEngine.Random.Range('0', '9' + 1);

            dataController.SaveData(Data, fileName, false); 
        }
        public void AutoSave() { dataController.SaveData(Data, "AutoSaveFile"); }

        private void SaveData() { dataController.SaveData(Data); }
        private void LoadData()
        {
            OnClick_StopPlaySheet();

            EM_Data data = dataController.LoadSelectedData<EM_Data>();

            emotionMusicController.OnDataLoad(data);

            selectedSoundButtonIndex = 0;
            SetSoundButtons(data.charactersID.ToArray());
            background.sprite = MainCharacter.musicBackground;

            currentSongID = data.songID;
            songButtonContainer.GetComponent<RectTransform>().anchoredPosition *= new Vector2(0, 1);
            GenerateSongButtons(MainCharacter);
            SetSongInfo(CurrentSong);

            StartCoroutine(SetSheet(CurrentSong, data.sheet));
            sheetPlayer.InitializeBySong(CurrentSong);
            StartCoroutine(WaitForOneFrame(OnClick_PlaySheet));
        }

        private void DeleteData() { dataController.DeleteSelectedData<EM_Data>(); }
        private void RenameData() { dataController.RenameSelectedData<EM_Data>(); }
        private void ExportData() { dataController.ExportData(Data); }
        private void ImportData()
        {
            OnClick_StopPlaySheet();

            EM_Data data = dataController.ImportData<EM_Data>();

            emotionMusicController.OnDataLoad(data);

            selectedSoundButtonIndex = 0;
            SetSoundButtons(data.charactersID.ToArray());
            background.sprite = MainCharacter.musicBackground;

            currentSongID = data.songID;
            songButtonContainer.GetComponent<RectTransform>().anchoredPosition *= new Vector2(0, 1);
            GenerateSongButtons(MainCharacter);
            SetSongInfo(CurrentSong);

            StartCoroutine(SetSheet(CurrentSong, data.sheet));
            sheetPlayer.InitializeBySong(CurrentSong);
            StartCoroutine(WaitForOneFrame(OnClick_PlaySheet));
        }
        #endregion

        #region Dance
        public void OnClick_PlayDance()
        {
            int[] charactersID = new int[5];
            for (int i = 0; i < charactersID.Length; i++) charactersID[i] = soundButtons[i].characterID;

            OnClick_StopPlaySheet();
            emotionMusicController.ChangeGameState(MusicGameState.Dance);
            dancePlayer.Play(CurrentSong, sheet, charactersID);
        }
        #endregion
    }
}