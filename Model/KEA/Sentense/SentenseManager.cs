using Model.DataSet;
using Model.KEA.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class SentenseManager : KEAManager
    {
        public SentenseManager(IWordContext context) : base(context)
        {

        }

        public IEnumerable<Word> SplitWordsFromSentences(Sentence sentence)
        {
            List<Word> wordsInstances = new List<Word>();
            var words = sentence.Content.Replace(" - ", "-").Split(' ').GroupBy(w => w).Select(s => s.FirstOrDefault());
            foreach (var word in words)
            {
                wordsInstances.Add(new Word(Regex.Replace(word, Statics.SentenceEndSymbols, string.Empty)));
            }
            return wordsInstances;
        }

        public void NormalizeWords(IEnumerable<Word> words, Sentence sentence)
        {
            ShakliJam(words);
            Ishorakuni(words);
            BandakiI(words);
            BandakiE(words);
            BandakiU(words);
            RemoveUnnassesaryWords(words.ToList(), sentence);
        }

        string[] pasoyandJam = new string[] { "он", "ён", "ҳо" };
        void ShakliJam(IEnumerable<Word> words)
        {
            var wordsJam = words.Where(s => pasoyandJam.Any(d => s.Value.ToLower().EndsWith(d)));
            foreach (var item in wordsJam)
            {
                var splited = item.Value.Substring(0, item.Value.Length - 2);
                if (splited.Length > 1 && Context.Words.Any(s => s.Content == splited))
                {
                    item.Value = splited;
                }
            }
        }

        string[] ishorakuni = new string[] { "ро", "ҳоро", "онро", "ёнро" };
        void Ishorakuni(IEnumerable<Word> words)
        {
            foreach (var ishora in ishorakuni)
            {
                foreach (var item in words.Where(s => s.Value.ToLower().EndsWith(ishora)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - ishora.Length);
                    if (splited.Length > 2 && Context.Words.Any(s => s.Content == splited))
                    {
                        item.Value = splited;
                    }
                }
            }
        }

        string[] bandakiI = new string[] { "и", "ҳои", "они", "ёни" };
        void BandakiI(IEnumerable<Word> words)
        {
            foreach (var bandak in bandakiI)
            {
                foreach (var item in words.Where(s => s.Value.EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, out string bandakToEnd))
                    {
                        if (bandakToEnd != null)
                        {
                            item.Value = bandakToEnd;
                        }
                        else
                        {
                            item.Value = splited;
                        }
                    }
                }
            }
        }

        string[] bandakiE = new string[] { "е", "ҳое", "оне", "ёне" };
        void BandakiE(IEnumerable<Word> words)
        {
            foreach (var bandak in bandakiE)
            {
                foreach (var item in words.Where(s => s.Value.EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, out string bandakToEnd))
                    {
                        if (bandakToEnd != null)
                        {
                            item.Value = bandakToEnd;
                        }
                        else
                        {
                            item.Value = splited;
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

        string[] bandakiU = new string[] { "ҳову", "ону", "Ю", "у", "ёну", "ҳоеро" };
        void BandakiU(IEnumerable<Word> words)
        {
            foreach (var bandak in bandakiU)
            {
                foreach (var item in words.Where(s => s.Value.ToLower().EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && Context.Words.Any(s => s.Content == splited))
                    {
                        item.Value = splited;
                    }
                }
            }
        }

        void RemoveUnnassesaryWords(IEnumerable<Word> words, Sentence sentence)
        {
            foreach (var item in words)
            {
                var shouldBeRemoved = ShouldBeRemoved(item.Value);
                if (shouldBeRemoved)
                {
                    sentence.Words.Remove(item);
                }
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
                Context.Bandaks.Any(s => s.Content == word) ||
                Context.Peshoyands.Any(s => s.Content == word) ||
                Context.Jonishins.Any(s => s.Content == word))
            {
                return true;
            }
            return false;
        }
    }
}
