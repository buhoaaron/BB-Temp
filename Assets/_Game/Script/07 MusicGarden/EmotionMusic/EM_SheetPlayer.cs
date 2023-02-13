using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomAudio;
using UnityEngine.UI;

namespace Barnabus.EmotionMusic
{
    public enum TempoPerBeat { Quarter, Eighth, Sixteenth }

    public class EM_SheetPlayer : MonoBehaviour
    {
        public bool IsPlaying { get { return !isBreak; } }

        [SerializeField]
        private EM_SheetController sheetController;
        [SerializeField]
        private bool isLoop = true;
        [SerializeField]
        private TempoPerBeat tempo = TempoPerBeat.Eighth;

        [Header("View")]
        [SerializeField]
        private RectTransform scrollView;
        [SerializeField]
        private RectTransform sheetView;

        [Header("Line")]
        [SerializeField]
        private RectTransform line;

        [Header("Progress Slider")]
        [SerializeField]
        private Slider songProgressSlider;

        private bool isBreak = true;
        private float linePosition;

        private Song song;
        private Sheet<CharacterSound> sheet;
        private float beatInterval;
        private float sheetTimeLength = 0;
        private float deltaTimeSinceSheetStart;

        List<NotePointer> playedLongNotes = new List<NotePointer>();

        private void Update()
        {
            if (IsPlaying)
            {
                deltaTimeSinceSheetStart += Time.deltaTime;
                SetSliderValue(deltaTimeSinceSheetStart / sheetTimeLength);
            }
            else
            {
                SetLinePosition(songProgressSlider.value, false);
            }
        }

        public void InitializeBySong(Song song)
        {
            this.song = song;
            beatInterval = tempo switch
            {
                TempoPerBeat.Quarter => 60f / (float)song.bpm,
                TempoPerBeat.Eighth => (60f / (float)song.bpm) / 2f,
                TempoPerBeat.Sixteenth => (60f / (float)song.bpm) / 4f,
                _ => (60f / (float)song.bpm) / 2f,
            };
            sheetTimeLength = (float)GetMeasureCount(song) * 4f * beatInterval;
            SetSliderValue(0);
        }

        public void Play(Sheet<CharacterSound> sheet)
        {
            isBreak = false;
            StopAllCoroutines();
            StartCoroutine(PlaySheet(sheet, songProgressSlider.value));
        }

        public void OnSliderValueChanged()
        {
            SetLinePosition(songProgressSlider.value, !IsPlaying);
        }

        private void SetSliderValue(float progress)
        {
            songProgressSlider.value = progress;
        }

        public void Stop()
        {
            isBreak = true;
            AudioManager.instance.StopAllSound();
            if (!sheetController.IsPreviewingSong) AudioManager.instance.StopBGM();
            OnPlayEnd();
            StopAllCoroutines();
            //SetSliderValue(0);
        }

        private void SetLinePosition(float progress, bool autoMoveSheet = true)
        {
            linePosition = sheetView.sizeDelta.x * progress;
            SetLineUiPosition(autoMoveSheet);
        }

        private void SetLineUiPosition(bool autoMoveSheet)
        {
            line.anchoredPosition = new Vector2(linePosition + sheetView.anchoredPosition.x, 0);
            if (autoMoveSheet)
            {
                if (line.anchoredPosition.x > scrollView.rect.width + 1)
                {
                    sheetView.anchoredPosition -= new Vector2(scrollView.rect.width, 0);
                    line.anchoredPosition -= new Vector2(scrollView.rect.width, 0);
                }
                else if (line.anchoredPosition.x < -1)
                {
                    sheetView.anchoredPosition += new Vector2(scrollView.rect.width, 0);
                    line.anchoredPosition += new Vector2(scrollView.rect.width, 0);
                }
            }
        }

        private void NotePlay(Note<CharacterSound> note)
        {
            AudioManager.instance.PlaySound(sheetController.Asset.GetCharacterAssetByID(note.info.characterID).sound, note.info.pitch);
        }

        private void PlayLongNote(NotePointer beginNote)
        {
            Note<CharacterSound> note = sheet.GetNote(beginNote);
            AudioClip clip = sheetController.Asset.GetCharacterAssetByID(note.info.characterID).longSound;
            AudioManager.instance.PlaySound(clip, note.info.pitch);
            StartCoroutine(StopLongSound(clip, beatInterval * GetLinkCount(beginNote) - 0.1f));

            MarkNotePlayed(beginNote);
        }

        private IEnumerator StopLongSound(AudioClip clip, float delay)
        {
            yield return new WaitForSeconds(delay);

            AudioManager.instance.StopSound(clip);
        }

