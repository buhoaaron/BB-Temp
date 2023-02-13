using System.Collections;
using UnityEngine;
using CustomAudio;

namespace Barnabus.MusicGarden.Base
{
    public enum TempoPerBeat { Quarter, Eighth, Sixteenth }

    public abstract class SheetPlayer<T> : MonoBehaviour
    {
        public bool IsPlaying { get { return !isBreak; } }

        [SerializeField]
        protected bool isLoop = true;
        [SerializeField]
        protected TempoPerBeat tempo;

        [Header("View")]
        [SerializeField]
        protected RectTransform scrollView;
        [SerializeField]
        protected RectTransform sheetView;

        [Header("Line")]
        [SerializeField]
        protected RectTransform line;
        [SerializeField]
        protected float lineSpeed = 2.5f;

        protected SoundAssetList soundAssets;
        protected bool isBreak = true;
        protected float bpm;
        protected float linePosition;
        protected float lineSpeedMultiple;

        protected void Start()
        {
            ResetLine();
            line.gameObject.SetActive(false);
        }

        public void SoundAssetList(SoundAssetList soundAssetList)
        {
            soundAssets = soundAssetList;
        }

        public void Play(Sheet<T> sheet)
        {
            isBreak = false;
            bpm = sheet.bpm;
            lineSpeedMultiple = tempo switch
            {
                TempoPerBeat.Quarter => 1,
                TempoPerBeat.Eighth => 2,
                TempoPerBeat.Sixteenth => 4,
                _ => 1,
            };
            StartCoroutine(PlaySheet(sheet));
        }

        public void Stop()
        {
            isBreak = true;
        }

        protected void Update()
        {
            if (IsPlaying) LineMove();
        }

        protected void LineMove()
        {
            linePosition += bpm * lineSpeed * lineSpeedMultiple * Time.deltaTime;
            SetLinePosition();

            if (!Input.GetMouseButton(0))
            {
                if (line.anchoredPosition.x > scrollView.rect.width)
                {
                    sheetView.anchoredPosition -= new Vector2(scrollView.rect.width, 0);
                    SetLinePosition();
                }
                else if (line.anchoredPosition.x < 0)
                {
                    sheetView.anchoredPosition += new Vector2(scrollView.rect.width, 0);
                    SetLinePosition();
                }
            }
        }

        protected void ResetLine()
        {
            linePosition = 0;
            SetLinePosition();
        }

        protected void SetLinePosition()
        {
            line.anchoredPosition = new Vector2(linePosition + sheetView.anchoredPosition.x, 0);
        }

        protected abstract void NotePlay(Note<T> note);

        protected IEnumerator PlaySheet(Sheet<T> sheet)
        {
            WaitForSeconds interval = tempo switch
            {
                TempoPerBeat.Quarter => new WaitForSeconds(60f / (float)sheet.bpm),
                TempoPerBeat.Eighth => new WaitForSeconds((60f / (float)sheet.bpm) / 2f),
                TempoPerBeat.Sixteenth => new WaitForSeconds((60f / (float)sheet.bpm) / 4f),
                _ => new WaitForSeconds(60f / (float)sheet.bpm),
            };

            Beat<T> beat;
            Note<T> note;

            line.gameObject.SetActive(true);
            while (true)
            {
                ResetLine();

                for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
                {
                    for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                    {
                        if (isBreak) goto Break;

                        beat = sheet.measures[measureIndex].beats[beatIndex];

                        for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                        {
                            note = beat.notes[noteIndex];
                            NotePlay(note);
                        }

                        yield return interval;
                    }
                }

                if (!isLoop)
                {
                    OnAllPlayEnd();
                    isBreak = true;
                }
            }

        Break:
            {
                AudioManager.instance.StopAllSound();
                ResetLine();
                line.gameObject.SetActive(false);
                OnPlayEnd();
            }
        }

        protected virtual void OnPlayEnd()
        {

        }

        protected virtual void OnAllPlayEnd()
        {

        }
    }
}