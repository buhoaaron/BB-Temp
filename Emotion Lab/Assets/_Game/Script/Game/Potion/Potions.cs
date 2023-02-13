using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType { Red, Yellow, Green, Blue }

namespace Barnabus
{
    [System.Serializable]
    public class Potions
    {
        public int Total { get { return red + yellow + green + blue; } }
        public int this[PotionType potionType]
        {
            get
            {
                return potionType switch
                {
                    PotionType.Red => red,
                    PotionType.Yellow => yellow,
                    PotionType.Green => green,
                    PotionType.Blue => blue,
                    _ => 0,
                };
            }
        }

        [SerializeField] private int red;
        [SerializeField] private int yellow;
        [SerializeField] private int green;
        [SerializeField] private int blue;

        private const int POTION_TYPE_COUNT = 4;

        public Potions()
        {
            red = 0;
            yellow = 0;
            green = 0;
            blue = 0;
        }

        public Potions(int red, int yellow, int green, int blue)
        {
            this.red = red;
            this.yellow = yellow;
            this.green = green;
            this.blue = blue;
        }

        public Potions(PotionRequirement potionRequirement)
        {
            red = potionRequirement.red;
            yellow = potionRequirement.yellow;
            green = potionRequirement.green;
            blue = potionRequirement.blue;
        }

        public Potions(string json)
        {
            Potions potionData = JsonUtility.FromJson<Potions>(json);
            if (potionData == null)
            {
                red = 0;
                yellow = 0;
                green = 0;
                blue = 0;
            }
            else
            {
                red = potionData.red;
                yellow = potionData.yellow;
                green = potionData.green;
                blue = potionData.blue;
            }
        }

        public string ToJson() { return JsonUtility.ToJson(this); }

        public void AddPotion(Potions potions)
        {
            red += potions.red;
            yellow += potions.yellow;
            green += potions.green;
            blue += potions.blue;
        }

        public void AddPotion(PotionType potionType, int value)
        {
            if (potionType == PotionType.Red) red += value;
            else if (potionType == PotionType.Yellow) yellow += value;
            else if (potionType == PotionType.Green) green += value;
            else if (potionType == PotionType.Blue) blue += value;
        }

        public void ReducePotion(Potions potions)
        {
            red -= potions.red;
            yellow -= potions.yellow;
            green -= potions.green;
            blue -= potions.blue;
            CheckNegative();
        }

        public void ReducePotion(PotionType potionType, int value)
        {
            if (potionType == PotionType.Red) red -= value;
            else if (potionType == PotionType.Yellow) yellow -= value;
            else if (potionType == PotionType.Green) green -= value;
            else if (potionType == PotionType.Blue) blue -= value;
            CheckNegative();
        }

        public float GetPercent(Potions goal)
        {
            float percent = 0;
            if (red == goal.red && yellow == goal.yellow && green == goal.green && blue == goal.blue) return 1;

            percent += Mathf.Min(((float)red / (float)goal.red), 1f) / (float)POTION_TYPE_COUNT;
            percent += Mathf.Min(((float)yellow / (float)goal.yellow), 1f) / (float)POTION_TYPE_COUNT;
            percent += Mathf.Min(((float)green / (float)goal.green), 1f) / (float)POTION_TYPE_COUNT;
            percent += Mathf.Min(((float)blue / (float)goal.blue), 1f) / (float)POTION_TYPE_COUNT;
            return percent;
        }

        private void CheckNegative()
        {
            if (red < 0) red = 0;
            if (yellow < 0) yellow = 0;
            if (green < 0) green = 0;
            if (blue < 0) blue = 0;
        }

    }
}