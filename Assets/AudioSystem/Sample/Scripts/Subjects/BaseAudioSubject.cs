namespace AudioSystem
{
    public abstract class BaseAudioSubject
    {
        protected AUDIO_NAME audio;

        public BaseAudioSubject(AUDIO_NAME audio)
        {
            this.audio = audio;
        }
    }
}
