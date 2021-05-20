using System;
using System.Collections.Generic;

namespace TajikKEA.DataSet
{
    /// <summary>
    /// Sctructure of one word
    /// </summary>
    public interface IWordDataSet
    {
        /// <summary>
        /// Unique key of word
        /// </summary>
        Guid Guid { get; set; }

        /// <summary>
        /// Word
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Shourt info about Word
        /// </summary>
        string ContentInfo { get; set; }

        /// <summary>
        /// IDF value of word
        /// RU: Важность слова
        /// TJ: Муҳимияти калима дар умум
        /// </summary>
        double CommonIDF { get; set; }

        /// <summary>
        /// Category links
        /// RU: Важность слова в категории
        /// TJ: Муҳимияти калима вобаста аз категорияҳо
        /// </summary>
        IList<IDFCategoryLink> IDFCategoryLinks { get; set; }
    }
}
