using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Barnabus.Shelf
{
    public class ShelfAssets : MonoBehaviour, IBaseSystem
    {
        public UnityAction OnLoadCompleted = null;

        [Header("Set HubBrand")]
        public List<Sprite> ListHubBrandSprite = null;
        [Header("Set HubBrand Bg")]
        public List<Sprite> ListHubBrandBgSprite = null;
        [SerializeField]
        private List<Sprite> listHubBrandBarnabusSprite = null;

        #region BASE_API
        public void Init()
        {

        }
        public void SystemUpdate()
        {
            
        }
        public void Clear()
        {
            
        }
        #endregion

        public void LoadAssets()
        {
            StartCoroutine(ILoadAssets());
        }

        private IEnumerator ILoadAssets()
        {
            var handle = Addressables.LoadAssetAsync<Sprite[]>("HubBrandSprites");

            yield return handle;

            listHubBrandBarnabusSprite = new List<Sprite>(handle.Result);

            OnLoadCompleted?.Invoke();

            Addressables.Release(handle);
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
