using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TajikKEA.DataSet;

namespace TajikKEA.Sentence
{
    /// <summary/>
    public class SentenseManager : KEAManager
    {
        public SentenseManager(IWordContext context) : base(context) { }

        /// <summary>
        /// RU: Соберет слова из предложения
        /// TJ: Гирифтани калимаҳо аз ҷумла
        /// </summary>
        public IEnumerable<TajikWord> SplitWordsFromSentences(TajikSentence sentence)
        {
            List<TajikWord> wordsInstances = new List<TajikWord>();
            var words = sentence.Content.Replace(" - ", "-").Split(' ').GroupBy(w => w).Select(s => s.FirstOrDefault());
            foreach (var word in words)
            {
                wordsInstances.Add(new TajikWord(Regex.Replace(word, Statics.SentenceEndSymbols, string.Empty)));
            }
            return wordsInstances;
        }

        /// <summary>
        /// RU: Нормализация текста
        /// TJ: Нормаликунонии матн
        /// </summary>
        public void NormalizeWords(IEnumerable<TajikWord> words, TajikSentence sentence)
        {
            foreach (var word in words.ToList())
            {
                ShakliJam(word);
                Ishorakuni(word);
                BandakiI(word);
                BandakiE(word);
                BandakiU(word);
                RemoveUnnassesaryWords(word, sentence);
            }
        }

        readonly string[] pasoyandJam = new string[] { "он", "ён", "ҳо" };
        void ShakliJam(TajikWord word)
        {
            if (pasoyandJam.Any(s => word.Value.EndsWith(s)))
            {
                var splited = word.Value.Substring(0, word.Value.Length - 2);
                if (splited.Length > 1 && Context.Words.Any(s => s.Content == splited))
                {
                    word.Value = splited;
                }
            }
        }

        readonly string[] ishorakuni = new string[] { "ро", "ҳоро", "онро", "ёнро" };
        void Ishorakuni(TajikWord word)
        {
            foreach (var ishora in ishorakuni)
            {
                if (word.Value.EndsWith(ishora))
                {
                    var splited = word.Value.Substring(0, word.Value.Length - ishora.Length);
                    if (splited.Length > 2 && Context.Words.Any(s => s.Content == splited))
                    {
                        word.Value = splited;
                    }
                }
            }
        }

        readonly string[] bandakiI = new string[] { "и", "ҳои", "они", "ёни" };
        void BandakiI(TajikWord word)
        {
            foreach (var bandak in bandakiI)
            {
                if (word.Value.EndsWith(bandak))
                {
                    var splited = word.Value.Substring(0, word.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, out string bandakToEnd))
                    {
                        if (bandakToEnd != null)
                        {
                            word.Value = bandakToEnd;
                        }
                        else
                        {
                            word.Value = splited;
                        }
                    }
                }
            }
        }

        readonly string[] bandakiE = new string[] { "е", "ҳое", "оне", "ёне" };
        void BandakiE(TajikWord word)
        {
            foreach (var bandak in bandakiE)
            {
                if (word.Value.EndsWith(bandak))
                {
                    var splited = word.Value.Substring(0, word.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, out string bandakToEnd))
                    {
                        if (bandakToEnd != null)
                        {
                            word.Value = bandakToEnd;
                        }
                        else
                        {
                            word.Value = splited;
                        }
                    }
                }
            }
        }

        bool DataSetContains(string splited, out string res)
        {
            res = null;
            if (splited.EndsWith("и"))
            {
                var addBandakToEnd = splited.Substring(0, splited.Length - 1) + "ӣ";
                bool contains = Context.Words.Any(s => s.Content == addBandakToEnd);
                if (contains)
                {
                    res = addBandakToEnd;
                    return true;
                }
            }
            return Context.Words.Any(s => s.Content == splited);
        }

        readonly string[] bandakiU = new string[] { "ҳову", "ону", "Ю", "у", "ёну", "ҳоеро" };
        void BandakiU(TajikWord word)
        {
            foreach (var bandak in bandakiU)
            {
                if (word.Value.EndsWith(bandak))
                {
                    var splited = word.Value.Substring(0, word.Value.Length - bandak.Length);
                    if (splited.Length > 2 && Context.Words.Any(s => s.Content == splited))
                    {
                        word.Value = splited;
                    }
                }
            }
        }

        void RemoveUnnassesaryWords(TajikWord word, TajikSentence sentence)
        {
            var shouldBeRemoved = ShouldBeRemoved(word.Value);
            if (shouldBeRemoved)
            {
                sentence.Words.Remove(word);
            }
        }

        bool ShouldBeRemoved(string word)
        {
            if (word == null || word == "" || int.TryParse(word, out int res))
            {
                return true;
            }
            foreach (char ch in word)
            {
                if ((int)ch >= 97 && (int)ch <= 122)
                {
                    return true;
                }
            }
            if (!Context.Words.Any(s => s.Content == word) ||
                Context.Suffixes.Any(s => s.Content == word) ||
                Context.Prepositions.Any(s => s.Content == word) ||
                Context.Pronouns.Any(s => s.Content == word) ||
                Context.StopWords.Any(s => s.Content == word))
            {
                return true;
            }
            return false;
        }
    }
}
