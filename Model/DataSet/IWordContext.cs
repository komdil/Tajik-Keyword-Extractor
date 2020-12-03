
using System.Collections.Generic;

namespace Model.DataSet
{
    public interface IWordContext
    {
        List<IWordsDataSet> Words { get; }
        List<IWordsDataSet> Bandaks { get; }
        List<IWordsDataSet> Jonishins { get; }
        List<IWordsDataSet> Peshoyands { get; }
        List<IWordsDataSet> StopWords { get; }
        List<IWordWithIDF> WordsWithIDF { get; }
    }
}
