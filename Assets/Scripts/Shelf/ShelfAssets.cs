using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Barnabus.Shelf
{
    public class ShelfAssets : BaseLoadAssets
    {
        public UnityAction OnLoadCompleted = null;

        [Header("Set HubBrand")]
        public List<Sprite> ListHubBrandSprite = null;

        [Header("Set HubBrand Bg")]
        public List<Sprite> ListHubBrandBgSprite = null;

        public List<Sprite> ListNavigationBarSprites = null;


        public Sprite GetHubBrandBg(int index)
        {
            return ListHubBrandBgSprite[index];
        }

        public Sprite GetHubBrand(int index)
        {
            return ListHubBrandSprite[index];
        }

        public void LoadNavigationBarAsset()
        {
            base.LoadAssetAsync<Sprite[]>(AddressablesLabels.NavigationBarSprites, OnLoadNavigationBarAssetComplete);
        }

        private void OnLoadNavigationBarAssetComplete(AsyncOperationHandle<Sprite[]> handle)
        {
            ListNavigationBarSprites = new List<Sprite>(handle.Result);

            //base.Release(handle);
        }
    }
}
