using System.Collections.Generic;

namespace Model.KEA
{
    public class Sentence
    {
        public Sentence(WordManager manager, string content)
        {
            WordManager = manager;
            Content = content;
        }

        public WordManager WordManager { get; }
        public SentenseType SentenseType { get; set; }
        public string Content { get; set; }
        public IEnumerable<Word> Words { get; set; }

        public void SplitWords()
        {
            Words = WordManager.SplitWordsFromSentences(this);
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
