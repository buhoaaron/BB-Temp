using UnityEngine;
using System.Collections.Generic;
using Barnabus;
using Barnabus.SceneManagement;
using HiAndBye.Question;
using HiAndBye.StateControl;
using Barnabus.UI;

namespace HiAndBye
{
    public class HiAndByeGameManager : MonoBehaviour, IBaseSystem
    {
        public GameStateController StateController = null;
        //答題管理
        public AnswerManager AnswerManager = null;
        //時間倒數
        public CountDownManager CountDownManager = null;
        //題目設定
        public QuestionManager QuestionManager = null;
        //階級判定
        public RankManager RankManager = null;
        //答錯Barnabus生成管理
        public IncorrentBarnabusBuilder IncorrentBarnabusBuilder = null;

        public InterferenceManager InterferenceManager = null;

        public GameRootUI GameRootUI = null;
        public GameResultUI GameResultUI = null;
        public SettingUI SettingUI = null;
        public PotionRewardUI PotionRewardUI = null;

        #region BASE_API
        public void Init()
        {
            StateController = new GameStateController(this);

            QuestionManager = new QuestionManager(this);
            QuestionManager.Init();

            CountDownManager = new CountDownManager(this);
            CountDownManager.Init();

            AnswerManager = new AnswerManager(this, QuestionManager);
            AnswerManager.Init();

            RankManager = new RankManager(this);
            RankManager.Init();

            IncorrentBarnabusBuilder = GetComponent<IncorrentBarnabusBuilder>();
            IncorrentBarnabusBuilder.Init();

            InterferenceManager = new InterferenceManager(this);
            InterferenceManager.Init();
        }
        public void SystemUpdate()
        {
            StateController?.StateUpdate();
            CountDownManager?.SystemUpdate();

            InterferenceManager?.SystemUpdate();
        }
        public void Clear()
        {
           
        }
        #endregion

        public void IncreasePlayerPotionAndSave(int value)
        {
            NewGameManager.Instance.PlayerDataManager.IncreasePotionAmount(value);
            NewGameManager.Instance.PlayerDataManager.Save();
        }

        public int GetPlayerBarnabusCount()
        {
            return NewGameManager.Instance.PlayerDataManager.GetPlayerBarnabusCount();
        }
        public List<PlayerBarnabusData> GetBatch(int batchNo)
        {
            return NewGameManager.Instance.PlayerDataManager.GetBatch(batchNo);
        }
        public Sprite GetBarnabusSprite(string name)
        {
            return NewGameManager.Instance.BarnabusCardManager.GetCard(name).BarnabusImage;
        }
        public HiAndByeSpineAssets GetSpineAssets()
        {
            if (!NewGameManager.Instance.TryGetComponent<HiAndByeSpineAssets>(out var spineAssets))
                spineAssets = gameObject.AddComponent<HiAndByeSpineAssets>();

            return spineAssets;
        }
        public void PlaySound(AUDIO_NAME audio)
        {
            NewGameManager.Instance.AudioManager.PlaySound(audio);
        }
        public void BackMainScene()
        {
            NewGameManager.Instance.SetSceneState(SCENE_STATE.LOADING_MAIN);
        }

        public AllRankInfo LoadRankInfo()
        {
            var infos = NewGameManager.Instance.JsonManager.DeserializeObject<AllRankInfo>(JsonText.RankData);

            RankManager.SetAllRankInfo(infos);

            return infos;
        }

        public AllInterferenceInfo LoadInterferenceInfo()
        {
            var infos = NewGameManager.Instance.JsonManager.DeserializeObject<AllInterferenceInfo>(JsonText.InterferenceData);

            InterferenceManager.SetAllInterferenceInfo(infos);

            return infos;
        }
    }
}
