using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model.DataSet.Json
{
    public class JsonContext : IContext
    {
        public List<Word> Words { get; set; }

        List<IWordsDataSet> IContext.Words => Words.Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Bandaks => Words.Where(w => w.IsBandak()).Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Jonishins => Words.Where(b => b.IsJonishin()).Cast<IWordsDataSet>().ToList();

        public List<IWordsDataSet> Peshoyands => Words.Where(s => s.IsPeshoyand()).Cast<IWordsDataSet>().ToList();

        public JsonContext()
        {
            Words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("DataSet\\Json\\dataset.json"));
        }
    }
}
