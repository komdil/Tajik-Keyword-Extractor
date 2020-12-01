using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSet.Json
{
    public class Word : IWordsDataSet
    {
        public Guid Guid { get; set; }

        public string Content { get; set; }

        public string ContentInfo { get; set; }
        public double IDF { get; set; }

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
