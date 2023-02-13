using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    [System.Serializable]
    public class ItemInfo
    {
        public string typeName;
        public string itemName;
        public Vector2 positionRatio = new Vector2(0, 0); //(-1, -1) to (1, 1)
        public float scale = 1;
        public float rotation = 0;
        public int layer;
    }
}