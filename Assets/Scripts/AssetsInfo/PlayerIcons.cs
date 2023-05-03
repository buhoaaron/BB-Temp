using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    public class PlayerIcons : List<Sprite>
    {
        private string iconFormat = "BG_PlayerID{0}_{1}";

        public PlayerIcons(IEnumerable<Sprite> collection) : base(collection)
        {}

        public Sprite GetIcon(int colorId, int skinId)
        {
            var name = string.Format(iconFormat, colorId, skinId);
            var sp = Find(x => x.name.Equals(name));

            return sp;
        }
    }
}
