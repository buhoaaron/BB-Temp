using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    [System.Serializable]
    public class PictureInfo
    {
        public int moodQuestLevel;
        public string pictureFilePath;

        public Vector3 backgroundColor = new Vector3(1, 1, 1);

        public string fileName;
        public string characterTypeName;
        public string characterName;
        public Vector3 characterColor = new Vector3(0, 0, 0);

        public List<ItemInfo> items = new List<ItemInfo>();
    }
}