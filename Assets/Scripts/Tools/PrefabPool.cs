using UnityEngine;
using System.Collections.Generic;

namespace Barnabus
{
    public class PrefabPool : MonoBehaviour
    {
        [System.Serializable]
        private struct PrefabPair
        {
            public string Name;
            public GameObject Prefab;
        }

        [SerializeField]
        [Header("Set Prefab")]
        private List<PrefabPair> Prefabs;

        public GameObject GetPrefab(int index)
        {
            return Prefabs[index].Prefab;
        }

        public GameObject GetPrefab(string name)
        {
            return Prefabs.Find(pair => pair.Name == name).Prefab;
        }
    }
}
