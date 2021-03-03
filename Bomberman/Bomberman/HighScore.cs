using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    public static class HighScore
    {
       
        static string filePath = @"HighScore.csv";        
        public static List<string[]> highScoreList = new List<string[]>();
        /// <summary>
        /// ファイルからハイスコアを取得
        /// </summary>
        public static void ReadHighScore(int recordNum)
        {
            Encoding enc = Encoding.GetEncoding("shift_jis");
            StreamReader sr = new StreamReader(filePath, enc);
            // List<string[]> highScoreList = new List<string[]>();
            int counter = 0;
            highScoreList.Clear();
            while (!sr.EndOfStream)
            {
                // CSVファイルの一行を読み込む
                string line = sr.ReadLine();

                //1行目は無視
                if (counter != 0)
                {                    
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split(',');

                    // 配列からリストに格納する                   
                    highScoreList.Add(values);                   
                }
                counter++;
            }            
        }

        /// <summary>
        ///ハイスコアをファイルに出力
        /// </summary>
        public static void WriteHighScore(int record, string name, DateTime date)
        {
            //既存のファイルに上書き
            var append = true;

            Encoding enc = Encoding.GetEncoding("shift_jis");
            StreamWriter sw = new StreamWriter(filePath, append, enc);

            //書き込む処理

        }

    }
}