        private float GetLinkCount(NotePointer beginNote)
        {
            int count = 1;
            Note<CharacterSound> note = sheet.GetNote(beginNote);
            while (!NotePointer.IsNull(note.info.linkNote))
            {
                count++;
                note = sheet.GetNote(note.info.linkNote);
            }
            return count;
        }

        private void MarkNotePlayed(NotePointer beginNote)
        {
            Note<CharacterSound> note = sheet.GetNote(beginNote);
            playedLongNotes.Add(new NotePointer(beginNote.measureID, beginNote.noteID));
            while (!NotePointer.IsNull(note.info.linkNote))
            {
                playedLongNotes.Add(new NotePointer(note.info.linkNote.measureID, note.info.linkNote.noteID));
                note = sheet.GetNote(note.info.linkNote);
            }
        }

        private IEnumerator PlaySheet(Sheet<CharacterSound> sheet, float progress)
        {
            this.sheet = sheet;

            bool isFirstCycle = true;
            float songBeginTime;
            float timeCorrection;

            int beatBeginPosition;
            int measureBeginIndex, beatBeginIndex;

            Beat<CharacterSound> beat;
            Note<CharacterSound> note;
            NotePointer pointer;

            while (true)
            {
                playedLongNotes = new List<NotePointer>();

                if (isFirstCycle)
                {
                    beatBeginPosition = Mathf.RoundToInt(sheetTimeLength * progress / beatInterval);
                    songBeginTime = (float)beatBeginPosition * beatInterval;
                    measureBeginIndex = beatBeginPosition / 4;
                    beatBeginIndex = beatBeginPosition % 4;
                }
                else
                {
                    songBeginTime = 0;
                    measureBeginIndex = 0;
                    beatBeginIndex = 0;
                }

                AudioManager.instance.PlayBGM(song.audioClip, false, songBeginTime);

                deltaTimeSinceSheetStart = songBeginTime;
                //SetLinePosition(progress);

                for (int measureIndex = measureBeginIndex; measureIndex < sheet.measures.Count; measureIndex++)
                {
                    for (int beatIndex = ((measureIndex == measureBeginIndex) ? beatBeginIndex : 0); beatIndex < 4; beatIndex++)
                    {
                        if (isBreak) goto Break;

                        timeCorrection = GetPreciseBeatTime(measureIndex, beatIndex) - deltaTimeSinceSheetStart;
                        if (timeCorrection > 0) timeCorrection = 0;

                        beat = sheet.measures[measureIndex].beats[beatIndex];

                        for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                        {
                            note = beat.notes[noteIndex];
                            pointer = new NotePointer(measureIndex, note.id);

                            if (note.info.isLongSound) 
                            {
                                if(playedLongNotes.Find(x => x.Equal(pointer)) == null) PlayLongNote(pointer);
                            }
                            else NotePlay(note);

                            if(note.info.isLongSound) sheetController.OnPlayedNoteLinker(pointer);
                            else sheetController.OnPlayedNoteButton(measureIndex, note.id);

                            sheetController.OnPlayedSoundButton(note.info.characterID);
                        }

                        yield return new WaitForSecondsRealtime(beatInterval + timeCorrection);
                    }
                }

                if (!isLoop)
                {
                    OnAllPlayEnd();
                    //isBreak = true;
                    Stop();
                }

                isFirstCycle = false;
            }

        Break:
            {
                /*
                AudioManager.instance.StopAllSound();
                if (!sheetController.IsPreviewingSong) AudioManager.instance.StopBGM();
                ResetLine();
                OnPlayEnd();
                */
            }
        }

        private float GetPreciseBeatTime(float measureIndex, float beatIndex)
        {
            return measureIndex * beatInterval * 4f + beatIndex * beatInterval;
        }

        private int GetMeasureCount(Song song)
        {
            //"*(bpm/60)"璸衡ㄓ琌4だ才计秖
            return tempo switch
            {
                TempoPerBeat.Quarter => (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f) / 4f), //だ才(–竊Τ44だ才=44だ才)
                TempoPerBeat.Eighth => (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f) / 2f), //だ才(–竊Τ48だ才=24だ才)
                TempoPerBeat.Sixteenth => (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f)), //せだ才(–竊Τ416だ才=14だ才)
                _ => (int)Mathf.Ceil(song.audioClip.length * (song.bpm / 60f) / 2f),
            };
        }

        private void OnPlayEnd()
        {

        }

        private void OnAllPlayEnd()
        {

        }
    }
}