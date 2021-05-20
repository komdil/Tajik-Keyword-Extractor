using System;
using System.Collections.Generic;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class TajikJsonStopWord : IWordDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
        public double CommonIDF { get; set; }
        public IList<IDFCategoryLink> IDFCategoryLinks { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
