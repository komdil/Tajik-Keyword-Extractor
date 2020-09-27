using System;
using System.Collections.Generic;
using System.Text;

namespace Model.KEA
{
    public static class Statics
    {
        public const string SplitSentensePattern = @"(?<=[\.!\?])\s+";

        public const string RemoveUnnassesarySpacesPattern = @"([\t ]*(\n)+[\t ]*)+";

        public const string SentenceEndSymbols = @"!|:|,|\.|\?";
    }
}