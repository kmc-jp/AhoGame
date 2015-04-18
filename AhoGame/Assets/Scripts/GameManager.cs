using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    Transform camera;

    /// <summary>
    /// 0がスタート画面、1~11がゲーム画面、12がスコア画面
    /// </summary>
    int stageNo = 0;
    Phase phase;

    public void Awake()
    {
        camera = GameObject.Find("Camera").transform;
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
            if(stageNo == 0)
            {

            }
            else if(0 < stageNo || stageNo < 12)
            {

            }
            else if(stageNo == 12)
            {

            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {

        }

        if(Input.GetMouseButtonDown(0))
        {
            if(stageNo == 0)
            {

            }
            else if(0 <stageNo || stageNo < 12)
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
            else if(stageNo == 12)
            {

            }
        }
    }

    public enum Phase
    {
        Speaking, Cutting, Score
    }
}
