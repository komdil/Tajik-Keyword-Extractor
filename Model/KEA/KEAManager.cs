using Model.DataSet;
using Model.KEA.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class KEAManager
    {
        public IContext Context { get; }
        public KEAManager(IContext context)
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
            return document.Sentences.SelectMany(s => s.Words.Select(s => s.Value)).ToList();
        }
    }
}
