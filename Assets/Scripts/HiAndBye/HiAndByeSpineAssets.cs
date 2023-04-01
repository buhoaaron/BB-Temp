using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace HiAndBye
{
    public class HiAndByeSpineAssets : MonoBehaviour, IBaseSystem
    {
        public readonly string Label = AddressablesLabels.HiAndByeSpines;
        public UnityAction OnLoadCompleted = null;

        private List<SkeletonDataAsset> listSkeletonDataAsset;
        private AsyncOperationHandle handleLoadSpineAsset;

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
            Addressables.Release(handleLoadSpineAsset);
        }
        #endregion

        public void LoadSpineAssets()
        {
            StartCoroutine(ILoadSpineAssets());
        }

        private IEnumerator ILoadSpineAssets()
        {
            handleLoadSpineAsset = Addressables.LoadAssetsAsync<SkeletonDataAsset>(Label, listSkeletonDataAsset.Add);

            yield return handleLoadSpineAsset;

            OnLoadCompleted?.Invoke();
        }

        public SkeletonDataAsset GetSpineAsset(string name)
        {
            return listSkeletonDataAsset.Find(data=>data.name.Contains(name));
        }
    }
}
