using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public static class WordManagerSentenseExtensions
    {
        public static IEnumerable<Word> SplitWordsFromSentences(this WordManager wordManager, Sentence sentence)
        {
            List<Word> wordsInstances = new List<Word>();
            var words = sentence.Content.Replace(" - ", "-").Split(' ').GroupBy(w => w).Select(s => s.FirstOrDefault());
            foreach (var word in words)
            {
                wordsInstances.Add(new Word(Regex.Replace(word, Statics.SentenceEndSymbols, string.Empty), wordManager));
            }
            return wordsInstances;
        }
    }
}
