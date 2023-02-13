using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Barnabus.MainScene
{
    public class MainContent : MonoBehaviour
    {
        public static MainContent Instance;

        public GameObject gameRoom;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}