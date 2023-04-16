using UnityEngine;
using TMPro;
using DG.Tweening;
using Barnabus.UI;

namespace Barnabus.Login
{
    public class BirthYearNumberController : MonoBehaviour, IBaseController
    {
        public TMP_Text TextNumber = null;
        public UIImageSwitch ImageSwitch = null;

        private Vector2 originPos = Vector2.zero;

        #region BASE_API
        public void Init()
        {
            originPos = transform.localPosition;
            TextNumber.text = "";
        }
        public void Refresh()
        {

        }
        public void Clear()
        {
            TextNumber.text = "";
        }
        #endregion

        /// <summary>
        /// 是否為空
        /// </summary>
        /// <returns></returns>
        public bool CheckEmpty()
        {
            return string.IsNullOrEmpty(TextNumber.text);
        }

        public void SetNumber(int number)
        {
            TextNumber.text = number.ToString();
        }

        public int GetNumber()
        {
            try
            {
                return int.Parse(TextNumber.text);
            }
            catch(System.Exception ex)
            {
                Debug.LogError("GetNumber exception: " + ex);
                return 0;
            }
        }

        public void DoShake()
        {
            transform.localPosition = originPos;
            transform.DOShakePosition(0.5f, 10);
        }
    }
}
