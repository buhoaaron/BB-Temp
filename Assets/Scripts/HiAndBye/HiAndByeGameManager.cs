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
        //答錯Barnabus生成管理
        public IncorrentBarnabusBuilder IncorrentBarnabusBuilder = null;

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

            IncorrentBarnabusBuilder = GetComponent<IncorrentBarnabusBuilder>();
            IncorrentBarnabusBuilder.Init();
        }
        public void SystemUpdate()
        {
            StateController?.StateUpdate();
            CountDownManager?.SystemUpdate();
        }
        public void Clear()
        {
           
        }
        #endregion

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
    }
}
