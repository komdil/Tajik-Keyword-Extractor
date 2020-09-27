using System.Collections.Generic;

namespace Model.KEA
{
    public class Sentense
    {
        public Sentense(WordManager manager)
        {
            WordManager = manager;
        }

        public WordManager WordManager { get; }
        public SentenseType SentenseType { get; set; }
        public string Content { get; set; }
        public IEnumerable<Word> Words { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }

    public enum SentenseType
    {

    }
}
