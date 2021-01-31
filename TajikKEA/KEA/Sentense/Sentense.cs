using System.Collections.Generic;
using System.Linq;

namespace TajikKEA.Sentence
{
    /// <summary>
    /// RU: Класс предложения
    /// TJ: Ҷумла 
    /// </summary>
    public class TajikSentence
    {
        /// <summary/>
        public TajikSentence(string content)
        {
            Content = content;
            Words = KEAGlobal.SentenseManager.SplitWordsFromSentences(this).ToList();
        }

        /// <summary>
        /// RU: Содержание (текст) предложения
        /// TJ: Матни ҷумла  
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// RU: Слова предложения
        /// TJ: Калимаҳои ҷумла 
        /// </summary>
        public List<Word> Words { get; set; }

        public override string ToString()
        {
            return Content;
        }

        /// <summary>
        /// RU: Нормализирование слова
        /// TJ: Нормализатсияи калимаҳо
        /// </summary>
        public void NormalizeWords()
        {
            KEAGlobal.SentenseManager.NormalizeWords(Words, this);
        }
    }
}
