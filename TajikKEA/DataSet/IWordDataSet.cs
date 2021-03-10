using System;
using System.Collections.Generic;

namespace TajikKEAHelper.DataSet
{
    /// <summary>
    /// Sctructure of one word
    /// </summary>
    public interface IWordsDataSet
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
        /// TJ: Муҳимияти калима
        /// </summary>
        double IDF { get; set; }

        /// <summary>
        /// Category links
        /// RU: Важность слова в категории
        /// TJ: Муҳимияти калима вобаста аз категорияҳо
        /// </summary>
        IEnumerable<IDFCategoryLink> IDFCategoryLinks { get; set; }
    }
}
