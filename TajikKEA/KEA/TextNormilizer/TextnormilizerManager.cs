using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TajikKEA.TextNormilizer
{
    public class TextnormilizerManager
    {
        string[] itemsToRemove = new string[] { "“", "»", "«", "”" };
        string[] itemsToEmptySpace = new string[] { "\n", "–", ";" };
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

        public List<ReplaceMent> ReplaceMents { get; }

        public TextnormilizerManager()
        {
            var json = File.ReadAllText("DataSet\\Json\\Replacement.json");
            var replaceMent = JsonConvert.DeserializeObject<List<ReplaceMent>>(json);
            if (replaceMent == null)
                replaceMent = new List<ReplaceMent>();
            ReplaceMents = replaceMent;
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
