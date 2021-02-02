using System;
using System.Collections.Generic;
using System.Linq;

namespace TajikKEA.TFIDF
{
    public class IDF
    {
        public IDF(IEnumerable<Document.TajikDocument> documents, TajikWord termin)
        {
            Documents = documents;
            Termin = termin;
        }

        public IEnumerable<Document.TajikDocument> Documents { get; set; }

        public TajikWord Termin { get; set; }

        public double CalculateIDF()
        {
            double countOfDocuments = Documents.Count();
            double countOfDocumentsWhichContainsTermin = Documents.Count(a => a.Content.Contains(Termin.Value));
            if (countOfDocumentsWhichContainsTermin == 0)
                return 0;
            return Math.Log(countOfDocuments / countOfDocumentsWhichContainsTermin);
        }
    }
}
