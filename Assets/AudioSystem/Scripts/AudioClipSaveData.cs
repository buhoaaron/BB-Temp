using System.Collections.Generic;

namespace AudioSystem
{
    public class AudioClipSaveData
    {
        public string Name;
        public string GUID;

        public AudioClipSaveData(string name, string id)
        {
            Name = name;
            GUID = id;
        }
    }
}