using Model.DataSet;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA.Document
{
    public class DocumentManager 
    {
        public List<Sentence> SplitSentencesFromDoc(Document document)
        {
            List<Sentence> sentenceInstanses = new List<Sentence>();
            var sentences = Regex.Split(document.Content, Statics.SplitSentensePattern).Where(a => a != "");
            foreach (var sentense in sentences)
            {
                sentenceInstanses.Add(new Sentence(sentense));
            }

            return sentenceInstanses;
        }
    }
}
