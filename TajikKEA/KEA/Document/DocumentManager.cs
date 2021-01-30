using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TajikKEA.Sentence;

namespace TajikKEA.Document
{
    public class DocumentManager
    {
        public List<TajikSentence> SplitSentencesFromDoc(TajikDocument document)
        {
            List<TajikSentence> sentenceInstanses = new List<TajikSentence>();
            var sentences = Regex.Split(document.Content, Statics.SplitSentensePattern).Where(a => a != "");
            foreach (var sentense in sentences)
            {
                sentenceInstanses.Add(new TajikSentence(sentense));
            }

            return sentenceInstanses;
        }
    }
}
