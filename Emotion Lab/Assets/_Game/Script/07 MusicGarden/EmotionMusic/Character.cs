using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Barnabus.EmotionMusic
{
    [CreateAssetMenu(menuName = "Game/Emotion Music/Character", fileName = "")]
    public class Character : ScriptableObject, IHasSkeletonDataAsset
    {
        public int id;

        [Header("Image")]
        public Sprite icon;
        public Sprite lockedIcon;
        public Sprite musicBackground;
        public Sprite buttonBackground;
        public Sprite noteIcon;
        public Sprite longNoteIcon;
        public Color noteColor;

        [Header("Audio")]
        public AudioClip sound;
        public AudioClip longSound;
        public List<Song> songList;

        [Header("Spine")]
        public SkeletonDataAsset spineAsset;
        [SpineAnimation]
        public string idleAnimationName;
        [SpineAnimation]
        public string danceAnimationName;
        
        public SkeletonDataAsset SkeletonDataAsset { get { return spineAsset; } }
    }
}