using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStream;
using System.IO;
using Game;
using UnityEngine.UI;

namespace Barnabus.EmotionFace
{
    public class FileController : MonoBehaviour
    {
        [SerializeField]
        private string fileKey;
        
        [Header("Load")]
        [SerializeField]
        private GameObject loadView;
        [SerializeField]
        private DataInfo dataInfoPrefab;
        [SerializeField]
        private Transform dataInfoContainer;
        [SerializeField]
        private GameObject loadConfirmButton;
        [SerializeField]
        private GameObject deleteButton;

        [Header("Debug")]
        [SerializeField]
        private TMPro.TextMeshProUGUI debugFilePath;

        [HideInInspector]
        public Texture2D picture;
        private Texture2D polaroid;
        private string folderPath = "/System/EmotionFace";

        private void Start()
        {
            if (!Directory.Exists(Application.persistentDataPath + folderPath)) 
                Directory.CreateDirectory(Application.persistentDataPath + folderPath);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.DeleteKey(fileKey);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                debugFilePath.text = Application.persistentDataPath + folderPath;
            }
        }

        public void SharePicture()
        {
            string filePath = Application.persistentDataPath + folderPath + "/Temp.png";

            byte[] bytes = polaroid.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);

            new NativeShare().AddFile(filePath)
                .AddTarget("com.facebook.katana")
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
                .Share();
        }

        public IEnumerator CapturePicture(Rect pictureRect)
        {
            yield return new WaitForEndOfFrame();

            picture = new Texture2D((int)pictureRect.width, (int)pictureRect.height, TextureFormat.RGB24, false);
            picture.ReadPixels(pictureRect, 0, 0);
            picture.Apply();
        }

        public IEnumerator CapturePolaroid(Rect polaroidRect)
        {
            yield return new WaitForEndOfFrame();

            polaroid = new Texture2D((int)polaroidRect.width, (int)polaroidRect.height, TextureFormat.RGB24, false);
            polaroidRect.height *= 1.02f;
            polaroid.ReadPixels(polaroidRect, 0, 0);
            polaroid.Apply();
        }

        public void SaveFile(PictureInfo pictureInfo)
        {
            int id = 0;
            while (File.Exists(Application.persistentDataPath + folderPath + "/Picture" + id + ".png")) id++;
            pictureInfo.pictureFilePath = Application.persistentDataPath + folderPath + "/Picture" + id + ".png";

            byte[] bytes = picture.EncodeToPNG();
            File.WriteAllBytes(pictureInfo.pictureFilePath, bytes);
            SaveFileToAlbum(pictureInfo.pictureFilePath, pictureInfo.fileName);

            DataAccess.AddData(fileKey, new Data<PictureInfo>(pictureInfo.fileName, pictureInfo), GameSettings.DataType, GameSettings.AccessType);
            Notifier.ShowAlert("Save success.");
        }

        private void SaveFileToAlbum(byte[] bytes, string fileName)
        {
            if (Application.platform == RuntimePlatform.Android)
                NativeGallery.SaveImageToGallery(bytes, "Barnabus", fileName);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return;
        }

        private void SaveFileToAlbum(string existingMediaPath, string fileName)
        {
            if (Application.platform == RuntimePlatform.Android)
                NativeGallery.SaveImageToGallery(existingMediaPath, "Barnabus", fileName);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return;
        }

        #region Load
        private int selectedInfoID = -1;
        private List<DataInfo> dataInfoList = new List<DataInfo>();

        public void OnClick_LoadData() { ShowLoadView(); }

        public PictureInfo LoadSelectedData()
        {
            Data<PictureInfo> data = DataAccess.LoadDataList<PictureInfo>(fileKey, GameSettings.DataType, GameSettings.AccessType)[selectedInfoID];
            Notifier.ShowAlert("Load success.");
            return data.data;
        }

        private void ShowLoadView()
        {
            if (selectedInfoID != -1) dataInfoList[selectedInfoID].SetSelected(false);
            selectedInfoID = -1;
            loadConfirmButton.SetActive(false);
            deleteButton.SetActive(false);

            RefreshDataInfoList();

            loadView.SetActive(true);
        }

        public void OnClick_DataInfo(int id)
        {
            if (selectedInfoID != -1) dataInfoList[selectedInfoID].SetSelected(false);
            selectedInfoID = id;
            dataInfoList[selectedInfoID].SetSelected(true);
            loadConfirmButton.SetActive(true);
            deleteButton.SetActive(true);

            Data<PictureInfo> data = DataAccess.LoadDataList<PictureInfo>(fileKey, GameSettings.DataType, GameSettings.AccessType)[selectedInfoID];
        }

        private void RefreshDataInfoList()
        {
            DataList<PictureInfo> dataList = DataAccess.LoadDataList<PictureInfo>(fileKey, GameSettings.DataType, GameSettings.AccessType);

            if(dataList.Count > 0)
            {
                for (int i = dataList.Count - 1; i >= 0; i--)
                {
                    if (!File.Exists(dataList[i].data.pictureFilePath))
                        dataList.RemoveAt(i);
                }
                DataAccess.SaveDataList(fileKey, dataList, GameSettings.DataType, GameSettings.AccessType);
            }
            
            if (dataInfoList.Count < dataList.Count)
            {
                int diff = dataList.Count - dataInfoList.Count;
                for (int i = 0; i < diff; i++)
                    dataInfoList.Add(Instantiate(dataInfoPrefab, dataInfoContainer));
            }
            else if (dataInfoList.Count > dataList.Count)
            {
                for (int i = 0; i < dataInfoList.Count; i++) Destroy(dataInfoList[i].gameObject);
                dataInfoList.Clear();
                for (int i = 0; i < dataList.Count; i++)
                    dataInfoList.Add(Instantiate(dataInfoPrefab, dataInfoContainer));
            }

            byte[] fileData;
            Texture2D texture;
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                dataInfoList[i].SetID(i);
                dataInfoList[i].SetInfo(dataList[i].name, dataList[i].lastModifyTime);
                dataInfoList[i].SetOnClickCallback(OnClick_DataInfo);
                dataInfoList[i].CancelRename();

                fileData = File.ReadAllBytes(dataList[i].data.pictureFilePath);
                texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                dataInfoList[i].transform.GetChild(0).GetComponent<RawImage>().texture = texture;
            }
        }

        public void OnClick_DeleteSelectedData()
        {
            Notifier.ShowConfirmView("Delete", "Do you want to delete this file?", DeleteSelectedData);
        }

        private void DeleteSelectedData()
        {
            File.Delete(LoadSelectedData().pictureFilePath);
            DataAccess.RemoveData<PictureInfo>(fileKey, selectedInfoID, GameSettings.DataType, GameSettings.AccessType);

            Destroy(dataInfoList[selectedInfoID].gameObject);
            dataInfoList.RemoveAt(selectedInfoID);

            RefreshDataInfoList();

            selectedInfoID = -1;
            loadConfirmButton.SetActive(false);
            deleteButton.SetActive(false);

            Notifier.ShowAlert("Delete success.");
        }
#endregion
    }
}