using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.EmotionMusic
{
    public class SongButton : MonoBehaviour
    {
        public Image songImage;
        public TMPro.TextMeshProUGUI songName;
        public GameObject frame;
        [SerializeField]
        private Animator animator;

        [HideInInspector]
        public int id;
        private Action<SongButton> onClick;

        public void OnClick() { onClick?.Invoke(this); }

        public void SetID(int id) { this.id = id; }
        public void SetOnClickCallback(Action<SongButton> onClickAction) { onClick = onClickAction; }

        public void SetSongSprite(Sprite sprite) { songImage.sprite = sprite; }
        public void SetSongName(string songName) { this.songName.text = songName; }
        public void SetFrameVisable(bool visable) { frame.SetActive(visable); }

        public void Play() { animator.Play("Playing", 0, 0); }
        public void Stop() { animator.Play("Idle", 0, 0); }
    }
}