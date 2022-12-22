using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using TMPro;
using System;

namespace DataStream
{
    public class DataController : MonoBehaviour
    {
        [SerializeField]
        private GameObject fileView;
        private void CloseFileView() { fileView.SetActive(false); }

        private string key;
        private int currentDataIndex = -1;
        private string currentDataName;

        public void SetKey(string key) { this.key = key; }

        public void SetActions(Action saveDataAction, 
                               Action loadDataAction, 
                               Action deleteDataAction, 
                               Action renameDataAction, 
                               Action exportDataAction,
                               Action importDataAction)
        {
            this.saveDataAction = saveDataAction;
            this.loadDataAction = loadDataAction;
            this.deleteDataAction = deleteDataAction;
            this.renameDataAction = renameDataAction;
            this.exportDataAction = exportDataAction;
            this.importDataAction = importDataAction;
        }

        #region Save
        [Header("Save")]
        [SerializeField]
        private GameObject saveView;
        [SerializeField]
        private TMP_InputField dataNameInput;

        private bool isSaveAsNew;
        private Action saveDataAction;

        public void OnClick_SaveData()
        {
            if (currentDataIndex == -1) ShowSaveView();
            else
            {
                saveDataAction?.Invoke(); 
                CloseFileView();
            }
        }

        public void OnClick_SaveAsNew() 
        {
            isSaveAsNew = true;
            ShowSaveView(); 
        }

        public void OnClick_ConfirmSaveData() 
        {
            if (dataNameInput.text == "")
            {
                Notifier.ShowAlert("Please enter a file name.");
                return;
            }

            saveDataAction?.Invoke();
            OnClick_CloseSaveView();
            fileView.SetActive(false);
        }

        public void OnClick_CloseSaveView() 
        {
            isSaveAsNew = false;
            saveView.SetActive(false); 
        }

        private void ShowSaveView()
        {
            dataNameInput.text = "";
            saveView.SetActive(true);
        }

        public void SaveData<T>(T data, string fileName = "", bool isForceSaveNewFile = true)
        {
            if (currentDataIndex == -1 || isSaveAsNew || (fileName != "") && isForceSaveNewFile)
            {
                currentDataName = (fileName == "") ? dataNameInput.text : fileName;
                currentDataIndex = DataAccess.AddData<T>(key, new Data<T>(currentDataName, data), GameSettings.DataType, GameSettings.AccessType);
            }
            else DataAccess.ModifyData<T>(key, currentDataIndex, new Data<T>(currentDataName, data), GameSettings.DataType, GameSettings.AccessType);

            Notifier.ShowAlert("Save success.");
        }
        #endregion
        
        #region Load
        [Header("Load")]
        [SerializeField]
        private GameObject loadView;
        [SerializeField]
        private DataInfo dataInfoPrefab;
        [SerializeField]
        private Transform dataInfoContainer;

        [Space(10)]
        [SerializeField]
        private GameObject loadConfirmButton;
        [SerializeField]
        private GameObject renameConfirmButton;
        [SerializeField]
        private GameObject renameButton;
        [SerializeField]
        private GameObject deleteButton;

        private int selectedInfoID = -1;
        private List<DataInfo> dataInfoList = new List<DataInfo>();
        private Action loadDataAction;
        private Action deleteDataAction;
        private Action renameDataAction;

        public void OnClick_LoadData() { ShowLoadView(); }

        public void OnClick_ConfirmLoadData() 
        { 
            loadDataAction?.Invoke();
            OnClick_CloseLoadView();

            Notifier.ShowAlert("Load success.");
        }

        public void OnClick_CloseLoadView() { loadView.SetActive(false); }

        private void ShowLoadView()
        {
            if (selectedInfoID != -1) dataInfoList[selectedInfoID].SetSelected(false);
            selectedInfoID = -1;
            loadConfirmButton.SetActive(false);
            renameConfirmButton.SetActive(false);
            renameButton.SetActive(false);
            deleteButton.SetActive(false);

            RefreshDataInfoList();

            loadView.SetActive(true);
        }

        public void OnClick_DataInfo(int id) 
        {
            if(selectedInfoID != -1) dataInfoList[selectedInfoID].SetSelected(false);
            selectedInfoID = id;
            dataInfoList[selectedInfoID].SetSelected(true);
            loadConfirmButton.SetActive(true);
            renameButton.SetActive(true);
            deleteButton.SetActive(true);
        }

        public void OnClick_DeleteSelectedData()
        {
            Notifier.ShowConfirmView("Delete", "Do you want to delete this file?", deleteDataAction);
        }

