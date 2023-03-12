using System;
using System.Collections.Generic;

namespace Barnabus
{
    public class AllBarnabusBaseData : List<BarnabusBaseData>
    {
        public AllBarnabusBaseData()
        {}

        public AllBarnabusBaseData(IEnumerable<BarnabusBaseData> collection) : base(collection)
        {}

        public BarnabusBaseData GetBarnabusBaseData(int id)
        {
            return Find(data => data.CharacterID == id);
        }

        public override string ToString()
        {
            var result = "";
            foreach(var data in this)
            {
                result += String.Format("ID:{0} Name:{1}\n", data.CharacterID, data.Name);
            }

            return result;
        }

        public AllBarnabusBaseData Copy()
        {
            var newData = new AllBarnabusBaseData();

            foreach(var data in this)
                newData.Add(data.Copy());

            return newData;
        }
    }
}
