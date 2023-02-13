using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.Hunting
{
    public class Point : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        private int characterID = -1;
        private Action<int> onClick;

        public void SetCharacterID(int id) { characterID = id; }

        public void SetImage(Sprite sprite) { image.sprite = sprite; }

        public void SetOnClickAction(Action<int> onClickAction)
        {
            onClick = onClickAction;
        }

        public void OnClick()
        {
            if(onClick != null)
            {
                onClick.Invoke(characterID);
                gameObject.SetActive(false);
            }
        }
    }
}