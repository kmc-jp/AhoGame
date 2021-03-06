﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ahoge
{
    public class StageController : MonoBehaviour
    {
        public GameObject Target;
        Texture2D targetTexture;
        public int BeginX;
        public int EndX;
        Animator animator;

        PointerController pointer;

        public string targetName;
        int width;
        int height;
        bool showInAdvance;
        public int percent;
        int textnum;
        List<string> texts;

        int nowText = -1;

        bool entered = false;

        int stageNumber;

        public int Result { get; private set; }

        public int ResultPercent { get; private set; }

        public bool IsEntering
        {
            get
            {
                var state = animator.GetCurrentAnimatorStateInfo(0);
                return state.IsName("Entering");
            }
        }

        public void Awake()
        {
            animator = this.GetComponentInChildren<Animator>();
            Target = transform.GetChild(0).gameObject;
            targetTexture = Target.GetComponent<SpriteRenderer>().sprite.texture;
            pointer = FindObjectOfType<PointerController>();
        }

        /// <summary>
        /// Stageの初期化
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Stageの背景画像ファイルの名前</returns>
        public string Initialize(string text, int stageNumber)
        {
            string[] settings = text.Split('\n');
            targetName = settings[0].Split(',')[1].Replace("\r", "");
            width = Int32.Parse(settings[1].Split(',')[1]);
            height = Int32.Parse(settings[2].Split(',')[1]);
            showInAdvance = Boolean.Parse(settings[3].Split(',')[1]);
            System.Random rand = new System.Random();
            percent = Int32.Parse(settings[4].Split(',')[1]) + rand.Next(3) - 5;
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
                if (t.Contains("{0}")) t = t.Replace("{0}", percent.ToString());
                texts.Add(t);
            }
            texts.Add("");
            this.stageNumber = stageNumber;

            return settings[5].Split(',')[1].Replace("\r", "");
        }

        public string GetNextText()
        {
            nowText++;
            return texts[nowText];
        }
		
		public void Update()
        {
            if (!entered)
            {
				if (animator == null) return;
				var state = animator.GetCurrentAnimatorStateInfo(0);
                if (state.IsName("Entered"))
                {
                    var tf = Target.transform;
                    float centerX = tf.position.x;
                    float diff = targetTexture.width / 200f;
                    pointer.Setup(this, tf.position.y + targetTexture.height / 200f, centerX - diff, centerX + diff);
                    entered = true;
                }
            }
        }

        public void Enter(){
            animator.SetBool("Enter", true);
        }

        public void Exit(){
            animator.SetBool("Exit", true);
        }

        public void Cut()
        {
            //割合
            var percent = pointer.PositionToPercent();
            int div = (int)(targetTexture.width * percent);
            var objects = PngScr.DivFromTexture2DinResources(Target.GetComponent<SpriteRenderer>(), targetTexture.name, div, true);
            if (!(objects == null || objects.Length == 0))
            {
                for (var i = 0; i < objects.Length; i++)
                {
                    if (objects[i] == null) continue;
                    GameObject.Destroy(objects[i], 2);
                }
            }
            var pixels = PngScr.pngCumulativeSum(targetTexture, true);
            var number = pixels[pixels.Length - 1];
            var cutPercent = Math.Min(number - pixels[div], pixels[div]) / (double)number * 100f;
            ResultPercent = (int)cutPercent;
            
            var diff = Math.Abs(cutPercent - this.percent);
            if (diff < 1) Result = 0; else if (diff < 5) Result = 1; else if (diff < 10) Result = 2; else if (diff < 15) Result = 3; else Result = 4;
            double keisu = Math.Exp(Math.Log(2.0 / 3.0) / 25.0 * diff * diff);
            double sc = 10000f * (1f + ((float)stageNumber / 10f)) * keisu;
            ScoreManager.AddScore((int)sc, stageNumber);
            Destroy(Target);
        }

        public Rect GetImageRect(){
            return new Rect();
        }

        public enum CutType{
            Vertical, Horizontal
        }
    }
}