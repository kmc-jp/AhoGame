using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

namespace Ahoge
{
    public class PngScr
    {
        //テキストファイルを作成して最終的な累積和を返却します。左上が原点の累積和です。
        //Sprite.textureでSpriteのTexture2Dにアクセスしても良いです。
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

        //累積和配列を返却します
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

        //アウトプットしたテキストファイルからint[]の累積和に変換します。
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

        //ResourcesにあるTexture2Dのデータからふたつのを作成する。
        //SpriteからTexture2Dを読み込むと変な挙動を起こす
        public static GameObject[] DivFromTexture2DinResources(string Texture2DName, int DivPos, bool DivVertical = false)
        {
            Texture2D tx = Resources.Load(Texture2DName) as Texture2D;
            Sprite sr1, sr2;

            if (DivVertical)
            {
                if (DivPos < 0 || DivPos > tx.width) { Debug.Log("Divpos error"); return null; }
                sr1 = Sprite.Create(tx, new Rect(0, 0, DivPos, tx.height), new Vector2(0.5f, 0.5f));
                sr2 = Sprite.Create(tx, new Rect(DivPos, 0, tx.width - DivPos, tx.height), new Vector2(0.5f, 0.5f));

            }
            else
            {
                if (DivPos < 0 || DivPos > tx.height) { Debug.Log("Divpos error"); return null; }
                sr1 = Sprite.Create(tx, new Rect(0, 0, tx.width, DivPos), new Vector2(0.5f, 0.5f));
                sr2 = Sprite.Create(tx, new Rect(0, DivPos, tx.width, tx.height - DivPos), new Vector2(0.5f, 0.5f));
            }
            GameObject go1 = new GameObject(); GameObject go2 = new GameObject();
            go1.name = Texture2DName + "1"; go2.name = Texture2DName + "2";
            go1.AddComponent<SpriteRenderer>().sprite = sr1;
            go2.AddComponent<SpriteRenderer>().sprite = sr2;

            GameObject[] resultgo = new GameObject[2];
            resultgo[0] = go1; resultgo[1] = go2;
            return resultgo;

        }
    }
}