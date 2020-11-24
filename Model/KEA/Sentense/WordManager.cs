using iTextSharp.text.api;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Model.DataSet.SqlServer;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public static class WordManagerSentenseExtensions
    {
        public static IEnumerable<Word> SplitWordsFromSentences(Sentence sentence)
        {
            List<Word> wordsInstances = new List<Word>();
            var words = sentence.Content.Replace(" - ", "-").Split(' ').GroupBy(w => w).Select(s => s.FirstOrDefault());
            foreach (var word in words)
            {
                wordsInstances.Add(new Word(Regex.Replace(word, Statics.SentenceEndSymbols, string.Empty)));
            }
            return wordsInstances;
        }

        public static void NormalizeWords(IEnumerable<Word> words, SqlServerContext context, Sentence sentence)
        {
            ShakliJam(words, context);
            Ishorakuni(words, context);
            BandakiI(words, context);
            BandakiE(words, context);
            BandakiU(words, context);
            RemoveUnnassesaryWords(words.ToList(), sentence, context);
        }

        static string[] pasoyandJam = new string[] { "он", "ён", "ҳо" };
        static void ShakliJam(IEnumerable<Word> words, SqlServerContext context)
        {
            var wordsJam = words.Where(s => pasoyandJam.Any(d => s.Value.ToLower().EndsWith(d)));
            foreach (var item in wordsJam)
            {
                var splited = item.Value.Substring(0, item.Value.Length - 2);
                if (splited.Length > 1 && context.WordsDataSet.Any(s => s.WordValue == splited))
                {
                    item.Value = splited;
                }
            }
        }

        static string[] ishorakuni = new string[] { "ро", "ҳоро", "онро", "ёнро" };
        static void Ishorakuni(IEnumerable<Word> words, SqlServerContext context)
        {
            foreach (var ishora in ishorakuni)
            {
                foreach (var item in words.Where(s => s.Value.ToLower().EndsWith(ishora)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - ishora.Length);
                    if (splited.Length > 2 && context.WordsDataSet.Any(s => s.WordValue == splited))
                    {
                        item.Value = splited;
                    }
                }
            }
        }

        static string[] bandakiI = new string[] { "и", "ҳои", "они", "ёни" };
        static void BandakiI(IEnumerable<Word> words, SqlServerContext context)
        {
            foreach (var bandak in bandakiI)
            {
                foreach (var item in words.Where(s => s.Value.EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, context, out string bandakToEnd))
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

        static string[] bandakiE = new string[] { "е", "ҳое", "оне", "ёне" };
        static void BandakiE(IEnumerable<Word> words, SqlServerContext context)
        {
            foreach (var bandak in bandakiE)
            {
                foreach (var item in words.Where(s => s.Value.EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && DataSetContains(splited, context, out string bandakToEnd))
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

        static bool DataSetContains(string splited, SqlServerContext context, out string res)
        {
            res = null;
            if (splited.EndsWith("и"))
            {
                var addBandakToEnd = splited.Substring(0, splited.Length - 1) + "ӣ";
                bool contains = context.WordsDataSet.Any(s => s.WordValue == addBandakToEnd);
                if (contains)
                {
                    res = addBandakToEnd;
                    return true;
                }
            }
            return context.WordsDataSet.Any(s => s.WordValue == splited);
        }

        static string[] bandakiU = new string[] { "ҳову", "ону", "Ю", "у", "ёну" };
        static void BandakiU(IEnumerable<Word> words, SqlServerContext context)
        {
            foreach (var bandak in bandakiU)
            {
                foreach (var item in words.Where(s => s.Value.ToLower().EndsWith(bandak)))
                {
                    var splited = item.Value.Substring(0, item.Value.Length - bandak.Length);
                    if (splited.Length > 2 && context.WordsDataSet.Any(s => s.WordValue == splited))
                    {
                        item.Value = splited;
                    }
                }
            }
        }

        static void RemoveUnnassesaryWords(IEnumerable<Word> words, Sentence sentence, SqlServerContext sqlServerContext)
        {
            foreach (var item in words)
            {
                if (ShouldBeRemoved(item.Value, sqlServerContext))
                {
                    sentence.Words.Remove(item);
                }
            }
        }

        static bool ShouldBeRemoved(string word, SqlServerContext context)
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
            if (!context.WordsDataSet.Any(s => s.WordValue == word) || context.BandakDataSets.Any(s => s.Value == word) || context.PeshoyandDataSets.Any(s => s.Value == word) || context.JonishinDataSets.Any(s => s.Value == word))
            {
                return true;
            }
            return false;
        }
    }
}
