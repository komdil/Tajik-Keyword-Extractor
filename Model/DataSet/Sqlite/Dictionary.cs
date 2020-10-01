using System;
using System.Collections.Generic;
using System.Text;

namespace Model.KEADataSet.Sqlite
{
    public class Dictionary
    {
        public int _id { get; set; }
        public string from_language { get; set; }
        public string to_language { get; set; }
        public virtual List<Word> Words { get; set; }
    }
}
