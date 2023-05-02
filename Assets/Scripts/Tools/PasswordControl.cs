using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Barnabus.UI
{
    /// <summary>
    /// PWD控制
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class PasswordControl : MonoBehaviour
    {
        public Char AsteriskChar = '●';
        public string RealText { private set; get; } = "";

        protected TMP_InputField target = null;

        protected bool isDetectChange = false;

        private void Awake()
        {
            target = GetComponent<TMP_InputField>();
            target.contentType = TMP_InputField.ContentType.Alphanumeric;

            target.onValueChanged.AddListener(ProcessValueChanged);
            target.onDeselect.AddListener(ProcessDeselect);
            target.onSelect.AddListener(ProcessSelect);
        }

        private string CheckLength(string value)
        {
            //玩家新增字符
            if (value.Length > RealText.Length)
            {
                //加入新增的字符
                RealText += value[value.Length - 1];
            }
            //玩家減少字符
            if (value.Length < RealText.Length)
            {
                var removeTimes = RealText.Length - value.Length;
                //刪掉玩家減少的字符數
                for(int i = 0; i < removeTimes; i++)
                {
                    RealText = RealText.Remove(RealText.Length - 1, 1);
                }
            }

            return RealText;
        }

        private void ProcessValueChanged(string value)
        {
            if (!isDetectChange) return;

            CheckLength(value);

            var newValue = "";
            var lastIndex = RealText.Length - 1;

            //只顯示最後一個字元，其它皆以AsteriskChar替代
            for (int index = 0; index < RealText.Length; index++)
            {
                bool isLastChar = index == lastIndex;
                newValue += isLastChar ? RealText[index] : AsteriskChar;
            }
            //更新顯示介面
            target.text = newValue;
        }

        private void ProcessSelect(string value)
        {
            isDetectChange = true;
        }

        private void ProcessDeselect(string value)
        {
            isDetectChange = false;

            //全部都隱藏
            var newValue = new string(AsteriskChar, RealText.Length);
            target.text = newValue;
        }
    }
}
