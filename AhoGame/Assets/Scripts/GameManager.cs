using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    Transform camera;

    /// <summary>
    /// 0がスタート画面、1~11がゲーム画面、12がスコア画面
    /// </summary>
    int nowStageNo = 0;
    int stagenum = 1;
    Phase phase;

    List<GameObject> stagePrefabs;

    public void Awake()
    {
        camera = GameObject.Find("Camera").transform;
        stagePrefabs = new List<GameObject>();
        for(var i = 0;i < 1;i++)
        {

        }
    }

    public void Start()
    {

    }

    public void Update()
    {

    }

    public void AcceptInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(nowStageNo == 0)
            {

            }
            else if(0 < nowStageNo || nowStageNo < 12)
            {

            }
            else if(nowStageNo == 12)
            {

            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

        }

        if(Input.GetMouseButtonDown(0))
        {
            if(nowStageNo == 0)
            {

            }
            else if(0 <nowStageNo || nowStageNo < 12)
            {
                switch(phase)
                {
                    case Phase.Speaking:
                        break;
                    case Phase.Cutting:
                        break;
                    case Phase.Score:
                        break;
                }

            }
            else if(nowStageNo == 12)
            {

            }
        }
    }

    public void LoadStages()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="no"></param>
    public void StageStart(int no)
    {

    }

    public enum Phase
    {
        Speaking, Cutting, Score
    }
}
