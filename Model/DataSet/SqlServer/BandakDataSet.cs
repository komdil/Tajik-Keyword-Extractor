using System;

namespace Model.DataSet.SqlServer
{
    public class BandakDataSet : IWordsDataSet
    {
        public Guid Guid { get; set; }
        public string Content { get; set; }
        public string ContentInfo { get; set; }
    }
}
