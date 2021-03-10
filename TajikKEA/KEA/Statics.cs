using System;
using System.Collections.Generic;
using System.Text;

namespace TajikKEAHelper
{
    public static class Statics
    {
        public const string SplitSentensePattern = @"(?<=[\.!\?])\s+";

        public const string RemoveUnnassesarySpacesPattern = @"([\t ]*(\n)+[\t ]*)+";

        public const string SentenceEndSymbols = @"!|:|,|\.|\?";
    }
}