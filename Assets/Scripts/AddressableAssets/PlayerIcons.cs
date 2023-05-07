using System.Collections.Generic;
using UnityEngine;

namespace Barnabus
{
    public class PlayerIcons : List<Sprite>
    {
        private string iconFormat = "avatar_icon_{0}_{1}";

        public PlayerIcons(IEnumerable<Sprite> collection) : base(collection)
        {}

        public Sprite GetIcon(string colorId, string skinId)
        {
            var name = string.Format(iconFormat, colorId, skinId);
            var sp = Find(x => x.name.Equals(name));

            return sp;
        }

        public Sprite GetIcon(int colorId, int skinId)
        {
            return GetIcon(colorId.ToString(), skinId.ToString());
        }
    }
}
