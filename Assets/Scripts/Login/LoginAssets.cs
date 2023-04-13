using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace Barnabus.Login
{
    public class LoginAssets : BaseLoadAssets
    {
        private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

        public async void LoadPrefabFromAddress(string key)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(key);

            await handle.Task;

            //Debug.Log(handle.Result.name);

            if (!prefabs.ContainsKey(key))
                prefabs.Add(key, handle.Result);
        }

        public GameObject GetPrefab(string key)
        {
            if (prefabs.ContainsKey(key))
                return prefabs[key];

            return null;
        }
    }
}
