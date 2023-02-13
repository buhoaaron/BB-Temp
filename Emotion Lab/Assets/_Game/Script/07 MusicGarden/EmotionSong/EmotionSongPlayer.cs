using Barnabus.MusicGarden.Base;
using CustomAudio;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.MusicGarden.EmotionSong
{
    public class EmotionSongPlayer : SheetPlayer<BabySound>
    {
        [Header("SoundButton")]
        [SerializeField]
        private List<Animator> soundButtonAnimators;

        protected override void NotePlay(Note<BabySound> note)
        {
            AudioManager.instance.PlaySound(soundAssets[note.info.soundID].audioClip, note.info.pitch);
            soundButtonAnimators[note.info.buttonID].Play("Play", 0, 0);
        }
    }
}