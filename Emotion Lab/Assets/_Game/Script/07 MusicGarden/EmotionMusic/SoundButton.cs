using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.EmotionMusic
{
    public class SoundButton : MonoBehaviour
    {
        public Image backgroundImage;
        public Image characterImage;
        public Image frameImage;
        [SerializeField]
        private Animator animator;

        [HideInInspector]
        public int characterID;

        public void SetCharacterID(int characterID) { this.characterID = characterID; }
        public void OnPlayed() { animator.Play("OnPlayed", 0, 0); }

        public void SetBackgroundSprite(Sprite sprite) { backgroundImage.sprite = sprite; }
        public void SetBackgroundColor(Color color) { backgroundImage.color = color; }
        public void SetCharacterSprite(Sprite sprite) { characterImage.sprite = sprite; }
        public void SetFrameVisable(bool visable) { frameImage.gameObject.SetActive(visable); }

        public void SetEmpty(bool isEmpty)
        {
            if (!isEmpty)
            {
                characterImage.color = Color.white;
            }
            else
            {
                backgroundImage.color = Color.white;
                characterImage.color = new Color(1, 1, 1, 0);
            }
        }
    }
}