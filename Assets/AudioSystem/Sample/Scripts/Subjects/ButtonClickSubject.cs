using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AudioSystem
{
    public class ButtonClickSubject : BaseAudioSubject
    {
        public UnityAction<AUDIO_NAME> OnPlaySound;

        private List<Button> buttons = new List<Button>();

        public ButtonClickSubject(AUDIO_NAME audio) : base(audio)
        {
        }

        public void AddButton(Button button)
        {
            if (buttons.Contains(button))
                return;

            button.onClick.AddListener(PlaySound);
            buttons.Add(button);
        }

        public void RemoveButton(Button button)
        {
            if (buttons.Contains(button))
            {
                button.onClick.RemoveListener(PlaySound);
                buttons.Remove(button);
            }
        }

        private void PlaySound()
        {
            OnPlaySound?.Invoke(audio);
        }
    }
}
