using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    public enum ActionType { ChangeCharacter, GenerateItem, DeleteItem, Edit}
    public enum TargetType { Background, Character, Item }
    public enum EditType { Layer, Move, Rotate, Scale, Color }

    public class ActionRecord
    {
        public ActionType actionType;
        public TargetType targetType;
        public EditType editType;
        public string targetTypeName;
        public string targetName;
        public ItemInfo info;
        public Vector3 color;
    }
}