using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model.DataSet.Json
{
    public class JsonContext : IWordContext
    {
        public List<Word> AllWords { get; set; }

        public List<IWordsDataSet> Bandaks { get; }

        public List<IWordsDataSet> Jonishins { get; }

        public List<IWordsDataSet> Peshoyands { get; }

        public List<IWordsDataSet> StopWords { get; }

        public List<IWordsDataSet> Words { get; }

        public List<IWordWithIDF> WordsWithIDF { get; }

        public JsonContext()
        {
            AllWords = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\dataset.json"));
            StopWords = JsonConvert.DeserializeObject<List<JsonStopWord>>(File.ReadAllText("DataSet\\Json\\StopWords.json")).Cast<IWordsDataSet>().ToList();
            Words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\Words.json")).Cast<IWordsDataSet>().ToList();
            Jonishins = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\Jonishins.json")).Cast<IWordsDataSet>().ToList();
            Peshoyands = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\Peshoyands.json")).Cast<IWordsDataSet>().ToList();
            Bandaks = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\Bandaks.json")).Cast<IWordsDataSet>().ToList();
            WordsWithIDF = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\WordsWithIDF.json")).Cast<IWordWithIDF>().ToList();
        }
    }
}
