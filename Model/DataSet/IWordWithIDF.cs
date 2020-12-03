using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSet
{
    public interface IWordWithIDF : IWordsDataSet
    {
        public double IDF { get; set; }
    }
}
