using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.Hunting
{
    [CreateAssetMenu(menuName = "Game/Hunting/Asset")]
    public class HuntingAsset : ScriptableObject
    {
        public Sprite notFoundSprite;
        public Sprite foundSprite;

        [Space(10)]
        public BarnabusList barnabusList;
        [Space(10)]
        public List<Sprite> decorateSprites;
    }
}