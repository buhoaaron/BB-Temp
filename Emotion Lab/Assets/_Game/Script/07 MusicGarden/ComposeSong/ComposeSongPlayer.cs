using Barnabus.MusicGarden.Base;
using CustomAudio;

namespace Barnabus.MusicGarden.ComposeSong
{
    public class ComposeSongPlayer : SheetPlayer<InstrumentSound>
    {
        protected override void NotePlay(Note<InstrumentSound> note)
        {
            AudioManager.instance.PlaySound(soundAssets[note.info.instrument].audioClip, note.info.pitch);
        }
    }
}