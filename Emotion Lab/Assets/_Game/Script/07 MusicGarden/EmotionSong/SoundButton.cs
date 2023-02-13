using UnityEngine;
using UnityEngine.UI;
using System;

namespace Barnabus.MusicGarden.EmotionSong
{
    public class SoundButton : MonoBehaviour
    {
        public Image backgroundImage;
        public Image soundImage;

        private int soundID;
        private Action<int> onClick;

        public void SetOnClickCallback(Action<int> onClickAction)
        {
            onClick = onClickAction;
        }

        public void SetSoundID(int soundID)
        {
            this.soundID = soundID;
        }

        public void OnClick()
        {
            onClick?.Invoke(soundID);
        }
    }
}