using System.Collections.Generic;

namespace Model.KEA
{
    public class Document
    {
        public Document(WordManager manager, string content)
        {
            WordManager = manager;
            Content = manager.RemoveUnnassesarySpaces(content);
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public WordManager WordManager { get; }

        public IEnumerable<Sentence> Sentences { get; set; }

        public void SplitSentenses()
        {
            Sentences = WordManager.SplitSentencesFromDoc(this);
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
