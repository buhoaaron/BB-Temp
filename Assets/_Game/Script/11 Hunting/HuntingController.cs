using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomAudio;
namespace Barnabus.Hunting
{
    public class HuntingController : MonoBehaviour
    {
        //[SerializeField]
        //private int objectAmount;
        [SerializeField]
        private int targetAmount;
        [SerializeField]
        private int seconds;

        [Space(10)]
        [SerializeField]
        private HuntingAsset asset;
        [SerializeField]
        private MapGenerator mapGenerator;

        [Header("Map Layer")]
        [SerializeField]
        private RectTransform mainMapLayer;
        [SerializeField]
        private List<MapLayer> mapLayers;

        [Header("TargetHint")]
        [SerializeField]
        private Transform targetHintContainer;
        [SerializeField]
        private TargetHint targetHintPrefab;

        [Header("Time")]
        [SerializeField]
        private TMPro.TextMeshProUGUI timeText;

        [Header("Character Simple Info")]
        [SerializeField]
        private GameObject simpleInfo;
        [SerializeField]
        private Animator simpleInfoAnimator;
        [SerializeField]
        private Image simpleInfo_characterImage;
        [SerializeField]
        private TMPro.TextMeshProUGUI simpleInfo_characterID;
        [SerializeField]
        private TMPro.TextMeshProUGUI simpleInfo_characterName;
        [SerializeField]
        private RectTransform elePosTransform;
        [SerializeField]
        private List<RectTransform> elePoses;
        [SerializeField]
        private AudioPlayer simpleInfoAudioPlayer;

        [Header("Result")]
        [SerializeField]
        private GameObject resultCanvas;
        [SerializeField]
        private TMPro.TextMeshProUGUI resultText;
        [SerializeField]
        private Transform resultTargetContainer;
        [SerializeField]
        private ResultTargetButton resultTargetPrefab;
        [SerializeField]
        public CardContent cardContent;
        [SerializeField]
        private TMPro.TextMeshProUGUI resultTitle;

        private List<int> targetList = new List<int>();
        private List<TargetHint> targetHintList = new List<TargetHint>();
        private List<ResultTargetButton> resultTargetButtonList = new List<ResultTargetButton>();

        private int foundCount = 0;
        private bool isTimePause;

        public GameObject clickToStartGroup;

        public static HuntingController Instance;

        private void OnEnable()
        {
            Instance = this;

            Debug.Log("Time stop");
            isTimePause = true;
            clickToStartGroup.SetActive(true);
        }

        public void GameStart()
        {
            isTimePause = false;
            clickToStartGroup.SetActive(false);
        }

        private void OnDisable()
        {
            isTimePause = true;
            clickToStartGroup.SetActive(false);
        }

        private void Start()
        {
            // isTimePause = false;
            GenerateRandomTarget();
            //mapGenerator.GenerateMap(asset, objectAmount, targetList, OnClickCharacter);
            mapGenerator.GenerateMap(asset, targetList, OnClickCharacter);

            StartCoroutine(TimerStart(seconds));
        }

        public void Pause(bool state)
        {
            isTimePause = state;
        }

        public void OnMainLayerMoving()
        {
            SetMapLayers();
        }

        private void SetMapLayers()
        {
            for (int i = 0; i < mapLayers.Count; i++)
            {
                mapLayers[i].SetPosition(mainMapLayer.anchoredPosition.x);
            }
        }

        private void GenerateRandomTarget()
        {
            int targetID;
            for (int i = 0; i < targetAmount; i++)
            {
                do
                {
                    targetID = Random.Range(1, asset.barnabusList.Count + 1);
                } while (targetList.Exists(x => x == targetID));

                targetList.Add(targetID);
            }

            TargetHint targetHint;
            ResultTargetButton resultTargetButton;
            for (int i = 0; i < targetList.Count; i++)
            {
                targetHint = Instantiate(targetHintPrefab, targetHintContainer);
                targetHint.characterID = targetList[i];
                targetHint.backgroundImage.sprite = asset.notFoundSprite;
                targetHint.characterImage.sprite = asset.barnabusList[targetList[i]].CharacterImage;
                targetHintList.Add(targetHint);

                resultTargetButton = Instantiate(resultTargetPrefab, resultTargetContainer);
                resultTargetButton.characterID = targetList[i];
                resultTargetButton.backgroundImage.sprite = asset.notFoundSprite;
                resultTargetButton.characterImage.sprite = asset.barnabusList[targetList[i]].CharacterImage;
                resultTargetButton.characterImage.color = new Color(0, 0, 0, 1);
                resultTargetButton.onClick = null;
                resultTargetButtonList.Add(resultTargetButton);
            }
        }

