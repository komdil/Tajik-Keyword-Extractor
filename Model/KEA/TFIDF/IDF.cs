using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.KEA.TFIDF
{
    public class IDF
    {
        public IDF(IEnumerable<Document> documents, string termin)
        {
            Documents = documents;
            Termin = termin;
        }

        public IEnumerable<Document> Documents { get; set; }

        public string Termin { get; set; }

        public double IDFValue { get; set; }

        public void CalculateIDF()
        {
            double countOfDocuments = Documents.Count();
            double countOfDocumentsWhichContainsTermin = Documents.Count(a => a.Content.Contains(Termin));
            if (countOfDocumentsWhichContainsTermin == 0)
            {
                IDFValue = 0;
                return;
            }

            IDFValue = Math.Log(countOfDocuments / countOfDocumentsWhichContainsTermin);
        }
    }
}
