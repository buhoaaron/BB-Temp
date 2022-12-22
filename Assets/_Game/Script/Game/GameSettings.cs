using UnityEngine;
using DataStream;

namespace Game
{
    //[CreateAssetMenu(menuName = "Game/Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Potion Type")]
        [SerializeField]
        private PotionType emotionFacePotionType;
        [SerializeField]
        private PotionType emotionMusicPotionType;
        [SerializeField]
        private PotionType dotToDotPotionType;
        [SerializeField]
        private PotionType breathingPotionType;

        [Header("Data")]
        [Tooltip("資料存取的所在地")]
        [SerializeField]
        private AccessType accessType;
        [Tooltip("資料存取的類型")]
        [SerializeField]
        private DataType dataType;

        [Header("Data Path")]
        [SerializeField]
        private string emotionMusicDataPath;
        [SerializeField]
        private string composeSongDataPath;
        [SerializeField]
        private string emotionSongDataPath;
        [SerializeField]
        private string gymDanceDataPath;
        [SerializeField]
        private string gymYogaDataPath;

        private static GameSettings instance;
        private static GameSettings Instance
        {
            get
            {
                if (!instance) instance = Resources.LoadAll<GameSettings>("Game")[0];
                if (!instance)
                {
                    Debug.LogError("GameSettings not found...");
                    instance = CreateInstance<GameSettings>();
                }
                return instance;
            }
        }

        public static PotionType EmotionFacePotionType { get { return Instance.emotionFacePotionType; } }
        public static PotionType EmotionMusicPotionType { get { return Instance.emotionMusicPotionType; } }
        public static PotionType DotToDotPotionType { get { return Instance.dotToDotPotionType; } }
        public static PotionType BreathingPotionType { get { return Instance.breathingPotionType; } }

        public static AccessType AccessType { get { return Instance.accessType; } }
        public static DataType DataType { get { return Instance.dataType; } }

        public static string EmotionMusicDataPath { get { return Instance.emotionMusicDataPath; } }
        public static string ComposeSongDataPath { get { return Instance.composeSongDataPath; } }
        public static string EmotionSongDataPath { get { return Instance.emotionSongDataPath; } }
        public static string GymDanceDataPath { get { return Instance.gymDanceDataPath; } }
        public static string GymYogaDataPath { get { return Instance.gymYogaDataPath; } }
    }
}