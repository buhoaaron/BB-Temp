using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataStream
{
    public enum AccessType { PlayerPrefs }
    public enum DataType { Json }

    public static class DataAccess
    {
        public static int AddData<T>(string key, Data<T> data, DataType dataType, AccessType accessType)
        {
            DataList<T> dataList = LoadDataList<T>(key, dataType, accessType);
            data.lastModifyTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            dataList.Add(data);
            SaveDataList(key, dataList, dataType, accessType);
            return dataList.Count - 1;
        }

        public static void RemoveData<T>(string key, int index, DataType dataType, AccessType accessType)
        {
            DataList<T> dataList = LoadDataList<T>(key, dataType, accessType);
            if (dataList.Count == 0) return;

            dataList.RemoveAt(index);
            SaveDataList(key, dataList, dataType, accessType);
        }

        public static void ModifyData<T>(string key, int index, Data<T> newData, DataType dataType, AccessType accessType)
        {
            DataList<T> dataList = LoadDataList<T>(key, dataType, accessType);
            if (dataList.Count <= index) return;

            newData.lastModifyTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            dataList[index] = newData;
            SaveDataList(key, dataList, dataType, accessType);
        }

        public static void RenameData<T>(string key, int index, string newName, DataType dataType, AccessType accessType)
        {
            DataList<T> dataList = LoadDataList<T>(key, dataType, accessType);
            if (dataList.Count <= index) return;

            dataList[index].name = newName;
            SaveDataList(key, dataList, dataType, accessType);
        }

        public static void SaveDataList<T>(string key, DataList<T> dataList, DataType dataType, AccessType accessType)
        {
            switch (accessType)
            {
                case AccessType.PlayerPrefs:
                    switch (dataType)
                    {
                        case DataType.Json:
                            PlayerPrefs.SetString(key, JsonUtility.ToJson(dataList));
                            break;
                    }
                    break;
            }
        }

        public static DataList<T> LoadDataList<T>(string key, DataType dataType, AccessType accessType)
        {
            switch (accessType)
            {
                case AccessType.PlayerPrefs:
                    string prefInfo = PlayerPrefs.GetString(key, "");
                    if (prefInfo == "") return new DataList<T>();

                    return dataType switch
                    {
                        DataType.Json => JsonUtility.FromJson<DataList<T>>(prefInfo),
                        _ => new DataList<T>(),
                    };
                default:
                    return new DataList<T>();
            }
        }
    }
}