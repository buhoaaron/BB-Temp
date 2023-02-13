using Barnabus.MusicGarden.Base;
using CustomAudio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.MusicGarden.EmotionSong
{
    public class DancePlayer : SheetPlayer<BabySound>
    {
        [Header("Canvas")]
        [SerializeField]
        private GameObject danceCanvas;

        [Header("DanceMeasure")]
        [SerializeField]
        private MeasureView measureViewPrefab;
        [SerializeField]
        private Transform measureContainer;

        [Header("Character")]
        [SerializeField]
        private List<Image> characters;
        [SerializeField]
        private List<Animator> characterAnimators;

        private Action autoSave;
        private List<MeasureView> measureViews = new List<MeasureView>();

        public void SetSoundAssetList(SoundAssetList soundAssetList) { soundAssets = soundAssetList; }
        public void SetAutoSaveAction(Action autoSaveAction) { autoSave = autoSaveAction; }

        public void PlayDance(Sheet<BabySound> sheet, int[] selectedSoundsId)
        {
            danceCanvas.SetActive(true);
            line.gameObject.SetActive(false);
            SetCharacterSprite(selectedSoundsId);
            LoadDanceSheet(sheet, selectedSoundsId);
            StartCoroutine(WaitForPlay(sheet, 0.5f));
        }

        public void OnClick_StopDance()
        {
            Stop();
            StopAllCoroutines();
            danceCanvas.SetActive(false);
        }

        private IEnumerator WaitForPlay(Sheet<BabySound> sheet, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            Play(sheet);
        }

        private void SetCharacterSprite(int[] selectedSoundsId)
        {
            for (int i = 1; i < selectedSoundsId.Length; i++)
            {
                characters[i - 1].sprite = soundAssets[selectedSoundsId[i]].buttonSprite;
            }
        }

        private void LoadDanceSheet(Sheet<BabySound> sheet, int[] selectedSoundsId)
        {
            for (int i = 0; i < measureViews.Count; i++) Destroy(measureViews[i].gameObject);
            measureViews = new List<MeasureView>();

            MeasureView newMeasureView;

            for (int i = 0; i < sheet.measures.Count; i++)
            {
                newMeasureView = Instantiate(measureViewPrefab, measureContainer);
                newMeasureView.Initialize(i, 16, null);
                measureViews.Add(newMeasureView);
            }

            //Generate Notes
            int level;
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

                        level = GetLevelBySoundId(note.info.soundID, selectedSoundsId);

                        if(level != -1)
                        {
                            GetDanceNoteButton(measureIndex, level * 4 + note.id % 4).Appear();
                            GetDanceNoteButton(measureIndex, level * 4 + note.id % 4).SetImage(GetNoteSprite(note.info.soundID));
                        }
                    }
                }
            }
        }

        private int GetLevelBySoundId(int soundId, int[] selectedSoundsId)
        {
            for(int i = 1; i < selectedSoundsId.Length; i++)
            {
                if (selectedSoundsId[i] == soundId) return (i - 1);
            }
            return -1;
        }

        private NoteButton GetDanceNoteButton(int measureID, int noteID)
        {
            return measureViews[measureID].GetNoteButton(noteID);
        }

        private Sprite GetNoteSprite(int soundId)
        {
            return soundAssets[soundId].noteSprite;
        }

        protected override void NotePlay(Note<BabySound> note)
        {
            AudioManager.instance.PlaySound(soundAssets[note.info.soundID].audioClip, note.info.pitch);
            if(note.info.buttonID != 0) characterAnimators[note.info.buttonID - 1].Play("Play", 0, 0);
        }

        protected override void OnAllPlayEnd()
        {
            base.OnAllPlayEnd();

            DialogController.ShowDialog("This game is emotion song.", () => AwardController.ShowAward(3, 10,
                                                                      () => SceneTransit.LoadScene("MainScene"),
                                                                      () => OnClick_StopDance(),
                                                                      () => ConfirmAutoSaveBeforeLeaving()));
        }

        private void ConfirmAutoSaveBeforeLeaving()
        {
            Notifier.ShowConfirmView("Warning", "Do you want to save data before leaving?",
                () =>
                {
                    autoSave?.Invoke();
                    SceneTransit.LoadSceneAsync("EmotionSong");
                },
                () => SceneTransit.LoadSceneAsync("EmotionSong"));
        }
    }
}