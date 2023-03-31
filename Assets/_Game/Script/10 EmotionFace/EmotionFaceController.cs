using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Barnabus.EmotionMusic;

namespace Barnabus.EmotionFace
{
    public class EmotionFaceController : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private RectTransform canvas;
        [SerializeField]
        private NameController nameController;
        [SerializeField]
        private TextMeshProUGUI nameTitle;

        [Header("Asset")]
        [SerializeField]
        private EmotionFaceAsset asset;

        [Header("Prefab")]
        [SerializeField]
        private SelectableButton buttonPrefab;
        [SerializeField]
        private SelectableButton leftButtonPrefab;
        [SerializeField]
        private SelectableButton rightButtonPrefab;
        [SerializeField]
        private Item itemPrefab;

        [Header("ButtonContainer")]
       /* [SerializeField]
        private Transform characterTypeButtonContainer;*/
        [SerializeField]
        private Transform characterButtonContainer;
        [SerializeField]
        private Transform itemTypeButtonContainer;
        [SerializeField]
        private Transform itemButtonContainer;

        [Header("List")]
        [SerializeField]
        private GameObject characterList;
        [SerializeField]
        private GameObject itemList;
        [SerializeField]
        private GameObject itemTypeList;

        [Header("Bases&Color")]
        [SerializeField]
        private SelectableButton characterBaseButton;
        [SerializeField]
        private SelectableButton backgroundColorButton;
        [SerializeField]
        private SelectableButton characterColorButton;

        [Header("Picture")]
        [SerializeField]
        private Image pictureBackground;
        [SerializeField]
        private Image pictureCharacter;
        [SerializeField]
        private Transform itemContainer;
        //[Range(0.5f, 1f)]
      /*  [SerializeField]
        private float deleteItemSensitivity;*/
        [SerializeField]
        private RectTransform deleteItemIcon;
       /* [SerializeField]
        private GameObject deleteHint;*/
        [SerializeField]
        private GameObject layerToolBar;
        [SerializeField]
        private Material itemOutlineMaterial;

        [Header("Export")]
        [SerializeField]
        private GameObject exportCanvas;
        [SerializeField]
        private RectTransform polaroidRectTransform;
        [SerializeField]
        private RawImage exportPictureRawImage;
        [SerializeField]
        private TextMeshProUGUI exportPictureName;

        [Header("File")]
        [SerializeField]
        private FileController fileController;

        private PictureInfo pictureInfo = new PictureInfo();
        private List<SelectableButton> characterButtons = new List<SelectableButton>();
        private List<SelectableButton> itemButtons = new List<SelectableButton>();

        
        private Item characterItem;
        private SelectableButton selectedItemTypeButton = null;
        private SelectableButton selectedCharacterTypeButton = null;
        private string selectedCharacterName;
        private SelectableButton defaultCharacterTypeButton;

        //private Rect pictureRect;
        private RectTransform pictureRectTransform;

        private void Start()
        {
            DataManager.LoadCharacterData();

            pictureRectTransform = pictureBackground.GetComponent<RectTransform>();

            //characterList.SetActive(false);
            itemList.SetActive(false);
            itemTypeList.SetActive(false);
            layerToolBar.SetActive(false);

            //GenerateCharacterTypeButtons();
            GenerateItemTypeButtons();

            SetDefaultCharacter();

            selectedBackgroundColorIndex = 0;
            selectedCharacterColorIndex = 1;
            SetBackgroundColor();
            SetCharacterColor();

            nameTitle.text = "";
            characterItem = pictureCharacter.GetComponent<Item>();
            characterItem.info.itemName = "Character";
            characterItem.info.layer = 0;
            items.Add(characterItem);

            //OnClick_CharacterType(defaultCharacterTypeButton);
            /*OnClick_Character(characterButtons[0]);
            OnClick_SelectColorTarget(0);
            OnClick_BackgroundColor(itemButtons[0]);*/

            characterBaseButton.backgroundImage.color = Color.black;
            backgroundColorButton.backgroundImage.color = Color.black;
            characterColorButton.backgroundImage.color = Color.black;

            //DialogController.ShowDialog(DialogController.StringAsset.emotionFaceStartDialog, () => nameController.ShowNameSelector());
        }

