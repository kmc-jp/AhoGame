using UnityEngine;
using System.Collections;

namespace Ahoge
{
    public class TweetButton : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void tweet()
        {
            int score = 0;
            for (int i = 0; i < 10; i++)
            {
                score += ScoreManager.Scores[i];
            }
            if (ScoreManager.CanExtra())
            {
                score += ScoreManager.Scores[10];
            }

            string text = "数％マーケットで" + score + "点取りました！！\n" + "http://www.kmc.gr.jp/projects/ahogeSuper/market " + "\n" + "#ahoge";
            TweetAhoge.TweetWithNewTab(text);
        }
    }
}