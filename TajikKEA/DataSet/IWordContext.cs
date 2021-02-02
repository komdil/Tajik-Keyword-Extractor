
using System.Collections.Generic;

namespace TajikKEA.DataSet
{
    /// <summary>
    /// Context which contains tajik words
    /// </summary>
    public interface IWordContext
    {
        /// <summary>
        /// RU: Слова
        /// TJ: Калимаҳо
        /// </summary>
        IEnumerable<IWordsDataSet> Words { get; }

        /// <summary>
        /// RU: Суффиксы
        /// TJ: Бандакҳо
        /// </summary>
        IEnumerable<IWordsDataSet> Suffixes { get; }

        /// <summary>
        /// RU: Местоимение
        /// TJ: Ҷонишинҳо
        /// </summary>
        IEnumerable<IWordsDataSet> Pronouns { get; }

        /// <summary>
        /// RU: Предлоги
        /// TJ: Пешояндҳоо
        /// </summary>
        IEnumerable<IWordsDataSet> Prepositions { get; }

        /// <summary>
        /// RU: Стоп-Слова
        /// TJ: Стоп-Калимаҳо
        /// </summary>
        IEnumerable<IWordsDataSet> StopWords { get; }

        /// <summary>
        /// RU: Изминение слов 
        /// TJ: Ивазкунии калима
        /// </summary>
        IEnumerable<ReplaceMent> Replacements { get; }
    }
}
