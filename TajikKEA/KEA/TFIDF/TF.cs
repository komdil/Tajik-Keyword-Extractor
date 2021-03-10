using System.Linq;

namespace TajikKEAHelper.TFIDF
{
    public class TF
    {
        public TF(TajikWord termin, Document.TajikDocument document)
        {
            Termin = termin;
            Document = document;
        }

        public TajikWord Termin { get; set; }

        public Document.TajikDocument Document { get; set; }

        public double CalculateTF()
        {
            double countOfWordInDocument = Document.Sentences.SelectMany(a => a.Words).Count(a => a.Value.ToLower() == Termin.Value.ToLower());
            double countOfWordsInDcoument = Document.Sentences.SelectMany(a => a.Words).Count();
            return countOfWordInDocument / countOfWordsInDcoument;
        }
    }
}
