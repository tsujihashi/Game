using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStudy
{
    public class GetWord
    {
        Encoding enc = Encoding.GetEncoding("shift_jis");

        public void GetWordFromFile(string filePath)
        {
            //string filePath = Directory.GetCurrentDirectory() + "\\data.csv";
            StaticData.selectedWordList = new WordList();
            StreamReader sr = new StreamReader(filePath, enc);
            while (!sr.EndOfStream)
            {
                var values = sr.ReadLine().Split(',');
                Word word = new Word(values);
                StaticData.selectedWordList.words.Add(word);
            }
            StaticData.selectedWordList.listName = filePath.Replace(Directory.GetCurrentDirectory() + "\\WordList\\", "").Replace(".csv", "");
            sr.Close();
        }
    }
}
