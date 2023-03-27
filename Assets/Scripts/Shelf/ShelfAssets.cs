using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
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
        [SerializeField]
        private List<Sprite> listHubBrandBarnabusSprite = null;

        public void LoadAssets()
        {
            LoadAsset<Sprite[]>(AddressablesLabels.HubBrandSprites, ProcessLoadCompleted);
        }

        private void ProcessLoadCompleted(AsyncOperationHandle<Sprite[]> handle)
        {
            listHubBrandBarnabusSprite = new List<Sprite>(handle.Result);

            OnLoadCompleted?.Invoke();
            
            Release(handle);
        }

        public Sprite GetHubBrandBarnabusSprite(string name)
        {
            return listHubBrandBarnabusSprite.Find(data => data.name.Contains(name));
        }

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
