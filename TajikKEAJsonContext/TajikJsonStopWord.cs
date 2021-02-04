using System;
using System.Collections.Generic;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class TajikJsonStopWord : IWordsDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
        public double IDF { get; set; }
        public IEnumerable<IDFCategoryLink> IDFCategoryLinks { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
