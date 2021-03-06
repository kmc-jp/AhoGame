﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Ahoge
{
    public static class ScoreManager 
    {
        public static int[] Scores = new int[11];

        public static void Reset()
        {
            for (int i = 0; i < Scores.Length; i++)
            {
                Scores[i] = 0;
            }
        }

        public static void AddScore(int score, int stage)
        {
            Scores[stage] = score;
            Debug.Log("Stage:" + stage + ", Score:" + Scores[stage]);
        }

        public static bool CanExtra()
        {
            int sum = Scores.Sum();
            return (sum - Scores[Scores.Length - 1] > 90000);
        }
    }
}
