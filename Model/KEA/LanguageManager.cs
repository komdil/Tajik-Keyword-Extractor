using Model.DataSet;
using Model.KEA.Document;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class LanguageManager
    {
        public IContext Context { get; }
        public LanguageManager(IContext context)
        {
            Context = context;
        }

        static Logger logger;
        public static Logger Logger
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

        public string RemoveUnnassesarySpaces(string text)
        {
            text = text.ToLower();
            bool preserveTabs = false;
            text = text.Replace("\n", " ");
            text = text.Trim();
            text = Regex.Replace(text, @" +", " ");
            if (preserveTabs)
            {
                text = Regex.Replace(text, @" *\t *", "\t");
            }
            else
            {
                text = Regex.Replace(text, @"[ \t]+", " ");
            }
            text = Regex.Replace(text, Statics.RemoveUnnassesarySpacesPattern, "\n");
            text = text.Replace("»", "");
            text = text.Replace("«", "");
            return text;
        }
    }
}
