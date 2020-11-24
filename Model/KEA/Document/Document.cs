using System.Collections.Generic;

namespace Model.KEA
{
    public class Document
    {
        public Document(string content)
        {
            Content = WordManager.RemoveUnnassesarySpaces(content);
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public IEnumerable<Sentence> Sentences { get; set; }

        public void SplitSentenses()
        {
            Sentences = WordManagerDocumentExtensions.SplitSentencesFromDoc(this);
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
