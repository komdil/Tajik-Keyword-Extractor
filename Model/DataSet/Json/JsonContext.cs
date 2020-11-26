using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model.DataSet.Json
{
    public class JsonContext : IWordContext
    {
        public List<Word> Words { get; set; }

        List<IWordsDataSet> IWordContext.Words => Words.Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Bandaks => Words.Where(w => w.IsBandak()).Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Jonishins => Words.Where(b => b.IsJonishin()).Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Peshoyands => Words.Where(s => s.IsPeshoyand()).Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> StopWords => stopWords;

        public JsonContext()
        {
            Words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\dataset.json"));
        }

        List<IWordsDataSet> stopWords = null;
        public List<IWordsDataSet> GetStopWords()
        {
            if (stopWords == null)
            {
                stopWords = JsonConvert.DeserializeObject<List<JsonStopWord>>(File.ReadAllText("DataSet\\Json\\dataset.json")).Cast<IWordsDataSet>().ToList();
            }
            return stopWords;
        }
    }
}