        private void OnClickCharacter(int characterID)
        {
            targetHintList.Find(x => x.characterID == characterID).backgroundImage.sprite = asset.foundSprite;

            ResultTargetButton resultTargetButton;
            resultTargetButton = resultTargetButtonList.Find(x => x.characterID == characterID);
            resultTargetButton.backgroundImage.sprite = asset.foundSprite;
            resultTargetButton.characterImage.color = new Color(1, 1, 1, 1);
            resultTargetButton.onClick = OnClick_ResultTargetButton;

            ShowCharacterSimpleInfo(characterID);
            foundCount++;

            //if (foundCount == targetAmount) ShowResult();
        }

        // Show ID Card
        private void OnClick_ResultTargetButton(int characterID)
        {
            Debug.Log("Load detail information.");
            BarnabusList barnabusList = Resources.Load<BarnabusList>("BarnabusCard/BarnabusList");
            BarnabusScanScriptable barnabusScanScriptable = barnabusList[characterID];
            cardContent.ReadBarnabus(barnabusScanScriptable);
            cardContent.gameObject.SetActive(true);
        }

        public void OnClick_BackToHome()
        {
            SceneTransit.LoadSceneAsync("MainScene");
        }

        public void OnClick_Replay()
        {
            SceneTransit.LoadSceneAsync("Hunting");
        }

        IEnumerator TimerStart(float duration)
        {
            WaitForSeconds oneSecond = new WaitForSeconds(1);

            float timeLeft = duration;
            timeText.text = (int)(timeLeft / 60) + " : " + ((int)timeLeft) % 60;

            while (timeLeft > 0)
            {
                if (isTimePause) yield return null;
                else
                {
                    yield return oneSecond;
                    if (isTimePause) continue;

                    timeLeft -= 1;
                    timeText.text = (int)(timeLeft / 60) + " : " + ((int)timeLeft) % 60;
                }
            }

            timeText.text = "0 : 0";

            ShowResult();
        }

        private void ShowResult()
        {
            StopAllCoroutines();
            resultCanvas.SetActive(true);
            resultText.text = "Found " + foundCount + " Barnabus";
            if (foundCount >= 3)
            {
                resultTitle.text = "Hooray! You found them all!";
            }
            else
            {
                resultTitle.text = "Time’s up";
            }
        }

        #region SimpleInfo
        public void OnClick_CloseSimpleInfo()
        {
            isTimePause = false;
            simpleInfo.SetActive(false);

            if (foundCount == targetAmount) ShowResult();
        }

        // Simple Clip
        private void ShowCharacterSimpleInfo(int id)
        {
            Debug.Log("ID: is " + id);
            Debug.Log("Ele no is " + GetCharacterElePos(id).ToString());
            isTimePause = true;
            simpleInfo_characterID.text = GetCharacterElePos(id).ToString();
            simpleInfo_characterName.text = GetCharacterName(id);
            simpleInfo_characterImage.sprite = GetCharacterSprite(id);
            simpleInfo.SetActive(true);
            simpleInfoAnimator.Play("Appear", 0, 0);
            elePosTransform.anchoredPosition = elePoses[GetCharacterElePos(id) - 1].anchoredPosition;
            simpleInfoAudioPlayer.clip = GetCharacterClip(id);
            CustomAudio.AudioManager.instance.PlaySound(asset.barnabusList[id].huntingFoundSound);
        }

        private AudioClip GetCharacterClip(int id)
        {
            return asset.barnabusList[id].barnabusSound;
        }

        private int GetCharacterElePos(int id)
        {
            return int.Parse(asset.barnabusList[id].barnabusNo);
        }

        private string GetCharacterName(int id)
        {
            return asset.barnabusList[id].barnabusName;
        }

        private Sprite GetCharacterSprite(int id)
        {
            return asset.barnabusList[id].CharacterImage;
        }
        #endregion
    }
}