using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TajikKEAHelper.TextNormilizer
{
    public class TextnormilizerManager
    {
        readonly string[] itemsToRemove = new string[] { "“", "»", "«", "”" };
        readonly string[] itemsToEmptySpace = new string[] { "\n", "–", ";" };
        public string RemoveUnnassesarySpaces(string text)
        {
            text = text.ToLower();
            bool preserveTabs = false;
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
            foreach (var item in itemsToRemove)
            {
                text = text.Replace(item, "");
            }

            foreach (var item in itemsToEmptySpace)
            {
                text = text.Replace(item, " ");
            }

            return text;
        }

        public IEnumerable<ReplaceMent> ReplaceMents { get; }

        public TextnormilizerManager()
        {
            ReplaceMents = KEAGlobal.Context.Replacements;
        }

        public string MakeReplaceMent(string text)
        {
            foreach (var item in ReplaceMents)
            {
                text = text.Replace(item.ReplaceFrom, item.ReplaceTo);
            }
            return text;
        }
    }
}
