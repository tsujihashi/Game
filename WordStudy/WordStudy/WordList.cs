using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStudy
{
    public class WordListSet
    {
        public List<WordList> wordList = new List<WordList>();
    }

    public class WordList
    {
        public int idx;
        public string listName;
        public List<Word> words = new List<Word>();
    }

    public class Word
    {
        public string problem;
        public string answer;

        public Word(string[] word)
        {
            problem = word[0];
            answer = word[1];
        }
    }

    
}
