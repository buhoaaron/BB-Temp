using CustomAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionMusic
{
    public class EM_DancePlayer : MonoBehaviour
    {
        [SerializeField]
        private EmotionMusicController emotionMusicController;
        [SerializeField]
        private TempoPerBeat tempo = TempoPerBeat.Eighth;
        [SerializeField]
        private List<SpineUIAnimator> characters;

        private EmotionMusicAsset Asset { get { return emotionMusicController.Asset; } }

        private bool isBreak = true;
        private float deltaTimeSinceSheetStart;
        private int[] charactersID;

        private Sheet<CharacterSound> sheet;
        private float beatInterval;
        List<NotePointer> playedLongNotes = new List<NotePointer>();

        private void Update()
        {
            if(!isBreak) deltaTimeSinceSheetStart += Time.deltaTime;
        }

        public void Play(Song song, Sheet<CharacterSound> sheet, int[] charactersID)
        {
            InitializeCharacters(charactersID);
            StartCoroutine(WaitForPlay(0.5f, sheet, song));
        }

        public void Stop()
        {
            isBreak = true;
            AudioManager.instance.StopAllSound();
            AudioManager.instance.StopBGM();
            StopAllCoroutines();
            for (int i = 0; i < characters.Count; i++) characters[i].StopAllCoroutines();
        }

        private void InitializeCharacters(int[] charactersID)
        {
            Character character;
            for (int i = 0; i < characters.Count; i++)
            {
                character = Asset.GetCharacterAssetByID(charactersID[i]);
                if(character)
                {
                    characters[i].gameObject.SetActive(true);
                    characters[i].SetAsset(character.SkeletonDataAsset);
                    characters[i].Play(character.idleAnimationName, true);
                }
                else
                {
                    characters[i].gameObject.SetActive(false);
                }
            }
            this.charactersID = charactersID;
        }

        private IEnumerator WaitForPlay(float delayTime, Sheet<CharacterSound> sheet, Song song)
        {
            yield return new WaitForSeconds(delayTime);

            isBreak = false;
            StartCoroutine(PlaySheet(sheet, song));
        }

        private IEnumerator PlaySheet(Sheet<CharacterSound> sheet, Song song)
        {
            this.sheet = sheet;

            beatInterval = tempo switch
            {
                TempoPerBeat.Quarter => 60f / (float)song.bpm,
                TempoPerBeat.Eighth => (60f / (float)song.bpm) / 2f,
                TempoPerBeat.Sixteenth => (60f / (float)song.bpm) / 4f,
                _ => (60f / (float)song.bpm) / 2f,
            };

            float timeCorrection;

            Beat<CharacterSound> beat;
            Note<CharacterSound> note;
            NotePointer pointer;

            deltaTimeSinceSheetStart = 0;
            AudioManager.instance.PlayBGM(song.audioClip, false); 
            playedLongNotes = new List<NotePointer>();

            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    if (isBreak) goto Break;

                    timeCorrection = GetPreciseBeatTime(measureIndex, beatIndex, beatInterval) - deltaTimeSinceSheetStart;
                    if (timeCorrection > 0) timeCorrection = 0;

                    beat = sheet.measures[measureIndex].beats[beatIndex];

                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        pointer = new NotePointer(measureIndex, note.id);

                        if (note.info.isLongSound)
                        {
                            if (playedLongNotes.Find(x => x.Equal(pointer)) == null) PlayLongNote(pointer);
                        }
                        else NotePlay(note);

                        CharacterPlay(Asset.GetCharacterAssetByID(note.info.characterID));
                    }

                    yield return new WaitForSecondsRealtime(beatInterval + timeCorrection);
                }
            }

            OnAllPlayEnd(sheet, song);
            //isBreak = true;
            Stop();

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

        private void NotePlay(Note<CharacterSound> note)
        {
            AudioManager.instance.PlaySound(Asset.GetCharacterAssetByID(note.info.characterID).sound, note.info.pitch);
        }

        private void PlayLongNote(NotePointer beginNote)
        {
            Note<CharacterSound> note = sheet.GetNote(beginNote);
            AudioClip clip = Asset.GetCharacterAssetByID(note.info.characterID).longSound;
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

        private void CharacterPlay(Character character)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                if(charactersID[i] == character.id)
                {
                    //characters[i].Play(character.danceAnimationName, false, () => characters[i].Play(character.idleAnimationName));
                    PlayCharacterDance(characters[i], character);
                }
            }
        }

        private void PlayCharacterDance(SpineUIAnimator character, Character characterAsset)
        {
            character.Play(characterAsset.danceAnimationName, false, () => character.Play(characterAsset.idleAnimationName));
        }

        private float GetPreciseBeatTime(float measureIndex, float beatIndex, float beatInterval)
        {
            return measureIndex * beatInterval * 4f + beatIndex * beatInterval;
        }

        private void OnAllPlayEnd(Sheet<CharacterSound> sheet, Song song)
        {
            emotionMusicController.ShowEndDialog();
            //Notifier.ShowConfirmView("Dance End", "Do you want to play again?", () => Play(song, sheet, charactersID), () => emotionMusicController.ShowEndDialog());
        }
    }
}