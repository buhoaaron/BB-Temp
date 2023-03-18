using UnityEngine;
using System.Collections.Generic;
using Barnabus;
using Barnabus.SceneManagement;
using HiAndBye.Question;
using HiAndBye.StateControl;

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

        public GameRootUI GameRootUI = null;
        public GameResultUI GameResultUI = null;

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
            return NewGameManager.Instance.PlayersBarnabusManager.GetPlayerBarnabusCount();
        }
        public List<BarnabusBaseData> GetBatch(int batchNo)
        {
            return NewGameManager.Instance.PlayersBarnabusManager.GetBatch(batchNo);
        }
        public Sprite GetBarnabusSprite(string name)
        {
            return NewGameManager.Instance.BarnabusCardManager.GetCard(name).BarnabusImage;
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
