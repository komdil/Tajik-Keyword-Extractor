using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.KEA.TFIDF
{
    public class IDF
    {
        public IDF(IEnumerable<Document.Document> documents, Word termin)
        {
            Documents = documents;
            Termin = termin;
        }

        public IEnumerable<Document.Document> Documents { get; set; }

        public Word Termin { get; set; }

        public double IDFValue { get; set; }

        public void CalculateIDF()
        {
            double countOfDocuments = Documents.Count();
            double countOfDocumentsWhichContainsTermin = Documents.Count(a => a.Content.Contains(Termin.Value));
            if (countOfDocumentsWhichContainsTermin == 0)
            {
                IDFValue = 0;
                return;
            }

            IDFValue = Math.Log(countOfDocuments / countOfDocumentsWhichContainsTermin);
        }
    }
}
