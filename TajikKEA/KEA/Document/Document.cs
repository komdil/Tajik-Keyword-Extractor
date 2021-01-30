using System.Collections.Generic;
using TajikKEA.Sentence;

namespace TajikKEA.Document
{
    public class TajikDocument
    {
        public TajikDocument(string content)
        {
            Content = KEAGlobal.TextnormilizerManager.RemoveUnnassesarySpaces(content);
            Content = KEAGlobal.TextnormilizerManager.MakeReplaceMent(Content);
            Sentences = KEAGlobal.DocumentManager.SplitSentencesFromDoc(this);
        }

        public string Name { get; set; }

        public string Content { get; set; }

        public List<TajikSentence> Sentences { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
