using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ahoge
{
    public class GameManager : MonoBehaviour
    {
        Transform camera;

        /// <summary>
        /// 0がスタート画面、1~11がゲーム画面、12がスコア画面
        /// </summary>
        int nowStageNo = -2;
        int stagenum = 2;
        Phase phase;

        List<GameObject> stagePrefabs;

        PointerController pointer;
        TextManager textManager;

        public void Awake()
        {
            camera = GameObject.Find("Camera").transform;
            textManager = this.GetComponent<TextManager>();
            LoadStages();
            textManager.SetVisible(false);
            nowBackGroundImageName = "title";
            texts = new List<string>();
            string[] ss = Resources.Load<TextAsset>("OtherTexts").text.Split('\n');
            for (var i = 0; i < ss.Length / 4; i++)
            {
                string s = ss[i * 4] + "\n" + ss[i * 4 + 1] + "\n" + ss[i * 4 + 2] + "\n" + ss[i * 4 + 3];
                texts.Add(s);
            }

            pointer = FindObjectOfType<PointerController>();
            
            Feed(previousBackGroundImageName, nowBackGroundImageName);
        }

        public void LoadStages()
        {
            stagePrefabs = new List<GameObject>();
            for (var i = 1; i < stagenum + 1; i++)
            {
                stagePrefabs.Add(Resources.Load<GameObject>("Prefabs/Stage" + i));
            }


        }



        public void Start()
        {
        }

        public void Update()
        {
            AcceptInput();
        }

        List<string> texts;

        /// <summary>
        /// 入力の適用
        /// </summary>
        public void AcceptInput()
        {

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if(nowStageNo == -2)
                {
                    previousBackGroundImageName = nowBackGroundImageName;
                    nowBackGroundImageName = "Start";
                    Feed(previousBackGroundImageName, nowBackGroundImageName);
                    textManager.SetText(texts[0]);
                    nowStageNo++;
                }
                else if(nowStageNo == -1)
                {
                    previousBackGroundImageName = nowBackGroundImageName;
                    nowBackGroundImageName = "BackGroundImage";
                    Feed(previousBackGroundImageName, nowBackGroundImageName);
                    textManager.SetText(texts[1]);
                    nowStageNo++;
                }
                else if (nowStageNo == 0)
                {
                    StartGame();
                    phase = Phase.Speaking;
                }
                else if (0 < nowStageNo || nowStageNo < stagenum + 1)
                {
                    switch (phase)
                    {
                        case Phase.Speaking:
                            string s = nowStageController.GetNextText();
                            if (s == "") StartCutting();
                            else textManager.SetText(s);
                            break;
                        case Phase.Cutting:
                            Cut();                            
                            phase = Phase.Score;
                            break;
                        case Phase.Score:
                            TransStage(++nowStageNo);
                            break;
                    }

                }
                else if (nowStageNo == stagenum + 1)
                {

                }

                Debug.Log(phase);
            }
        }



        //前のステージ用のオブジェクトたち
        GameObject previousStageObject;
        StageController previousStageController;
        string previousBackGroundImageName;

        //今のステージ用のオブジェクトたち
        GameObject nowStageObject;
        StageController nowStageController;
        string nowBackGroundImageName;

        public void StartGame()
        {
            textManager.SetVisible(true);
            string setting = Resources.Load<TextAsset>("StageSettings/Stage1").text;
            nowStageObject = GameObject.Instantiate(stagePrefabs[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            nowStageController = nowStageObject.GetComponent<StageController>();
            previousBackGroundImageName = nowBackGroundImageName;
            nowBackGroundImageName = nowStageController.Initialize(setting, stagenum);
            textManager.SetText(nowStageController.GetNextText());
            Feed(previousBackGroundImageName, nowBackGroundImageName);
            nowStageNo++;
            ScoreManager.Reset();
        }

        /// <summary>
        /// ステージの遷移
        /// </summary>
        /// <param name="toNo"></param>
        public void TransStage(int toNo)
        {
            previousStageObject = nowStageObject;
            previousStageController = nowStageController;
            previousBackGroundImageName = nowBackGroundImageName;

            string setting = Resources.Load<TextAsset>("StageSettings/Stage" + toNo).text;
            nowStageObject = GameObject.Instantiate(stagePrefabs[toNo - 1], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            nowStageController = nowStageObject.GetComponent<StageController>();
            nowBackGroundImageName = nowStageController.Initialize(setting, stagenum);

            Feed(previousBackGroundImageName, nowBackGroundImageName);
            textManager.SetText(nowStageController.GetNextText());
            phase = Phase.Speaking;
            //Stage○の表示をにゅって感じで出す
        }

        /// <summary>
        /// フィードイン・アウト
        /// </summary>
        /// <param name="previous"></param>
        /// <param name="now"></param>
        public void Feed(string previous, string now)
        {
            if (previous == now) return;
            if (!String.IsNullOrEmpty(previous))
            {
                GameObject.Find("/BackGround/" + previous).GetComponent<Animator>().SetBool("Out", true);
            }
            if (!String.IsNullOrEmpty(now))
            {
                GameObject.Find("/BackGround/" + now).GetComponent<Animator>().SetBool("In", true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartCutting()
        {
            phase = Phase.Cutting;
            nowStageController.Enter();
        }


        void Cut()
        {
            pointer.Down();
            nowStageController.Cut();
            //nowStageController.Result
        }
        
        public enum Phase
        {
            Speaking, Cutting, Score
        }
    }
}