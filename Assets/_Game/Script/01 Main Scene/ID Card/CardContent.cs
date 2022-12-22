using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CustomAudio;
using Barnabus;
using Spine.Unity;

public class CardContent : MonoBehaviour
{
    public Image barnabusInLab;

    public Image background;
    public Image barnabus;
    public SkeletonGraphic skeletonGraphic;
    public TMP_Text element;
    public TMP_Text elementName;
    public TMP_Text elementNo;
    public TMP_Text eyebrow;
    public TMP_Text eyes;
    public TMP_Text mouth;
    public TMP_Text body;
    public TMP_Text soundDescribe;
    public Sprite barnabusSprite;
    public Transform elePos;
    public List<Transform> elePoses;
    public AudioPlayer audioPlayer;
    public GameObject soundBtn;

    [Header("Story")]
    public GameObject storyBtn;
    public AudioPlayer storyPlayer;


    [HideInInspector]
    public string elementColor;

    private void OnEnable()
    {
        if (barnabusInLab != null)
            barnabusInLab.sprite = barnabusSprite;
    }

    public void ReadBarnabus(BarnabusScanScriptable _barnabus)
    {
        background.sprite = _barnabus.GetCardImage;
        barnabus.sprite = _barnabus.GetBarnabusImage;
        if (_barnabus.IsUnlocked())
        {
            element.text = _barnabus.element;
            elementName.text = _barnabus.barnabusName;
            elementNo.text = _barnabus.barnabusNo;
            eyebrow.text = _barnabus.eyerbow;
            eyes.text = _barnabus.face;
            mouth.text = _barnabus.mouth;
            body.text = _barnabus.body;
            soundDescribe.text = _barnabus.soundDescribe;
            audioPlayer.clip = _barnabus.barnabusSound;
            soundBtn.SetActive(true);
            elePos.gameObject.SetActive(true);
            storyPlayer.clip = _barnabus.storySound;
            storyBtn.SetActive(true);
        }
        else
        {
            element.text = "";
            elementName.text = "";
            elementNo.text = "";
            eyebrow.text = "";
            eyes.text = "";
            mouth.text = "";
            body.text = "";
            soundDescribe.text = "";
            soundBtn.SetActive(false);
            elePos.gameObject.SetActive(false);
            storyBtn.SetActive(false);
        }
        elementColor = "#" + _barnabus.elementColor;

        if (_barnabus.skeletonMaterial == null)
        {
            barnabusSprite = _barnabus.GetBarnabusImage;
            barnabus.gameObject.SetActive(true);
            skeletonGraphic.gameObject.SetActive(false);
        }
        else
        {
            barnabus.gameObject.SetActive(false);
            skeletonGraphic.gameObject.SetActive(true);

            skeletonGraphic.skeletonDataAsset = _barnabus.skeletonData;
            skeletonGraphic.Initialize(true);

            Material _mat = _barnabus.skeletonMaterial;
            _barnabus.SetSkeletonMaterial();
            skeletonGraphic.material = _barnabus.skeletonMaterial;

            Debug.Log(skeletonGraphic.AnimationState.GetCurrent(0));
            skeletonGraphic.AnimationState.SetAnimation(0, _barnabus.GetAnimationName, true);
        }


        elePos.position = elePoses[int.Parse(_barnabus.barnabusNo) - 1].position;

        ChangeTextColor(DataManager.IsCharacterUnlocked(_barnabus.id));
    }

    public void ChangeTextColor(bool isUnLocked)
    {
        Debug.Log("IDDD card");
        Color color;
        if (isUnLocked == false)
        {
            ColorUtility.TryParseHtmlString("#B3B3B3", out color);
        }
        else
        {
            Debug.Log("COlllllll");
            ColorUtility.TryParseHtmlString(elementColor, out color);
        }
        elementName.color = color;
        elementNo.color = color;
    }

    private bool isPlaying = false;

    public void PlayStory()
    {
        if (isPlaying)
            return;

        StartCoroutine(WaitForPlay());
    }

    IEnumerator WaitForPlay()
    {
        isPlaying = true;
        storyPlayer.Play();
        yield return new WaitForSeconds(storyPlayer.clip.length);
        isPlaying = false;
    }

    private void OnDisable()
    {
        AudioManager.instance.FindAudioSource(storyPlayer.clip).Stop();

        isPlaying = false;
    }
}
