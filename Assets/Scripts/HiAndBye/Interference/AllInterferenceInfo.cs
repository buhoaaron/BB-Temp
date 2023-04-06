using System.Collections.Generic;

namespace HiAndBye
{
    public class AllInterferenceInfo : List<InterferenceInfo>
    {
        public InterferenceInfo GetInterferenceInfo(EFFECT effect)
        {
            return Find(info => info.EffectType == effect);
        }
    }
}