        private void Update()
        {

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                TouchManager.SetPosition1();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                StartScaleItem();
                StartRotateItem();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                StopScaleItem();
                StopRotateItem();
            }
#elif UNITY_ANDROID || UNITY_IOS
            if(Input.touchCount == 2)
            {
                if(Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    StartScaleItem();
                    StartRotateItem();
                }
            }
            else if(isScalingItem && Input.touchCount < 2)
            {
                StopScaleItem();
                StopRotateItem();
            }
#endif
            if (isMovingItem)
            {
                MoveItem();
                /*DeleteItemDetect();
                if (isReadyDelete) SetDeleteIconPosition();*/

               //deleteHint.SetActive(!deleteItemIcon.gameObject.activeSelf);
            }
            //else if (isReadyDelete) HideDeleteIcon();

            if (isScalingItem) ScaleItem();
            if (isRotatingItem) RotateItem();
            layerToolBar.SetActive(currentEditItem);

            if (currentEditItem != null)
            {
                ShowDeleteIcon();
            }
            else HideDeleteIcon();
            
        }

        public void ClearList()
        {
            if(characterButtonContainer.childCount==0)
            {
                return;
            }
            else
            for (int i = 0; i < characterButtonContainer.childCount; i++)
            {
                //Destroy(characterList.transform.GetChild(i).gameObject);
                characterButtonContainer.GetChild(i).gameObject.SetActive(false);
            }

            if(characterColorButton.backgroundImage.color==Color.black)
            {
                characterColorButton.backgroundImage.color = Color.white;
                backgroundColorButton.backgroundImage.color = Color.white;
                characterBaseButton.backgroundImage.color = Color.white;
            }

        }



        #region Character
        private void SetDefaultCharacter()
        {
            pictureInfo.characterTypeName = asset.GetCharacterType(0).Name;
            /*pictureInfo.characterName = asset.GetCharacterType(0).Elements[0].Name;
            selectedCharacterName = asset.GetCharacterType(0).Elements[0].Name;
            pictureCharacter.sprite = asset.GetCharacterType(0).Elements[0].Sprite;*/
            pictureCharacter.sprite = asset.GetCharacterType(0).Sprite;
        }

        public void GenerateCharacterTypeButtons()
        {
            ClearList();
            characterList.SetActive(true);
            itemList.SetActive(false);

            for (int i = 0; i < characterButtons.Count; i++)
            {
                Destroy(characterButtons[i].gameObject);

                characterButtons.Clear();
            }

            SelectableButton newButton;
            for (int i = 0; i < asset.CharacterTypeCount; i++)
            {
                newButton = Instantiate(leftButtonPrefab, characterButtonContainer);
                newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                newButton.buttonIcon.sprite = asset.GetCharacterType(i).Icon;
                newButton.parameter = asset.GetCharacterType(i).Name;
                newButton.onClick = OnClick_CharacterType;

                if (i == 0) defaultCharacterTypeButton = newButton;
            }
        }

        private void RefreshCharacterButton(string typeName)
        {
            for (int i = 0; i < characterButtons.Count; i++) Destroy(characterButtons[i].gameObject);
            characterButtons.Clear();

         /*   SelectableButton newButton;
            TypeAsset characterType = asset.GetCharacterType(typeName);
            for (int i = 0; i < characterType.Elements.Count; i++)
            {
                newButton = Instantiate(buttonPrefab, characterButtonContainer);
                newButton.name = "CharacterButton";
                if (characterType.Elements[i].Name == selectedCharacterName) newButton.backgroundImage.sprite = asset.buttonSelectedSprite;
                else newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                if (asset.IsCharacterUnlocked(int.Parse(characterType.Elements[i].Name)))
                {
                    newButton.onClick = OnClick_Character;
                    newButton.buttonIcon.sprite = characterType.Elements[i].Icon;
                    newButton.parameter = characterType.Elements[i].Name;
                }
                else
                    newButton.buttonIcon.sprite = characterType.Elements[i].LockedIcon;

                characterButtons.Add(newButton);*/

                SelectableButton newButton;
            //TypeAsset characterType = asset.GetCharacterType(typeName);
            CharacterAsset characterType = asset.GetCharacterType(typeName);
                for (int i = 0; i < characterType.Name.Length; i++)
                {
                    newButton = Instantiate(buttonPrefab, characterButtonContainer);
                    newButton.name = "CharacterButton";
                    if (characterType.Name == selectedCharacterName) newButton.backgroundImage.sprite = asset.buttonSelectedSprite;
                    else newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                    if (asset.IsCharacterUnlocked(int.Parse(characterType.Name)))
                    {
                        newButton.onClick = OnClick_Character;
                        newButton.buttonIcon.sprite = characterType.Icon;
                        newButton.parameter = characterType.Name;
                    }
                    else
                        newButton.buttonIcon.sprite = characterType.LockedIcon;

                    characterButtons.Add(newButton);
                }
        }

        private void OnClick_CharacterType(SelectableButton clickedButton)
        {
            //if(clickedButton == selectedCharacterTypeButton) characterList.SetActive(!characterList.activeSelf);
            if (clickedButton == selectedCharacterTypeButton)
            {
                characterList.SetActive(false);
                clickedButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                selectedCharacterTypeButton = null;
            }
            else
            {
                if (selectedCharacterTypeButton) selectedCharacterTypeButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                clickedButton.backgroundImage.sprite = asset.buttonSelectedSprite;
                selectedCharacterTypeButton = clickedButton;

                RefreshCharacterButton(clickedButton.parameter);
                characterList.SetActive(true);
            }
        }

        private void OnClick_Character(SelectableButton clickedButton)
        {
            for (int i = 0; i < characterButtons.Count; i++)
            {
                if (characterButtons[i].parameter == selectedCharacterName)
                    characterButtons[i].backgroundImage.sprite = asset.buttonUnselectedSprite;
            }
            clickedButton.backgroundImage.sprite = asset.buttonSelectedSprite;
            selectedCharacterName = clickedButton.parameter;

            //pictureCharacter.sprite = asset.GetCharacterType(selectedCharacterTypeButton.parameter).GetElement(clickedButton.parameter).Sprite;
            SetCharacterColor();
            if (selectedItemTypeButton == characterColorButton)
            {
                for (int i = 0; i < itemButtons.Count; i++)
                {
                    itemButtons[i].buttonIcon.sprite = pictureCharacter.sprite;
                }
            }

            pictureInfo.characterTypeName = selectedCharacterTypeButton.parameter;
            pictureInfo.characterName = clickedButton.parameter;
            //Save
        }
        #endregion

        #region Item
        private void GenerateItemTypeButtons()
        {
            SelectableButton newButton;
            for (int i = 0; i < asset.ItemTypeCount; i++)
            {
                newButton = Instantiate(rightButtonPrefab, itemTypeButtonContainer);
                // newButton.backgroundImage
                newButton.backgroundImage.sprite = asset.rightButtonUnselectedSprite;
                newButton.buttonIcon.sprite = asset.GetItemType(i).Icon;
                newButton.parameter = asset.GetItemType(i).Name;
                newButton.onClick = OnClick_ItemType;
            }
        }

        private void RefreshItemButton(string typeName)
        {
            for (int i = 0; i < itemButtons.Count; i++) Destroy(itemButtons[i].gameObject);
            itemButtons.Clear();

            SelectableButton newButton;
            TypeAsset itemType = asset.GetItemType(typeName);
            for (int i = 0; i < itemType.Elements.Count; i++)
            {
                newButton = Instantiate(buttonPrefab, itemButtonContainer);
                newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                newButton.buttonIcon.sprite = itemType.Elements[i].Icon;
                newButton.parameter = itemType.Elements[i].Name;
                newButton.onClick = OnClick_Item;

                itemButtons.Add(newButton);
            }
        }

        private void OnClick_ItemType(SelectableButton clickedButton)
        {
            ClearList();
            characterList.SetActive(false);

            //if (clickedButton == selectedItemTypeButton) itemList.SetActive(!itemList.activeSelf);
            if (clickedButton == selectedItemTypeButton)
            {
                //itemList.SetActive(false);
                clickedButton.backgroundImage.sprite = asset.rightButtonUnselectedSprite;
                selectedItemTypeButton = null;
            }
            else
            {
                if (selectedItemTypeButton) selectedItemTypeButton.backgroundImage.sprite = asset.rightButtonUnselectedSprite;
                clickedButton.backgroundImage.sprite = asset.rightButtonSelectedSprite;
                selectedItemTypeButton = clickedButton;

                RefreshItemButton(clickedButton.parameter);
                itemList.SetActive(true);
            }
        }

        private void OnClick_Item(SelectableButton clickedButton)
        {
            GenerateItem(selectedItemTypeButton.parameter, clickedButton.parameter);
        }
        #endregion

        #region Edit Picture
        private List<Item> items = new List<Item>();
        private Item currentEditItem = null;

        private bool isReadyDelete = false;
        private Vector2 originPositionRatio;

        /* private void DeleteItemDetect()
         {
             if (currentEditItem == null) return;

             originPositionRatio = GetPositionRatio(false);

             if (originPositionRatio.x >= 1.1f + (1f - deleteItemSensitivity)) ShowDeleteIcon();
             else if (originPositionRatio.x <= -1.1f - (1f - deleteItemSensitivity)) ShowDeleteIcon();
             else if (originPositionRatio.y >= 1.1f + (1f - deleteItemSensitivity)) ShowDeleteIcon();
             else if (originPositionRatio.y <= -1.1f - (1f - deleteItemSensitivity)) ShowDeleteIcon();
             else HideDeleteIcon();
         }*/

        private void ShowDeleteIcon()
        {
            deleteItemIcon.gameObject.SetActive(true);
            //SetDeleteIconPosition();
            isReadyDelete = true;
        }

        private void HideDeleteIcon()
        {
            deleteItemIcon.gameObject.SetActive(false);
            isReadyDelete = false;
        }

        //private void SetDeleteIconPosition() { deleteItemIcon.anchoredPosition = GetUiPosition(); }

        public void DeleteItem()
        {
            if (currentEditItem == null) return;

            items.Remove(currentEditItem);
            Destroy(currentEditItem.gameObject);
            currentEditItem = null;

            //StartCoroutine(SetItemsLayer());

            //HideDeleteIcon();
        }

        public void OnClick_LayerDown()
        {
            if (currentEditItem == null) return;
            if (currentEditItem.info.layer <= 0) return;

            currentEditItem.info.layer--;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].info.layer == currentEditItem.info.layer && items[i] != currentEditItem)
                    items[i].info.layer++;
            }
            RefreshItemLayer();
        }

        public void OnClick_LayerUp()
        {
            if (currentEditItem == null) return;
            if (currentEditItem.info.layer >= items.Count - 1) return;

            currentEditItem.info.layer++;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].info.layer == currentEditItem.info.layer && items[i] != currentEditItem)
                    items[i].info.layer--;
            }
            RefreshItemLayer();
        }

        private void RefreshItemLayer()
        {
            for (int i = 0; i < items.Count; i++) items[i].transform.SetSiblingIndex(items[i].info.layer);
        }

        IEnumerator SetItemsLayer()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < items.Count; i++) itemContainer.GetChild(i).GetComponent<Item>().info.layer = i;
        }

        private bool isRotatingItem = false;
        private float firstRotataion;
        private float firstAngle;

        private void StartRotateItem()
        {
            if (currentEditItem == null) return;

            firstRotataion = currentEditItem.info.rotation;
            firstAngle = TouchManager.Angle();
            isRotatingItem = true;
        }

        private void RotateItem()
        {
            currentEditItem.SetRotation(firstRotataion + (TouchManager.Angle() - firstAngle));
        }

        private void StopRotateItem()
        {
            isRotatingItem = false;
        }

        private bool isScalingItem = false;
        private float firstScale;
        private float firstDistance;

        private void StartScaleItem()
        {
            if (currentEditItem == null) return;

            firstScale = currentEditItem.info.scale;
            firstDistance = TouchManager.Distance();
            isScalingItem = true;
        }

        private void ScaleItem()
        {
            currentEditItem.SetScale(firstScale * (TouchManager.Distance() / firstDistance));
        }

        private void StopScaleItem()
        {
            isScalingItem = false;
        }

        private bool isMovingItem = false;
        private Vector2 positionRatioOffset;

        private void OnPointerDown_Item(Item item)
        {
            if (currentEditItem) currentEditItem.image.material = null;
            currentEditItem = item;
            currentEditItem.image.material = itemOutlineMaterial;
            positionRatioOffset = GetPositionRatio() - currentEditItem.rectTransform.anchoredPosition * 2f / pictureRectTransform.sizeDelta;
            isMovingItem = true;
        }

        private void OnPointerUp_Item(Item item)
        {
            isMovingItem = false;
            //if (isReadyDelete) DeleteItem();
        }

        private void MoveItem()
        {
            currentEditItem.SetPosition(GetPositionRatio() - positionRatioOffset);
        }

        private void GenerateItem(string itemTypeName, string itemName)
        {
            ClearList();

            Item item = Instantiate(itemPrefab, itemContainer);
            item.SetCallbacks(OnPointerDown_Item, OnPointerUp_Item);
            item.SetPictureRect(pictureRectTransform);
            item.SetPosition(Vector2.zero);
            item.SetScale(0.5f);
            item.image.sprite = asset.GetItemType(itemTypeName).GetElement(itemName).Sprite;
            item.image.SetNativeSize();
            item.info.layer = items.Count;
            item.info.typeName = itemTypeName;
            item.info.itemName = itemName;
            items.Add(item);

            if (currentEditItem) currentEditItem.image.material = null;
            currentEditItem = item;
            currentEditItem.image.material = itemOutlineMaterial;
        }

        private Vector2 originRatio;
        private Vector2 position;
        private Vector2 positionRatio;

        private Vector2 GetUiPosition()
        {
            originRatio = mainCamera.ScreenToViewportPoint(TouchManager.Position());
            position = canvas.rect.size * originRatio - canvas.rect.size / 2f;
            return position;
        }

        private Vector2 GetPositionRatio(bool isCheckRange = true)
        {
            originRatio = mainCamera.ScreenToViewportPoint(TouchManager.Position());
            position = canvas.rect.size * originRatio - canvas.rect.size / 2f;
            positionRatio = (position - pictureRectTransform.anchoredPosition) * 2f / pictureRectTransform.sizeDelta;
            if (isCheckRange)
            {
                positionRatio.x = (positionRatio.x > 1) ? 1 : ((positionRatio.x < -1) ? -1 : positionRatio.x);
                positionRatio.y = (positionRatio.y > 1) ? 1 : ((positionRatio.y < -1) ? -1 : positionRatio.y);
            }
            return positionRatio;
        }
        #endregion

        #region Color
        public enum ColorTarget { Background, Character }

        private ColorTarget colorTarget;
        private int selectedBackgroundColorIndex;
        private int selectedCharacterColorIndex;

        public void OnClick_SelectColorTarget(int target)
        {
            itemList.SetActive(false);
            characterList.SetActive(true);

            colorTarget = (ColorTarget)target;
            if (colorTarget == ColorTarget.Background && selectedItemTypeButton == backgroundColorButton)
            {
                //itemList.SetActive(false);
                backgroundColorButton.backgroundImage.sprite = asset.leftButtonUnselectedSprite;
                selectedItemTypeButton = null;
            }
            else if (colorTarget == ColorTarget.Character && selectedItemTypeButton == characterColorButton)
            {
                //itemList.SetActive(false);
                characterColorButton.backgroundImage.sprite = asset.leftButtonUnselectedSprite;
                selectedItemTypeButton = null;
            }
            else
            {
                if (selectedItemTypeButton) selectedItemTypeButton.backgroundImage.sprite = asset.leftButtonUnselectedSprite;
                //itemList.SetActive(true);

                GenerateColorButton(colorTarget);
                switch (colorTarget)
                {
                    case ColorTarget.Background:
                        selectedItemTypeButton = backgroundColorButton;
                        break;
                    case ColorTarget.Character:
                        selectedItemTypeButton = characterColorButton;
                        break;
                }
                selectedItemTypeButton.backgroundImage.sprite = asset.leftButtonSelectedSprite;
            }
        }

        private void GenerateColorButton(ColorTarget colorTarget)
        {
            ClearList();

         /*   for (int i = 0; i < itemButtons.Count; i++)
            {
                Destroy(itemButtons[i].gameObject);
                itemButtons.Clear();
            }*/
                

            SelectableButton newButton;
            for (int i = 0; i < asset.colors.Count; i++)
            {
                newButton = Instantiate(buttonPrefab, characterButtonContainer);
                newButton.name = colorTarget.ToString() + "colorBTN";
                switch (colorTarget)
                {
                    case ColorTarget.Background:
                        if (i == selectedBackgroundColorIndex) newButton.backgroundImage.sprite = asset.buttonSelectedSprite;
                        else newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                        newButton.buttonIcon.sprite = asset.backgroundButtonImage;
                        newButton.onClick = OnClick_BackgroundColor;
                        break;
                    case ColorTarget.Character:
                        if (i == selectedCharacterColorIndex) newButton.backgroundImage.sprite = asset.buttonSelectedSprite;
                        else newButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
                        newButton.buttonIcon.sprite = pictureCharacter.sprite;
                        newButton.onClick = OnClick_CharacterColor;
                        break;
                }
                newButton.buttonIcon.color = asset.colors[i];
                newButton.parameter = i.ToString();

                itemButtons.Add(newButton);
            }
        }

        private void OnClick_BackgroundColor(SelectableButton clickedButton)
        {
            for (int i = 0; i < itemButtons.Count; i++)
            {
                if (itemButtons[i].parameter == selectedBackgroundColorIndex.ToString())
                    itemButtons[i].backgroundImage.sprite = asset.buttonUnselectedSprite;
            }
            clickedButton.backgroundImage.sprite = asset.buttonSelectedSprite;
            selectedBackgroundColorIndex = int.Parse(clickedButton.parameter);
            SetBackgroundColor();
        }

        private void OnClick_CharacterColor(SelectableButton clickedButton)
        {
            for (int i = 0; i < itemButtons.Count; i++)
            {
                if (itemButtons[i].parameter == selectedCharacterColorIndex.ToString())
                    itemButtons[i].backgroundImage.sprite = asset.buttonUnselectedSprite;
            }
            clickedButton.backgroundImage.sprite = asset.buttonSelectedSprite;
            selectedCharacterColorIndex = int.Parse(clickedButton.parameter);
            SetCharacterColor();
        }

        private void SetBackgroundColor()
        {
            Color color = asset.colors[selectedBackgroundColorIndex];
            pictureInfo.backgroundColor = new Vector3(color.r, color.g, color.b);
            pictureBackground.color = color;
        }

        private void SetCharacterColor()
        {
            Color color = asset.colors[selectedCharacterColorIndex];
            pictureInfo.characterColor = new Vector3(color.r, color.g, color.b);
            pictureCharacter.color = color;
        }

        private void SetSelectedBackgroundColorIndex()
        {
            selectedBackgroundColorIndex = asset.colors.FindIndex(x => (x.r == pictureInfo.backgroundColor.x) &&
                                                                       (x.g == pictureInfo.backgroundColor.y) &&
                                                                       (x.b == pictureInfo.backgroundColor.z));
        }

        private void SetSelectedCharacterColorIndex()
        {
            selectedCharacterColorIndex = asset.colors.FindIndex(x => (x.r == pictureInfo.characterColor.x) &&
                                                                      (x.g == pictureInfo.characterColor.y) &&
                                                                      (x.b == pictureInfo.characterColor.z));
        }
        #endregion

        #region Export
        public void ExportPicture()
        {
            if (currentEditItem) currentEditItem.image.material = null;
            currentEditItem = null;

            if (nameController.CurrentMoodQuest >= DataManager.MoodQuestLevel)
                DataManager.MoodQuestLevel = nameController.CurrentMoodQuest + 1;

            StartCoroutine(WaitForCapturePicture());
        }

        public void CapturePolaroid()
        {
            StartCoroutine(WaitForCapturePolaroid());
        }

        public void CloseExportCanvas()
        {
            exportCanvas.SetActive(false);
            ShowEndDialog();
        }

        private void ShowEndDialog()
        {
            DialogController.ShowDialog(DialogController.StringAsset.emotionFaceEndDialog, ShowAward);
        }

        private void ShowAward()
        {
            AwardController.SetPotionSprite(Game.GameSettings.EmotionFacePotionType);
            AwardController.ShowAward(3, 3, () => SceneTransit.LoadSceneAsync("MainScene"),
                                           null,
                                           () => SceneTransit.LoadSceneAsync("EmotionFace"));

            DataManager.LoadPotions();
            DataManager.Potions.AddPotion(Game.GameSettings.EmotionFacePotionType, 3);
            DataManager.SavePotions();
        }

        private IEnumerator WaitForCapturePicture()
        {
            Vector2 screenOffset = new Vector2(Mathf.Abs(Screen.width - CameraFit.screenWidth), Mathf.Abs(Screen.height - CameraFit.screenHeight)) / 2f;
            float screenScale = (float)CameraFit.screenWidth / canvas.sizeDelta.x;
            Rect pictureRect = new Rect(screenOffset + (pictureRectTransform.offsetMin + canvas.sizeDelta / 2f) * screenScale,
                                        pictureRectTransform.sizeDelta * screenScale);
            yield return StartCoroutine(fileController.CapturePicture(pictureRect));

            exportPictureRawImage.texture = fileController.picture;
            exportPictureName.text = pictureInfo.fileName;
            exportCanvas.SetActive(true);
        }

        private IEnumerator WaitForCapturePolaroid()
        {
            Vector2 screenOffset = new Vector2(Mathf.Abs(Screen.width - CameraFit.screenWidth), Mathf.Abs(Screen.height - CameraFit.screenHeight)) / 2f;
            float screenScale = (float)CameraFit.screenWidth / canvas.sizeDelta.x;
            Rect polaroidRect = new Rect(screenOffset + (polaroidRectTransform.offsetMin * polaroidRectTransform.localScale + canvas.sizeDelta / 2f) * screenScale,
                                        polaroidRectTransform.sizeDelta * screenScale * polaroidRectTransform.localScale);
            yield return StartCoroutine(fileController.CapturePolaroid(polaroidRect));
        }
        #endregion

        #region File
        public void SetFileName(string name)
        {
            pictureInfo.fileName = name;
            nameTitle.text = name;
        }

        public void OnClick_SaveData()
        {
            SetItemsToPictureInfo();
            pictureInfo.moodQuestLevel = nameController.CurrentMoodQuest;
            fileController.SaveFile(pictureInfo);
        }

        public void OnClick_ConfirmLoadData()
        {
            pictureInfo = fileController.LoadSelectedData();
            nameController.CurrentMoodQuest = pictureInfo.moodQuestLevel;
            nameTitle.text = pictureInfo.fileName;
            InitializeByInfo();
        }

        private void InitializeByInfo()
        {
            SetSelectedBackgroundColorIndex();
            SetSelectedCharacterColorIndex();
            SetBackgroundColor();
            SetCharacterColor();

            if (selectedCharacterTypeButton) selectedCharacterTypeButton.backgroundImage.sprite = asset.buttonUnselectedSprite;
            selectedCharacterTypeButton = null;
            characterList.SetActive(false);

            if (pictureInfo.characterName == "") SetDefaultCharacter();
            selectedCharacterName = pictureInfo.characterName;
            pictureCharacter.sprite = asset.GetCharacterType(pictureInfo.characterTypeName).Sprite;

            if (selectedItemTypeButton) selectedItemTypeButton.backgroundImage.sprite = asset.rightButtonUnselectedSprite;
            selectedItemTypeButton = null;
            itemList.SetActive(false);

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].info.itemName != "Character") Destroy(items[i].gameObject);
            }
            items.Clear();

            Item item;
            for (int i = 0; i < pictureInfo.items.Count; i++)
            {
                if (pictureInfo.items[i].itemName == "Character")
                {
                    characterItem.info.layer = pictureInfo.items[i].layer;
                    characterItem.transform.SetAsLastSibling();
                    items.Add(characterItem);
                }
                else
                {
                    item = Instantiate(itemPrefab, itemContainer);
                    item.SetCallbacks(OnPointerDown_Item, OnPointerUp_Item);
                    item.SetPictureRect(pictureRectTransform);
                    item.image.sprite = asset.GetItemType(pictureInfo.items[i].typeName).GetElement(pictureInfo.items[i].itemName).Sprite;
                    item.image.SetNativeSize();
                    item.Initialize(pictureInfo.items[i]);
                    items.Add(item);
                }
            }
            RefreshItemLayer();
            currentEditItem = null;
        }

        private void SetItemsToPictureInfo()
        {
            pictureInfo.items.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                pictureInfo.items.Add(items[i].info);
            }
        }
        #endregion

        #region Share
        public void OnClick_SharePicture()
        {
            fileController.SharePicture();
        }
        #endregion
    }
}