using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TajikKEAHelper.Sentence;

namespace TajikKEAHelper.Document
{
    /// <summary/>
    public class DocumentManager
    {
        /// <summary>
        /// RU: Соберёт предложения из документа
        /// TJ: Аз матн ҷумлаҳоо ҷамъ мекунад 
        /// </summary>
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
