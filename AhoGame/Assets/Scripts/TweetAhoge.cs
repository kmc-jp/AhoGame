using UnityEngine;

namespace Ahoge
{

    public class TweetAhoge
    {

        //新しいタブを開くのでポップアップブロックがあり得る。
        public static void TweetWithNewTab(string text)
        {
            Application.ExternalEval("window.open('http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(text) + "');");
        }

        //その自分のタブで作るので、unityの方は初めからになる
        public static void TweetInThisTab(string text)
        {
            Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(text));
        }

    }
}