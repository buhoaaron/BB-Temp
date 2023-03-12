using UnityEngine;

namespace Barnabus.Card
{
    [CreateAssetMenu(fileName = "BarnabusData", menuName = "Barnabus/BarnabusData", order = 1)]
    public  class BarnabusData : ScriptableObject
    {
        public BARNABUS_CARD Type;
        public string Name;
        public string Element;
        public string ElementName;
    }
}
