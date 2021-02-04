using System.Collections.Generic;
using System.Linq;
using TajikKEA.DataSet;

namespace TajikKEA
{
    public class KEAManager
    {
        public IWordContext Context { get; }
        public KEAManager(IWordContext context)
        {
            Context = context;
        }
        public List<string> GetKeywords(string text, int countOfKeywords)
        {
            var document = new Document.TajikDocument(text);
            document.Sentences.ForEach(s => s.NormalizeWords());
            var tfIdf = KEAGlobal.TFIDFManager.CalculateTFIDFWithIDF(new List<Document.TajikDocument> { document }, document).OrderByDescending(s => s.TF_IDF).ThenByDescending(s => s.TF).ThenByDescending(s => s.IDF);
            return tfIdf.Select(s => s.Word).ToList();
        }

        public List<string> GetKeywords(string text, int countOfKeywords, IDFCategory category)
        {
            var document = new Document.TajikDocument(text);
            document.Sentences.ForEach(s => s.NormalizeWords());
            var tfIdf = KEAGlobal.TFIDFManager.CalculateTFIDFWithIDF(new List<Document.TajikDocument> { document }, document).OrderByDescending(s => s.TF_IDF).ThenByDescending(s => s.TF).ThenByDescending(s => s.IDF);
            return tfIdf.Select(s => s.Word).ToList();
        }
    }
}
