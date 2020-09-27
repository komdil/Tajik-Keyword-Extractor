using System.Text.RegularExpressions;

namespace Model.KEA
{
    public class WordManager
    {
        public string RemoveUnnassesarySpaces(string text)
        {
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
            return text;
        }
    }
}
