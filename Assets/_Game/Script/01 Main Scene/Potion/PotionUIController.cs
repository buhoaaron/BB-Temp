using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Barnabus
{
    public class PotionUIController : MonoBehaviour
    {
        [SerializeField]
        private bool enableTest = false;
        [SerializeField]
        private bool enableGoalImage = false;

        [Space(10)]
        [SerializeField]
        private BarnabusList barnabusList;
        [SerializeField]
        private Image potionRate;
        [SerializeField]
        private GameObject goalCompletedImage;

        [Header("Text")]
        [SerializeField]
        private TextMeshProUGUI redPotionCount;
        [SerializeField]
        private TextMeshProUGUI yellowPotionCount;
        [SerializeField]
        private TextMeshProUGUI greenPotionCount;
        [SerializeField]
        private TextMeshProUGUI bluePotionCount;
        [SerializeField]
        private TextMeshProUGUI redPotionRequirementCount;
        [SerializeField]
        private TextMeshProUGUI yellowPotionRequirementCount;
        [SerializeField]
        private TextMeshProUGUI greenPotionRequirementCount;
        [SerializeField]
        private TextMeshProUGUI bluePotionRequirementCount;
        [SerializeField]
        private Color notEnoughTextColor = Color.red;
        [SerializeField]
        private Color enoughTextColor = Color.green;

        private Potions requirementPotions;

        private void Start()
        {
            DataManager.LoadPotions();
            DataManager.LoadCharacterData();
            RefreshUI();
        }
        private void Update()
        {
#if UNITY_EDITOR
            if (enableTest)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    DataManager.Potions.AddPotion(PotionType.Red, 1);
                    DataManager.SavePotions();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    DataManager.Potions.AddPotion(PotionType.Yellow, 1);
                    DataManager.SavePotions();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    DataManager.Potions.AddPotion(PotionType.Green, 1);
                    DataManager.SavePotions();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    DataManager.Potions.AddPotion(PotionType.Blue, 1);
                    DataManager.SavePotions();
                }
                else if (Input.GetKeyDown(KeyCode.R))
                {
                    DataManager.Potions.ReducePotion(PotionType.Red, DataManager.Potions[PotionType.Red]);
                    DataManager.Potions.ReducePotion(PotionType.Yellow, DataManager.Potions[PotionType.Yellow]);
                    DataManager.Potions.ReducePotion(PotionType.Green, DataManager.Potions[PotionType.Green]);
                    DataManager.Potions.ReducePotion(PotionType.Blue, DataManager.Potions[PotionType.Blue]);
                    DataManager.SavePotions();
                }
                else if (Input.GetKeyDown(KeyCode.S)) DataManager.SavePotions();
                else if (Input.GetKeyDown(KeyCode.L)) DataManager.LoadPotions();
            }
#endif
            RefreshUI();
        }

        public void RefreshUI()
        {
            requirementPotions = GetNextBarnabusPotions();
            redPotionCount.text = DataManager.Potions[PotionType.Red].ToString();
            yellowPotionCount.text = DataManager.Potions[PotionType.Yellow].ToString();
            greenPotionCount.text = DataManager.Potions[PotionType.Green].ToString();
            bluePotionCount.text = DataManager.Potions[PotionType.Blue].ToString();

            if (DataManager.Potions[PotionType.Red] >= requirementPotions[PotionType.Red]) redPotionCount.color = enoughTextColor;
            else redPotionCount.color = notEnoughTextColor;
            if (DataManager.Potions[PotionType.Yellow] >= requirementPotions[PotionType.Yellow]) yellowPotionCount.color = enoughTextColor;
            else yellowPotionCount.color = notEnoughTextColor;
            if (DataManager.Potions[PotionType.Green] >= requirementPotions[PotionType.Green]) greenPotionCount.color = enoughTextColor;
            else greenPotionCount.color = notEnoughTextColor;
            if (DataManager.Potions[PotionType.Blue] >= requirementPotions[PotionType.Blue]) bluePotionCount.color = enoughTextColor;
            else bluePotionCount.color = notEnoughTextColor;

            redPotionRequirementCount.text = requirementPotions[PotionType.Red].ToString();
            yellowPotionRequirementCount.text = requirementPotions[PotionType.Yellow].ToString();
            greenPotionRequirementCount.text = requirementPotions[PotionType.Green].ToString();
            bluePotionRequirementCount.text = requirementPotions[PotionType.Blue].ToString();
            potionRate.fillAmount = DataManager.Potions.GetPercent(requirementPotions);
            if (enableGoalImage) goalCompletedImage.SetActive(potionRate.fillAmount >= 1);
        }

        public void FitRequirement(GameObject go)
        {
            Debug.Log(go.name);

            if (DataManager.Potions[PotionType.Red] < requirementPotions[PotionType.Red])
                return;
            else if (DataManager.Potions[PotionType.Yellow] < requirementPotions[PotionType.Yellow])
                return;
            else if (DataManager.Potions[PotionType.Green] < requirementPotions[PotionType.Green])
                return;
            else if (DataManager.Potions[PotionType.Blue] < requirementPotions[PotionType.Blue])
                return;

            Debug.Log(go);

            go.SetActive(true);
        }

        private Potions GetNextBarnabusPotions()
        {
            for (int i = 1; i <= barnabusList.Count; i++)
            {
                if (!DataManager.IsCharacterUnlocked(i))
                {
                    if (barnabusList[i] != null) return new Potions(barnabusList[i].potionRequirement);
                }
            }
            return new Potions(9999, 9999, 9999, 9999);
        }
    }
}
