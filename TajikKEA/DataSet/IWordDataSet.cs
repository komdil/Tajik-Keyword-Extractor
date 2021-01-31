using System;

namespace TajikKEA.DataSet
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
    }
}
