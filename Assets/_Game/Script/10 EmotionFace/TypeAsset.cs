using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.EmotionFace
{
    [System.Serializable]
    public class TypeAsset
    {
        [SerializeField]
        private string typeName;
        [SerializeField]
        private Sprite buttonIcon;
        [NonReorderable]
        [SerializeField]
        private List<ElementAsset> elements;





        public string Name { get { return typeName; } }
        public Sprite Icon { get { return buttonIcon; } }
        public List<ElementAsset> Elements { get { return elements; } }

        public ElementAsset GetElement(string elementName)
        {
            for (int i = 0; i < elements.Count; i++)
                if (elements[i].Name == elementName)
                    return elements[i];

            return null;
        }
    }
}