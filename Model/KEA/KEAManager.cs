using Model.DataSet;
using Model.KEA.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class KEAManager
    {
        public IWordContext Context { get; }
        public KEAManager(IWordContext context)
        {
            Context = context;
        }

        Logger logger;
        public Logger Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = new Logger();
                }
                return logger;
            }
        }

        public List<string> GetSimpleKeywords(string text)
        {
            var document = new Document.Document(text);
            document.Sentences.ForEach(s => s.NormalizeWords());
            var tfIdf = KEAGlobal.TFIDFManager.CalculateTFIDF(new List<Document.Document> { document }, document).OrderByDescending(s => s.TF_IDF).ThenByDescending(s => s.TF).ThenByDescending(s => s.IDF);
            return tfIdf.Select(s => s.Word).ToList();
        }
    }
}
