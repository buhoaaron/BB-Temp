using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Barnabus.Shelf
{
    public class ShelfAssets : BaseLoadAssets
    {
        public UnityAction OnLoadCompleted = null;

        [Header("Set HubBrand")]
        public List<Sprite> ListHubBrandSprite = null;

        [Header("Set HubBrand Bg")]
        public List<Sprite> ListHubBrandBgSprite = null;

        public Sprite GetHubBrandBg(int index)
        {
            return ListHubBrandBgSprite[index];
        }

        public Sprite GetHubBrand(int index)
        {
            return ListHubBrandSprite[index];
        }
    }
}
