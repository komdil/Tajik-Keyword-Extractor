using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DataSet.Json
{
    public class Word
    {
        public Guid Guid { get; set; }

        public string WordValue { get; set; }

        public string Info { get; set; }

        public bool IsBandak()
        {
            return Info.ToLower().Contains("бандак");
        }

        public bool IsPeshoyand()
        {
            return Info.ToLower().Contains("пешоянд");
        }

        public bool IsJonishin()
        {
            return Info.ToLower().Contains("ҷонишин");
        }
    }
}
