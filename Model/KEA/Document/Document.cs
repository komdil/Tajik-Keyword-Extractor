using System.Collections.Generic;

namespace Model.KEA.Document
{
    public class Document
    {
        public Document(string content)
        {
            Content = KEAGlobal.DocumentManager.RemoveUnnassesarySpaces(content);
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public IEnumerable<Sentence> Sentences { get; set; }

        public void SplitSentenses()
        {
            Sentences = KEAGlobal.DocumentManager.SplitSentencesFromDoc(this);
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
