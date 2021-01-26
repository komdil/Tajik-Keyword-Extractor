using System;
using System.Collections.Generic;
using System.Text;

namespace TajikKEA.DataSet
{
    public interface IWordWithIDF : IWordsDataSet
    {
        public double IDF { get; set; }
    }
}
