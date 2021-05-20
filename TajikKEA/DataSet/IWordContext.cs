
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
        IEnumerable<IWordDataSet> Words { get; }

        /// <summary>
        /// RU: Суффиксы
        /// TJ: Бандакҳо
        /// </summary>
        IEnumerable<IWordDataSet> Suffixes { get; }

        /// <summary>
        /// RU: Местоимение
        /// TJ: Ҷонишинҳо
        /// </summary>
        IEnumerable<IWordDataSet> Pronouns { get; }

        /// <summary>
        /// RU: Предлоги
        /// TJ: Пешояндҳоо
        /// </summary>
        IEnumerable<IWordDataSet> Prepositions { get; }

        /// <summary>
        /// RU: Стоп-Слова
        /// TJ: Стоп-Калимаҳо
        /// </summary>
        IEnumerable<IWordDataSet> StopWords { get; }

        /// <summary>
        /// RU: Изминение слов 
        /// TJ: Ивазкунии калима
        /// </summary>
        IEnumerable<ReplaceMent> Replacements { get; }

        /// <summary>
        /// RU: Категории
        /// TJ: Категорияҳо
        /// </summary>
        IEnumerable<IDFCategory> Categories { get; }

        /// <summary>
        /// RU: IDF в Категории
        /// TJ: IDF дар Категорияҳо
        /// </summary>
        IEnumerable<IDFCategoryLink> IDFCategories { get; }
    }
}
