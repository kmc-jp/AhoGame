using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace Ahoge
{
    public class PngScr
    {
        /// <summary>
        /// テキストファイルを作成して最終的な累積和を返却します。左上が原点の累積和です。
        /// Sprite.textureでSpriteのTexture2Dにアクセスしても良いです。
        /// </summary>
        /// <param name="tx2D"></param>
        /// <param name="SaveFileName"></param>
        /// <param name="ReadVertical"></param>
        /// <param name="HeaderWithWidthHeight"></param>
        /// <returns></returns>
        public static int SavepngCumulativeSum(Texture2D tx2D, string SaveFileName, bool ReadVertical = false, bool HeaderWithWidthHeight = false)
        {
            FileStream f = new FileStream(SaveFileName, FileMode.Create, FileAccess.Write);
            var writer = new StreamWriter(f);
            if (HeaderWithWidthHeight) { writer.WriteLine(tx2D.width + " " + tx2D.height); }

            int[] ASum = pngCumulativeSum(tx2D, ReadVertical);
            for (int i = 0; i < ASum.Length; i++) { writer.WriteLine(ASum[i]); }
            writer.Close();

            return ASum[ASum.Length - 1];
        }

        /// <summary>
        /// 累積和配列を返却します
        /// </summary>
        /// <param name="tx2D"></param>
        /// <param name="ReadVertical"></param>
        /// <returns></returns>
        public static int[] pngCumulativeSum(Texture2D tx2D, bool ReadVertical = false)
        {
            Color32[] pix = tx2D.GetPixels32();//左下から右に一行ずつ読み込んだ配列
            int[] ASum;
            if (ReadVertical)
            {
                ASum = new int[tx2D.width];
                for (int i = 0; i < tx2D.width; i++)
                {
                    ASum[i] = 0;
                    for (int j = 0; j < tx2D.height; j++)
                    {
                        if (pix[i + j * tx2D.width].a == 255) ASum[i]++;
                    }
                }
            }
            else
            {
                ASum = new int[tx2D.height];
                for (int i = 0; i < tx2D.height; i++)
                {
                    ASum[tx2D.height - i - 1] = 0;
                    for (int j = 0; j < tx2D.width; j++)
                    {
                        if (pix[j + i * tx2D.width].a == 255) ASum[tx2D.height - i - 1]++;
                    }
                }
            }

            int sum = 0;
            int[] Result = new int[ASum.Length];
            for (int i = 0; i < ASum.Length; i++)
            {
                sum += ASum[i];
                Result[i] = sum;
            }
            return Result;
        }

        /// <summary>
        /// アウトプットしたテキストファイルからint[]の累積和に変換します。
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static int[] CumulativeSumFromFile(string FileName)
        {
            FileStream f = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(f);
            var AL = new List<int>();
            while (reader != null && !reader.EndOfStream)
            {
                AL.Add(int.Parse(reader.ReadLine()));
            }
            reader.Close();
            return AL.ToArray();
        }

        /// <summary>
        /// ResourcesにあるTexture2Dのデータからふたつのを作成する。
        /// SpriteからTexture2Dを読み込むと変な挙動を起こす
        /// </summary>
        /// <param name="Texture2DName"></param>
        /// <param name="DivPos"></param>
        /// <param name="DivVertical"></param>
        /// <returns></returns>
        public static GameObject[] DivFromTexture2DinResources(SpriteRenderer BaseSRR, string Texture2DName, int DivPos, bool DivVertical = false)
        {
            Texture2D tx = Resources.Load("Images/" + Texture2DName) as Texture2D;
            Sprite[] sr = new Sprite[2];
            if (DivVertical)
            {
                if (DivPos < 0 || DivPos > tx.width) { Debug.Log("Divpos error"); return null; }
                sr[0] = Sprite.Create(tx, new Rect(0, 0, DivPos, tx.height), new Vector2(0.5f, 0.5f));
                sr[1] = Sprite.Create(tx, new Rect(DivPos, 0, tx.width - DivPos, tx.height), new Vector2(0.5f, 0.5f));

            }
            else
            {
                if (DivPos < 0 || DivPos > tx.height) { Debug.Log("Divpos error"); return null; }
                sr[0] = Sprite.Create(tx, new Rect(0, 0, tx.width, DivPos), new Vector2(0.5f, 0.5f));
                sr[1] = Sprite.Create(tx, new Rect(0, DivPos, tx.width, tx.height - DivPos), new Vector2(0.5f, 0.5f));
            }
            GameObject[] go = new GameObject[2];
            Sprite BaseSprite = BaseSRR.sprite;
            GameObject BaseGo = BaseSRR.gameObject;
            float ppu = BaseSprite.pixelsPerUnit;
            for (int i = 0; i < 2; i++)
            {
                go[i] = new GameObject();
                go[i].name = Texture2DName + i;
                go[i].AddComponent<SpriteRenderer>().sprite = sr[i];
                //Sprite S = go[i].GetComponent<SpriteRenderer>().sprite ;

            }
            var diff = (DivPos + 90) / 2f / ppu;
            if (DivVertical)
            {
                go[0].transform.position = BaseGo.transform.position - new Vector3(diff, 0, 0);
                go[1].transform.position = BaseGo.transform.position + new Vector3(diff, 0, 0);
            }
            else
            {
                go[0].transform.position = BaseGo.transform.position + new Vector3(0, 0, 0);
                go[1].transform.position = BaseGo.transform.position + new Vector3(0, diff, 0);
            }
            go[0].GetComponent<SpriteRenderer>().sortingLayerID = 1;
            go[1].GetComponent<SpriteRenderer>().sortingLayerID = -1;

            return go;
        }
    }
}