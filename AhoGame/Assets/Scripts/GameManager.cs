using UnityEngine;
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
        int nowStageNo = 0;
        int stagenum = 2;
        Phase phase;

        List<GameObject> stagePrefabs;

        PointerController pointer;
        TextManager textManager;

        public void Awake()
        {
            camera = GameObject.Find("Camera").transform;
            LoadStages();
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

        /// <summary>
        /// 入力の適用
        /// </summary>
        public void AcceptInput()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (nowStageNo == 0)
                {

                }
                else if (0 < nowStageNo || nowStageNo < stagenum + 1)
                {

                }
                else if (nowStageNo == stagenum + 1)
                {

                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {

            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("!!");
                if (nowStageNo == 0)
                {
                    StartGame();
                    phase = Phase.Score;
                }
                else if (0 < nowStageNo || nowStageNo < 12)
                {
                    switch (phase)
                    {
                        case Phase.Speaking:
                            break;
                        case Phase.Cutting:
                            break;
                        case Phase.Score:
                            TransStage(nowStageNo++);
                            break;
                    }

                }
                else if (nowStageNo == 12)
                {

                }
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
            string setting = Resources.Load<TextAsset>("StageSettings/Stage1").text;
            nowStageObject = stagePrefabs[0];
            nowStageController = nowStageObject.GetComponent<StageController>();
            nowBackGroundImageName = nowStageController.Initialize(setting);
            nowStageNo++;
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
            nowStageObject = GameObject.Instantiate<GameObject>(stagePrefabs[toNo - 1]);
            nowStageController = nowStageObject.GetComponent<StageController>();
            nowBackGroundImageName = nowStageController.Initialize(setting);

            //フェードイン/アウト
            if (nowBackGroundImageName != "" && previousBackGroundImageName != null && nowBackGroundImageName != previousBackGroundImageName)
            {
                GameObject.Find("/BackGround/" + previousBackGroundImageName).GetComponent<Animator>().SetBool("Out", true);
                GameObject.Find("/BackGround/" + nowBackGroundImageName).GetComponent<Animator>().SetBool("In", true);
            }

            //Stage○の表示をにゅって感じで出す
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        public void StartCutting(int no)
        {

        }

        public enum Phase
        {
            Speaking, Cutting, Score
        }
    }
}