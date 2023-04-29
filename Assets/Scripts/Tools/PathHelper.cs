using System.IO;
using UnityEngine;

namespace Barnabus
{
    public static class PathHelper
    {
        /// <summary>
        /// 根據平台組合streamingAssetsPath
        /// </summary>
        public static string GetStreamingAssetsPath(string fileName)
        {
#if UNITY_ANDROID 
            var result = Path.Combine(Application.streamingAssetsPath, fileName);
#elif UNITY_IPHONE
            var result = Path.Combine("file://" + Application.streamingAssetsPath, fileName);
#else
            var result = Path.Combine(Application.streamingAssetsPath, fileName);
#endif

            return result;
        }
    }
}
