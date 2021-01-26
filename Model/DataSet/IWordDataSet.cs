using System;
using System.Collections.Generic;
using System.Text;

namespace TajikKEA.DataSet
{
    public interface IWordsDataSet
    {
        Guid Guid { get; set; }

        string Content { get; set; }

        string ContentInfo { get; set; }
    }
}
