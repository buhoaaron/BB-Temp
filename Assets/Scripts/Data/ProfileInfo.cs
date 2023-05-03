﻿using UnityEngine;

namespace Barnabus
{
    /// <summary>
    /// Profile資訊(Server給的)
    /// </summary>
    public class ProfileInfo
    {
        public readonly int player_id;
        public readonly string first_name;
        public readonly string last_name;
        public readonly string family_nick_name;

        #region CLIENT
        public Sprite SpriteIcon = null;
        #endregion

        public ProfileInfo(int player_id, string first_name, string last_name, string family_nick_name)
        {
            this.player_id = player_id;
            this.first_name = first_name;
            this.last_name = last_name;
            this.family_nick_name = family_nick_name;
        }
    }
}
