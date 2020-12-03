using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSet.SqlServer
{
    public class WordsWithIDFDataSet : IWordWithIDF
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
        public double IDF { get; set; }
    }
}
