
using System.Collections.Generic;

namespace Model.DataSet
{
    public interface IContext
    {
        List<IWordsDataSet> Words { get; }
        List<IWordsDataSet> Bandaks { get; }
        List<IWordsDataSet> Jonishins { get; }
        List<IWordsDataSet> Peshoyands { get; }
    }
}
