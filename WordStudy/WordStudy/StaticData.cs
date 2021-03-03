using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStudy
{
    public static class StaticData
    {
        public static List<string> files = new List<string>();
        public static List<WordList> wordListSet = new List<WordList>();
        public static WordList selectedWordList = new WordList();
        public static List<string[]> wordList = new List<string[]>();

        //public static List<Tuple<List<string[]>, string>> allWordList = new List<Tuple<List<string[]>, string>>();
    }
}
