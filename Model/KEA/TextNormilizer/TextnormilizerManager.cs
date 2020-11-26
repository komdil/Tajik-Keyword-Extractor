﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Model.KEA.TextNormilizer
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
    }
}
