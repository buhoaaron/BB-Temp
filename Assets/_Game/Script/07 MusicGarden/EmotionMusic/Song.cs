using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionMusic
{
    [CreateAssetMenu(menuName = "Game/Emotion Music/Song", fileName = "")]
    public class Song : ScriptableObject
    {
        public new string name;
        public int bpm;
        public Sprite sprite;
        public AudioClip audioClip;
    }
}