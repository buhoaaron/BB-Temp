using System.IO;
using UnityEngine;

namespace Barnabus
{
    public enum PLATFORM
    {
        IOS,
        ANDROID,
    }

    public static class PlatformHelper
    {
        /// <summary>
        /// 根據平台組合streamingAssetsPath
        /// </summary>
        public static PLATFORM GetPlatform()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.IPhonePlayer:
                    return PLATFORM.IOS;
                case RuntimePlatform.Android:
                    return PLATFORM.ANDROID;
                default:
                    return PLATFORM.ANDROID;
            }
        }
    }
}
