using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TajikKEA;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class TajikKEAJsonContext : IWordContext
    {
        public IEnumerable<TajikJsonWord> AllWords { get; set; }

        public IEnumerable<IWordsDataSet> Suffixes { get; }

        public IEnumerable<IWordsDataSet> Prepositions { get; }

        public IEnumerable<IWordsDataSet> Pronouns { get; }

        public IEnumerable<IWordsDataSet> StopWords { get; }

        public IEnumerable<IWordsDataSet> Words { get; }

        public IEnumerable<ReplaceMent> Replacements { get; }

        public TajikKEAJsonContext()
        {
            AllWords = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("dataset.json"));
            StopWords = JsonConvert.DeserializeObject<IEnumerable<TajikJsonStopWord>>(File.ReadAllText("StopWords.json")).Cast<IWordsDataSet>().ToList();
            Prepositions = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Jonishins.json")).Cast<IWordsDataSet>().ToList();
            Pronouns = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Peshoyands.json")).Cast<IWordsDataSet>().ToList();
            Suffixes = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Bandaks.json")).Cast<IWordsDataSet>().ToList();
            Words = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("WordsWithIDF.json")).Cast<IWordsDataSet>().ToList();
            Replacements = JsonConvert.DeserializeObject<IEnumerable<ReplaceMent>>(File.ReadAllText("Replacements.json")).ToList();
        }
    }
}
