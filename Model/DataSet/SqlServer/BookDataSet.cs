using System;
using System.Text;

namespace Model.DataSet.SqlServer
{
    public class BookDataSet
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}
