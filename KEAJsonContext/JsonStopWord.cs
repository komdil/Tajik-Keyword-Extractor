using System;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class JsonStopWord : IWordsDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
