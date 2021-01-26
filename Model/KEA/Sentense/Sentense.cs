using System;
using System.Collections.Generic;
using System.Linq;

namespace TajikKEA.Sentence
{
    public class TajikSentence
    {
        public TajikSentence(string content)
        {
            Content = content;
            Words = KEAGlobal.SentenseManager.SplitWordsFromSentences(this).ToList();
        }

        public SentenseType SentenseType { get; set; }
        public string Content { get; set; }
        public List<Word> Words { get; set; }

        public override string ToString()
        {
            return Content;
        }

        public void NormalizeWords()
        {
            KEAGlobal.SentenseManager.NormalizeWords(Words, this);
        }
    }
}
