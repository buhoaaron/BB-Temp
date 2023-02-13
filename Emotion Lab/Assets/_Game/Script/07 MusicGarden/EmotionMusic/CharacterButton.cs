using UnityEngine;
using UnityEngine.UI;
using System;

namespace Barnabus.EmotionMusic
{
    public class CharacterButton : MonoBehaviour
    {
        public Image backgroundImage;
        public Image characterImage;
        public Image frameImage;
        [SerializeField]
        private Button button;

        [HideInInspector]
        public int characterID;
        private Action<CharacterButton> onClick;

        public void OnClick() { onClick?.Invoke(this); }

        public void SetCharacterID(int characterID) { this.characterID = characterID; }
        public void SetOnClickCallback(Action<CharacterButton> onClickAction) { onClick = onClickAction; }

        public void SetBackgroundColor(Color color) { backgroundImage.color = color; }
        public void SetBackgroundSprite(Sprite sprite) { backgroundImage.sprite = sprite; }

        public void SetCharacterColor(Color color) { characterImage.color = color; }
        public void SetCharacterSprite(Sprite sprite) { characterImage.sprite = sprite; }

        public void SetFrameSprite(Sprite sprite) { frameImage.sprite = sprite; }
        public void SetFrameVisable(bool visable) { frameImage.gameObject.SetActive(visable); }

        public void SetEnable(bool enable)
        {
            button.enabled = enable;
            if (enable)
            {
                backgroundImage.color = Color.white;
                //characterImage.color = Color.white;
            }
            else
            {
                backgroundImage.color = Color.gray;
                //characterImage.color = Color.gray;
            }
        }
    }
}