        public void OnClick_RenameSelectedData()
        {
            dataInfoList[selectedInfoID].StartRename();
            loadConfirmButton.SetActive(false);
            renameButton.SetActive(false);
            deleteButton.SetActive(false);
            renameConfirmButton.SetActive(true);
        }

        public void OnClick_RenameConfirm()
        {
            dataInfoList[selectedInfoID].CompleteRename();
            renameDataAction?.Invoke();
            loadConfirmButton.SetActive(true);
            renameButton.SetActive(true);
            deleteButton.SetActive(true);
            renameConfirmButton.SetActive(false);
        }

        private void RefreshDataInfoList()
        {
            DataList<int> dataList = DataAccess.LoadDataList<int>(key, GameSettings.DataType, GameSettings.AccessType);
            if (dataInfoList.Count < dataList.Count)
            {
                int diff = dataList.Count - dataInfoList.Count;
                for (int i = 0; i < diff; i++)
                    dataInfoList.Add(Instantiate(dataInfoPrefab, dataInfoContainer));
            }
            else if(dataInfoList.Count > dataList.Count)
            {
                for (int i = 0; i < dataInfoList.Count; i++) Destroy(dataInfoList[i].gameObject);
                dataInfoList.Clear();
                for (int i = 0; i < dataList.Count; i++)
                    dataInfoList.Add(Instantiate(dataInfoPrefab, dataInfoContainer));
            }
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                dataInfoList[i].SetID(i);
                dataInfoList[i].SetInfo(dataList[i].name, dataList[i].lastModifyTime);
                dataInfoList[i].SetOnClickCallback(OnClick_DataInfo);
                dataInfoList[i].CancelRename();
            }
        }

        public T LoadSelectedData<T>()
        {
            Data<T> data = DataAccess.LoadDataList<T>(key, GameSettings.DataType, GameSettings.AccessType)[selectedInfoID];
            currentDataIndex = selectedInfoID;
            currentDataName = data.name;
            return data.data;
        }

        public void DeleteSelectedData<T>()
        {
            DataAccess.RemoveData<T>(key, selectedInfoID, GameSettings.DataType, GameSettings.AccessType);

            Destroy(dataInfoList[selectedInfoID].gameObject);
            dataInfoList.RemoveAt(selectedInfoID);

            RefreshDataInfoList();

            if (currentDataIndex == selectedInfoID) currentDataIndex = -1;
            else if (currentDataIndex > selectedInfoID) currentDataIndex -= 1;

            selectedInfoID = -1;
            loadConfirmButton.SetActive(false);
            renameButton.SetActive(false);
            deleteButton.SetActive(false);

            Notifier.ShowAlert("Delete success.");
        }

        public void RenameSelectedData<T>()
        {
            if (currentDataIndex == selectedInfoID) currentDataName = dataInfoList[selectedInfoID].GetNewName();
            DataAccess.RenameData<T>(key, selectedInfoID, dataInfoList[selectedInfoID].GetNewName(), GameSettings.DataType, GameSettings.AccessType);

            Notifier.ShowAlert("Rename success.");
        }
        #endregion

        #region Export
        [Header("Export")]
        [SerializeField]
        private GameObject exportView;
        [SerializeField]
        private TMP_InputField exportField;

        private Action exportDataAction;

        public void OnClick_Export()
        {
            exportDataAction?.Invoke();
            exportView.SetActive(true);
        }

        public void OnClick_CopyDataToClipboard()
        {
            GUIUtility.systemCopyBuffer = exportField.text;

            Notifier.ShowAlert("Copy success.");
        }

        public void OnClick_CloseExportView()
        {
            exportView.SetActive(false);
        }

        public void ExportData<T>(T data)
        {
            exportField.text = JsonUtility.ToJson(data);
        }
        #endregion

        #region Import
        [Header("Import")]
        [SerializeField]
        private GameObject importView;
        [SerializeField]
        private TMP_InputField importField;

        private Action importDataAction;

        public void OnClick_Import()
        {
            importField.text = "";
            importView.SetActive(true);
        }

        public void OnClick_ConfirmImport()
        {
            if (importField.text == "") return;
            importDataAction?.Invoke();
            OnClick_CloseImportView();

            Notifier.ShowAlert("Import success.");
        }

        public void OnClick_PasteDataFromClipboard()
        {
            importField.text = GUIUtility.systemCopyBuffer;
        }

        public void OnClick_CloseImportView()
        {
            importView.SetActive(false);
        }

        public T ImportData<T>()
        {
            T data;

            try
            {
                data = JsonUtility.FromJson<T>(importField.text);
            }
            catch (System.Exception)
            {
                Notifier.ShowAlert("Parse failed.");
                throw;
            }

            return data;
        }
        #endregion
    }
}