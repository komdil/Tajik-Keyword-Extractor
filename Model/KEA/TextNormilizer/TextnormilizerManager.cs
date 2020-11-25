using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Model.KEA.TextNormilizer
{
    public class TextnormilizerManager
    {
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
