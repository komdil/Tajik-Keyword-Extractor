using System.Collections.Generic;
using TajikKEA.Sentence;

namespace TajikKEA.Document
{
    /// <summary>
    /// RU: Текст или документ который содержит не менее одного предложения
    /// TJ: Матн ё ҳуҷҷате на кам аз як ҷумла иборат аст
    /// </summary>
    public class TajikDocument
    {
        /// <summary/>
        /// <param name="content">Матн</param>
        public TajikDocument(string content)
        {
            Content = KEAGlobal.TextnormilizerManager.RemoveUnnassesarySpaces(content);
            Content = KEAGlobal.TextnormilizerManager.MakeReplaceMent(Content);
            Sentences = KEAGlobal.DocumentManager.SplitSentencesFromDoc(this);
        }

        /// <summary>
        /// RU: Имя документа
        /// TJ: Номи ҳуҷҷат
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// RU: Текст документа
        /// TJ: Матни ҳуҷҷат
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// RU: Предложение
        /// TJ: Ҷумлаҳо
        /// </summary>
        public List<TajikSentence> Sentences { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
