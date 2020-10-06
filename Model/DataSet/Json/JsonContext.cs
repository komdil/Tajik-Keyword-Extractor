using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Model.DataSet.Json
{
    public class JsonContext
    {
        public List<Word> Words { get; set; }
        public JsonContext()
        {
            Words = JsonConvert.DeserializeObject<List<Word>>(File.ReadAllText("dataset.json"));
        }
    }
}
