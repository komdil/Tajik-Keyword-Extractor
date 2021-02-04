using System;
using System.Collections.Generic;

namespace TajikKEA.DataSet
{
    public class IDFCategory
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<IDFCategoryLink> IDFCategoryLinks { get; set; }
    }
}
