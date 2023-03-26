using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine.AddressableAssets;

namespace HiAndBye
{
    public class HiAndByeSpineAssets : MonoBehaviour, IBaseSystem
    {
        public readonly string Label = "HiAndByeSpine";
        public UnityAction OnLoadCompleted = null;

        private List<SkeletonDataAsset> listSkeletonDataAsset;
        private Coroutine coroutineLoadSpineAsset = null;

        #region BASE_API
        public void Init()
        {
            listSkeletonDataAsset = new List<SkeletonDataAsset>();
        }
        public void SystemUpdate()
        {
            
        }
        public void Clear()
        {
            Destroy(this);
        }
        #endregion

        public void LoadSpineAssets()
        {
            coroutineLoadSpineAsset = StartCoroutine(ILoadSpineAssets());
        }

        private IEnumerator ILoadSpineAssets()
        {
            var handle = Addressables.LoadAssetsAsync<SkeletonDataAsset>(Label, listSkeletonDataAsset.Add);

            yield return handle;

            OnLoadCompleted?.Invoke();

            Addressables.Release(handle);
        }

        public SkeletonDataAsset GetSpineAsset(string name)
        {
            return listSkeletonDataAsset.Find(data=>data.name.Contains(name));
        }
    }
}
