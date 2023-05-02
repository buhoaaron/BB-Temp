using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Barnabus.UI
{
    public class UIDropdownEnhance : MonoBehaviour
    {

        private void Awake()
        {
            var dropdown = GetComponent<TMP_Dropdown>();

            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = "K";

            var list = new List<TMP_Dropdown.OptionData>();
            list.Add(option);

            dropdown.AddOptions(list);
            dropdown.onValueChanged.AddListener(OnValueChanged);

            
        }

        private void OnValueChanged(int value)
        {
            print(value);
        }
    }
}
