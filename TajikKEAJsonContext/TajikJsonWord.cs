using System;
using System.Collections.Generic;
using TajikKEA.DataSet;

namespace TajikKEAJsonContext
{
    public class TajikJsonWord : IWordDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
        public double CommonIDF { get; set; }
        public IList<IDFCategoryLink> IDFCategoryLinks { get; set; }

        public bool IsBandak()
        {
            return ContentInfo.ToLower().Contains("бандак");
        }

        public bool IsPeshoyand()
        {
            return ContentInfo.ToLower().Contains("пешоянд");
        }

        public bool IsJonishin()
        {
            return ContentInfo.ToLower().Contains("ҷонишин");
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
