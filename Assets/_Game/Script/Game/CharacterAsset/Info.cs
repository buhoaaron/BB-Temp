using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.CharacterAsset
{
    [CreateAssetMenu(menuName = "Game/CharacterAsset/Info", fileName = "")]
    public class Info : ScriptableObject
    {
        public int id;
        public new string name;
    }
}