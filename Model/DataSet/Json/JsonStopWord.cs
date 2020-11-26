using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSet.Json
{
    public class JsonStopWord : IWordsDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
    }
}
