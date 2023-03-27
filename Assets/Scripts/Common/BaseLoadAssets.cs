using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

namespace Barnabus
{
    public class BaseLoadAssets : MonoBehaviour
    {
        protected AsyncOperationHandle LoadAsset<T>(string label, Action<AsyncOperationHandle<T>> onComplete)
        {
            var handle = Addressables.LoadAssetAsync<T>(label);
            handle.Completed += onComplete;

            return handle;
        }

        protected void Release(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }
    }
}
