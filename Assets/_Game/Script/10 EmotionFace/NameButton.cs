using UnityEngine;
using UnityEngine.UI;
using System;

namespace Barnabus.EmotionFace
{
    public class NameButton : MonoBehaviour
    {
        [SerializeField]
        private EmotionFaceController controller;

        public TMPro.TextMeshProUGUI text;
        public Image backgroundImage;
        public Button button;
        public int id;

       
      
        private Action<NameButton> onClick;

        public void OnClick()
        {
            onClick?.Invoke(this);
            controller.SetFileName(text.text);

           
        }

        void Awake()
        {
            controller = GameObject.Find("EmotionFaceController").GetComponent<EmotionFaceController>();
            
            
        
        }

        void Start()
        {
            
        }

        private void Update()
        {
            if (backgroundImage.enabled == false)
            {
                backgroundImage.enabled = true;
            }

           
        }

        public void SetOnClick(Action<NameButton> callback)
        {
            onClick = callback;
            
        }

        public void SetText(string content)
        {
            text.text = content;
        }

        public void SetEnable(bool enabled) { button.enabled = enabled; }
    }
}