using System.Collections.Generic;

namespace Model.KEA.Document
{
    public class Document
    {
        public Document(string content)
        {
            Content = KEAGlobal.TextnormilizerManager.RemoveUnnassesarySpaces(content);
            Content = KEAGlobal.TextnormilizerManager.MakeReplaceMent(Content);
            Sentences = KEAGlobal.DocumentManager.SplitSentencesFromDoc(this);
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public List<Sentence> Sentences { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
