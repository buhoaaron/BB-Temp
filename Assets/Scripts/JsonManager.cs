using UnityEngine;
using System.Collections.Generic;

using Newtonsoft.Json;

/// <summary>
/// Json檔案管理者
/// </summary>
public class JsonManager : MonoBehaviour, IBaseSystem
{
    public List<TextAsset> ListJson = new List<TextAsset>();

    #region BASE_API
    public void Init()
    {
        
    }
    public void SystemUpdate()
    {

    }
    public void Clear()
    {

    }
    #endregion

    public T DeserializeObject<T>(int jsonTextIndex)
    {
        return JsonConvert.DeserializeObject<T>(ListJson[jsonTextIndex].text);
    }
}