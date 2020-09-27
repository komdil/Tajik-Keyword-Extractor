using System.Collections.Generic;

namespace Model.KEA
{
    public class Document
    {
        public Document(WordManager manager)
        {
            WordManager = manager;
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public WordManager WordManager { get; }

        public IEnumerable<Sentense> Sentenses { get; }

        public void SplitSentenses()
        {

        }

        public override string ToString()
        {
            return Content;
        }
    }
}
