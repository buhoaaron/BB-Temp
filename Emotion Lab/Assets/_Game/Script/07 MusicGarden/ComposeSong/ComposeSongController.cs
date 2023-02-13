using CustomAudio;
using System.Collections.Generic;
using UnityEngine;
using Barnabus.MusicGarden.Base;
using Game;
using UnityEngine.UI;

namespace Barnabus.MusicGarden.ComposeSong
{
    public enum Instrument { Drum, Piano, Guitar}

    public class ComposeSongController : Base.SheetController<InstrumentSound>
    {
        [Header("InstrumentButtons")]
        [SerializeField]
        private List<Image> buttonBackgrounds;
        [SerializeField]
        private List<Image> instrumentImages;

        [Header("ButtonSprite")]
        [SerializeField]
        private Sprite normalSprite;
        [SerializeField]
        private Sprite selectedSprite;

        private Instrument selectedInstrument = Instrument.Piano; 
        private readonly float pitchScale = Mathf.Pow(2f, 1f / 12f);

        public override void Initialize()
        {
            base.Initialize();

            if(soundAssets.Count < instrumentImages.Count)
            {
                Debug.LogError("ComposeSong soundAssets not enough.");
                return;
            }

            for(int i = 0; i < instrumentImages.Count; i++) 
            {
                instrumentImages[i].sprite = soundAssets[i].buttonSprite;
                buttonBackgrounds[i].sprite = normalSprite;
            }

            SelectInstrument(1);
        }

        //Data
        protected override void SetDataKey()
        {
            dataController.SetKey(GameSettings.ComposeSongDataPath);
        }

        // Measure
        protected override int GetNoteAmountPerMeasure()
        {
            return 36;
        }

        //Instrument
        public void SelectInstrument(int id)
        {
            buttonBackgrounds[(int)selectedInstrument].sprite = normalSprite;
            selectedInstrument = (Instrument)id;
            buttonBackgrounds[(int)selectedInstrument].sprite = selectedSprite;
        }

        //Note
        public override void ClickedNote(int measureID, int noteID)
        {
            if (selectedInstrument == Instrument.Drum && noteID > 3) return;
            else if (selectedInstrument != Instrument.Drum && noteID <= 3) return;

            AddNote(measureID, noteID, out int removedNoteID);

            if (removedNoteID != -1) GetNoteButton(measureID, removedNoteID).Disappear();
            if (noteID != removedNoteID)
            {
                GetNoteButton(measureID, noteID).Appear();
                GetNoteButton(measureID, noteID).SetImage(GetNoteSpriteByInstrument(selectedInstrument));
                //GetNoteButton(measureID, noteID).SetColor(GetColorByInstrument(selectedInstrument));

                if (!sheetPlayer.IsPlaying)
                    AudioManager.instance.PlaySound(GetAudioClipByInstrument(selectedInstrument), GetPitchByNoteID(noteID));
            }
        }

        protected override void AddNote(int measureID, int noteID, out int removedNoteID)
        {
            List<Note<InstrumentSound>> notes = sheet.measures[measureID].beats[noteID % 4].notes;

            removedNoteID = -1;
            int sameIdIndex = FindNoteById(notes, noteID);
            int sameInstrumentIndex = FindNoteByInstrument(notes, (int)selectedInstrument);

            if (sameIdIndex == -1) //�I������l�O�Ū�
            {
                if (sameInstrumentIndex == -1) //��beat�|�����o�Ӽ־� => �s�W
                {
                    sheet.AddNote(measureID, noteID, new InstrumentSound((int)selectedInstrument, GetPitchByNoteID(noteID)));
                }
                else //��beat��L��l�w���o�Ӽ־� => �R���Ӯ�A�s�W�o��
                {
                    removedNoteID = notes[sameInstrumentIndex].id;
                    sheet.RemoveNote(measureID, removedNoteID);
                    sheet.AddNote(measureID, noteID, new InstrumentSound((int)selectedInstrument, GetPitchByNoteID(noteID)));
                }
            }
            else //�I������l�w���־�
            {
                if (notes[sameIdIndex].info.instrument == (int)selectedInstrument) //�P�Ӯ�־��ۦP => �R��
                {
                    removedNoteID = notes[sameIdIndex].id;
                    sheet.RemoveNote(measureID, removedNoteID);
                }
                else //�P�Ӯ�־����P => �����Ӯ�־�
                {
                    notes[sameIdIndex].info.instrument = (int)selectedInstrument;

                    if (sameInstrumentIndex != -1) //��beat��L��l���ۦP�־� => �R���Ӯ�
                    {
                        removedNoteID = notes[sameInstrumentIndex].id;
                        sheet.RemoveNote(measureID, removedNoteID);
                    }
                }
            }
        }

        private int FindNoteById(List<Note<InstrumentSound>> notes, int noteID)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].id == noteID) 
                    return i;

            return -1;
        }

        private int FindNoteByInstrument(List<Note<InstrumentSound>> notes, int instrument)
        {
            for (int i = 0; i < notes.Count; i++)
                if (notes[i].info.instrument == instrument)
                    return i;

            return -1;
        }

        private NoteButton GetNoteButton(int measureID, int noteID)
        {
            return measureViews[measureID].GetNoteButton(noteID);
        }

        private Sprite GetNoteSpriteByInstrument(Instrument instrument)
        {
            return soundAssets[(int)instrument].noteSprite;
        }

        private AudioClip GetAudioClipByInstrument(Instrument instrument)
        {
            return soundAssets[(int)instrument].audioClip;
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
                _ => 1, //Drum or C
            };
        }

        protected override void SetNoteButton(int measureID, Note<InstrumentSound> note)
        {
            GetNoteButton(measureID, note.id).SetImage(GetNoteSpriteByInstrument((Instrument)note.info.instrument));
            //GetNoteButton(measureID, note.id).SetColor(GetColorByInstrument((Instrument)note.info.instrument));
        }
    }
}