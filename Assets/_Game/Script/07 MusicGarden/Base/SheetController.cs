using CustomAudio;
using CustomAudio.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataStream;

namespace Barnabus.MusicGarden.Base
{
    public abstract class SheetController<T> : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        protected DataController dataController;

        [Header("Settings")]
        [SerializeField]
        protected int minMeasureCount = 2;
        [SerializeField]
        protected SoundAssetList soundAssets;

        [Header("SheetPlayer")]
        [SerializeField]
        protected SheetPlayer<T> sheetPlayer;
        [SerializeField]
        protected Image playButton;
        [SerializeField]
        protected Sprite playSprite;
        [SerializeField]
        protected Sprite stopSprite;

        [Header("Measure")]
        [SerializeField]
        protected MeasureView measureViewPrefab;
        [SerializeField]
        protected Transform measureContainer;
        [SerializeField]
        protected Transform modifyButtonGroup;
        [SerializeField]
        protected GameObject removeMeasureButton;

        protected Sheet<T> sheet = new();
        protected List<MeasureView> measureViews;

        protected void Start()
        {
            Initialize();
            DataControllerInitialize();
        }

        public virtual void Initialize()
        {
            sheetPlayer.SoundAssetList(soundAssets);

            measureViews = new List<MeasureView>();
            for (int i = 0; i < minMeasureCount; i++) AddMeasure();
            removeMeasureButton.SetActive(false);
        }

        //Data
        protected abstract void SetDataKey();

        protected void DataControllerInitialize()
        {
            SetDataKey();
            dataController.SetActions(SaveData, LoadData, DeleteData, RenameData, ExportData, ImportData);
        }

        protected virtual void SaveData() { dataController.SaveData<Sheet<T>>(sheet); }
        protected virtual void LoadData()
        {
            StopSheetPlay();
            StopAllCoroutines();

            sheet = dataController.LoadSelectedData<Sheet<T>>();
            StartCoroutine(UpdateSheetView());
        }

        protected virtual void DeleteData() { dataController.DeleteSelectedData<Sheet<T>>(); }
        protected virtual void RenameData() { dataController.RenameSelectedData<Sheet<T>>(); }
        protected virtual void ExportData() { dataController.ExportData<Sheet<T>>(sheet); }
        protected virtual void ImportData()
        {
            StopSheetPlay();
            StopAllCoroutines();

            sheet = dataController.ImportData<Sheet<T>>();
            StartCoroutine(UpdateSheetView());
        }

        // Measure
        public virtual void AddMeasure()
        {
            StopSheetPlay();

            MeasureView newMeasureView = Instantiate(measureViewPrefab, measureContainer);
            newMeasureView.Initialize(measureViews.Count, GetNoteAmountPerMeasure(), ClickedNote);
            measureViews.Add(newMeasureView);
            sheet.AddMeasure();

            modifyButtonGroup.SetSiblingIndex(-1);
            if (sheet.measures.Count > minMeasureCount) removeMeasureButton.SetActive(true);
        }

        public virtual void RemoveLastMeasure()
        {
            StopSheetPlay();

            Destroy(measureViews[measureViews.Count - 1].gameObject);
            measureViews.RemoveAt(measureViews.Count - 1);

            if (measureViews.Count == minMeasureCount) removeMeasureButton.SetActive(false);

            sheet.RemoveLastMeasure();
        }

        protected abstract int GetNoteAmountPerMeasure();

        //Note
        public abstract void ClickedNote(int measureID, int noteID);

        protected abstract void AddNote(int measureID, int noteID, out int removedNoteID);

        private NoteButton GetNoteButton(int measureID, int noteID)
        {
            return measureViews[measureID].GetNoteButton(noteID);
        }

        protected abstract void SetNoteButton(int measureID, Note<T> note);

        //Sheet
        public virtual void OnClick_ClearSheetView()
        {
            StopSheetPlay();
            StartCoroutine(ClearSheetView());
        }

        public virtual void OnClick_PlaySheet()
        {
            if (sheetPlayer.IsPlaying) StopSheetPlay();
            else PlaySheet();
        }

        protected virtual void PlaySheet()
        {
            sheetPlayer.Play(sheet);
            playButton.sprite = stopSprite;
        }

        protected virtual void StopSheetPlay()
        {
            sheetPlayer.Stop();
            playButton.sprite = playSprite;
        }

        protected IEnumerator UpdateSheetView()
        {
            //Clear MeasureViews
            for (int i = 0; i < measureViews.Count; i++) Destroy(measureViews[i].gameObject);
            measureViews = new List<MeasureView>();

            //Generate MeasureViews
            MeasureView newMeasureView;

            for (int i = 0; i < sheet.measures.Count; i++)
            {
                newMeasureView = Instantiate(measureViewPrefab, measureContainer);
                newMeasureView.Initialize(i, GetNoteAmountPerMeasure(), ClickedNote);

                measureViews.Add(newMeasureView);
            }
            modifyButtonGroup.SetSiblingIndex(-1);
            removeMeasureButton.SetActive(sheet.measures.Count > minMeasureCount);

            //Generate Notes
            Beat<T> beat;
            Note<T> note;

            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        GetNoteButton(measureIndex, note.id).Appear();
                        SetNoteButton(measureIndex, note);
                    }
                    //yield return new WaitForSeconds(0.05f);
                }
            }
            yield return null;
        }

        protected IEnumerator ClearSheetView()
        {
            Beat<T> beat;
            Note<T> note;

            for (int measureIndex = 0; measureIndex < sheet.measures.Count; measureIndex++)
            {
                for (int beatIndex = 0; beatIndex < 4; beatIndex++)
                {
                    beat = sheet.measures[measureIndex].beats[beatIndex];
                    for (int noteIndex = 0; noteIndex < beat.notes.Count; noteIndex++)
                    {
                        note = beat.notes[noteIndex];
                        GetNoteButton(measureIndex, note.id).Disappear();
                    }
                    beat.notes = new();
                    //yield return new WaitForSeconds(0.05f);
                }
            }
            yield return null;
        }
    }
}