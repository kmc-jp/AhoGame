using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ahoge
{
    public class StageController : MonoBehaviour
    {
        public GameObject Target;
        public int BeginX;
        public int EndX;
        Animator animator;

        string targetName;
        int width;
        int height;
        bool showInAdvance;
        int percent;
        int textnum;
        List<string> texts;

        int nowText = -1;

        public void Awake()
        {
            animator = this.GetComponent<Animator>();
        }

        /// <summary>
        /// Stageの初期化
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Stageの背景画像ファイルの名前</returns>
        public string Initialize(string text)
        {
            string[] settings = text.Split('\n');
            targetName = settings[0].Split(',')[1].Replace("\r", "");
            width = Int32.Parse(settings[1].Split(',')[1]);
            height = Int32.Parse(settings[2].Split(',')[1]);
            showInAdvance = Boolean.Parse(settings[3].Split(',')[1]);
            percent = Int32.Parse(settings[4].Split(',')[1]);
            textnum = Int32.Parse(settings[6].Split(',')[1]);
            texts = new List<string>();
            for (var i = 0; i < textnum; i++)
            {
                string t = "";
                for (var j = 0; j < 4; j++)
                {
                    t += settings[7 + i * 4 + j];
                    t += "\n";
                }
                texts.Add(t);
            }
            texts.Add("");

            return settings[5].Split(',')[1].Replace("\r", "");
        }

        public string GetNextText()
        {
            nowText++;
            return texts[nowText];
        }

        public void Start()
        {

        }

        public void Update()
        {
        }

        public void Enter()
        {
            animator.SetBool("Enter", true);
            animator.SetBool("Entered", true);
        }

        public void Exit()
        {
            animator.SetBool("Exit", true);
        }

        public Rect GetImageRect()
        {

            return new Rect();
        }

        public enum CutType
        {
            Vertical, Horizontal
        }
    }
}