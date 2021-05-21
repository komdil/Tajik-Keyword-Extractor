using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TajikKEA;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class TajikKEAContext : IWordContext
    {
        public IEnumerable<IWordDataSet> Suffixes { get; }

        public IEnumerable<IWordDataSet> Prepositions { get; }

        public IEnumerable<IWordDataSet> Pronouns { get; }

        public IEnumerable<IWordDataSet> StopWords { get; }

        public IEnumerable<IWordDataSet> Words { get; }

        public IEnumerable<ReplaceMent> Replacements { get; }

        public IEnumerable<IDFCategory> Categories { get; }

        public IEnumerable<IDFCategoryLink> IDFCategories { get; }

        public TajikKEAContext()
        {
            StopWords = JsonConvert.DeserializeObject<IEnumerable<TajikJsonStopWord>>(File.ReadAllText("StopWords.json")).Cast<IWordDataSet>().ToList();
            Prepositions = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Jonishins.json")).Cast<IWordDataSet>().ToList();
            Pronouns = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Peshoyands.json")).Cast<IWordDataSet>().ToList();
            Suffixes = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Bandaks.json")).Cast<IWordDataSet>().ToList();
            Words = JsonConvert.DeserializeObject<IEnumerable<TajikJsonWord>>(File.ReadAllText("Words.json")).Cast<IWordDataSet>().ToList();
            Replacements = JsonConvert.DeserializeObject<IEnumerable<ReplaceMent>>(File.ReadAllText("Replacement.json")).ToList();
            Categories = JsonConvert.DeserializeObject<IEnumerable<IDFCategory>>(File.ReadAllText("Categories.json")).ToList();
        }
    }
}
