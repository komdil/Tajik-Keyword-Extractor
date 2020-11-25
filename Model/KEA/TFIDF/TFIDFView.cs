using System;
using System.Collections.Generic;
using System.Text;

namespace Model.KEA.TFIDF
{
    public class TFIDFView
    {
        public string Word { get; set; }

        public double TF { get; set; }

        public double IDF { get; set; }

        public double TF_IDF { get; set; }

        public override string ToString()
        {
            return Word;
        }
    }
}
