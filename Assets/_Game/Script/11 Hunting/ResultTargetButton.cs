using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.Hunting
{
    public class ResultTargetButton : MonoBehaviour
    {
        public int characterID;
        public Image characterImage;
        public Image backgroundImage;

        public Action<int> onClick;

        public void OnClick()
        {
            onClick?.Invoke(characterID);
        }
    }
}