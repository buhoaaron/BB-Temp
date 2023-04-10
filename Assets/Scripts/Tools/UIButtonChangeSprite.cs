using UnityEngine;
using UnityEngine.UI;

namespace Barnabus.UI
{
    public class UIButtonChangeSprite : MonoBehaviour
    {
        public Sprite[] Sprites;

        private Button button = null;

        private void Awake()
        {
            if (!TryGetComponent<Button>(out button))
                Debug.LogError("Button component is required.");
        }

        public void Change(int index, bool isNativeSize = false)
        {
            button.image.sprite = Sprites[index];

            if (isNativeSize)
                button.image.SetNativeSize();
        }
    }
}
