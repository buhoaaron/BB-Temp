namespace HiAndBye
{
    public class InterferenceInfo
    {
        public readonly EFFECT EffectType;
        public readonly float t;
        public readonly float W_Time;
        public readonly float W_CorrectNum;

        public InterferenceInfo(EFFECT effectType, float t, int w_Time, int w_CorrectNum)
        {
            this.EffectType = effectType;
            this.t = t;
            this.W_Time = w_Time;
            this.W_CorrectNum = w_CorrectNum;
        }
    }
}
