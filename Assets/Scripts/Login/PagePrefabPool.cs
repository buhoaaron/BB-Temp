using UnityEngine;
using System.Collections.Generic;

namespace Barnabus.Login
{
    public class PagePrefabPool : MonoBehaviour
    {
        [System.Serializable]
        private struct PagePrefabPair
        {
            public PAGE Name;
            public GameObject Prefab;
        }

        [SerializeField]
        [Header("Set Page Prefab")]
        private List<PagePrefabPair> Prefabs;

        public GameObject GetPrefab(PAGE name)
        {
            var prefabPair = Prefabs.Find(pair => pair.Name == name);

            Debug.Assert(prefabPair.Prefab != null, "GetPrefab Fail:" + name);

            return prefabPair.Prefab;
        }
    }
}
