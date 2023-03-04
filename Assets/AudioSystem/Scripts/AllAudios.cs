using System.Collections.Generic;

namespace AudioSystem
{
    public class AllAudios
    {
        public List<AudioClipData> clips= new List<AudioClipData>();

        /// <summary>
        /// 轉成儲存用的結構
        /// </summary>
        public List<AudioClipSaveData> ConvertSaveData()
        {
            return clips.ConvertAll((clipData) => new AudioClipSaveData(clipData.Name, clipData.GUID));
        }
    }
}
